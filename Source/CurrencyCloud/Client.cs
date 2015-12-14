using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CurrencyCloud.Entity;
using CurrencyCloud.Extension;
using CurrencyCloud.Exception;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Entity.List;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CurrencyCloud.Tests")]

namespace CurrencyCloud
{
    /// <summary>
    /// Represents API client
    /// </summary>
    public class Client
    {
        private HttpClient httpClient;
        private dynamic credentials;
        private string onBehalfOf;

        internal string Token
        {
            get
            {
                return httpClient.DefaultRequestHeaders.GetValues("X-Auth-Token").FirstOrDefault();
            }
            set
            {
                httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", value);
            }
        }

        #region Request

        private async Task<string> AuthorizeAsync()
        {
            string requestUri = string.Format("/v2/authenticate/api?login_id={0}&api_key={1}", credentials.LoginId, credentials.ApiKey);

            HttpResponseMessage res = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri));
            if (res.IsSuccessStatusCode)
            {
                string resString = await res.Content.ReadAsStringAsync();
                JObject resObject = JObject.Parse(resString);

                var token = resObject["auth_token"].Value<string>();
                httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", token);

                return token;
            }
            else
            {
                throw await ApiExceptionFactory.FromHttpResponse(res);
            }
        }

        private async Task<TResult> RequestAsync<TResult>(string path, HttpMethod method, ParamsObject paramsObj = null)
        {
            if (httpClient == null)
            {
                throw new InvalidOperationException("Client is not initialized.");
            }

            if (onBehalfOf != null)
            {
                if(paramsObj == null)
                {
                    paramsObj = new ParamsObject();
                }

                paramsObj.Add(new
                {
                    OnBehalfOf = onBehalfOf
                });
            }

            string requestUri = path;
            if (paramsObj != null && !paramsObj.IsEmpty)
            {
                requestUri += "?" + paramsObj.ToQueryString();
            }

            Func<Task<TResult>> requestAsyncDelegate = async () => 
            {
                HttpResponseMessage res = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri));
                if (res.IsSuccessStatusCode)
                {
                    string resString = await res.Content.ReadAsStringAsync();

                    var serializerSettings = new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new PascalContractResolver()
                    };

                    return JsonConvert.DeserializeObject<TResult>(resString, serializerSettings);
                }
                else
                {
                    throw await ApiExceptionFactory.FromHttpResponse(res);
                }
            };

            for (int attempts = 0;; attempts++)
            {
                try
                {
                    if (attempts > 0)
                    {
                        await AuthorizeAsync();
                    }

                    return await requestAsyncDelegate();
                }
                catch (AuthenticationException)
                {
                    if (attempts == 3)
                    {
                        throw;
                    }
                }
            }
        }

        private Task RequestAsync(string path, HttpMethod method, ParamsObject paramsObj = null)
        {
            return RequestAsync<object>(path, method, paramsObj);
        }

        #endregion

        #region OnBehalfOf

        /// <summary>
        /// Executes operations on behalf of another contact.
        /// </summary>
        /// <param name="id">Id of the contact.</param>
        /// <param name="function">Asynchronous function, which is executed on behalf of the given contact.</param>
        /// <returns>Asynchronous task.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the previous call of the method has not yet completed.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="id"/> is not a valid UUID.</exception>
        public async Task OnBehalfOf(string id, Func<Task> function)
        {
            if (onBehalfOf != null)
            {
                throw new InvalidOperationException("OnBehalfOf has already been called and not yet completed.");
            }

            string UuidPattern = @"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$";
            if (!Regex.IsMatch(id, UuidPattern))
            {
                throw new ArgumentException("Id is not a valid UUID", "id");
            }

            onBehalfOf = id;

            try
            {
                await function();
            }
            finally
            {
                onBehalfOf = null;
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Gets a value that indicates whether the client is initialized.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return httpClient != null;
            }
        }

        /// <summary>
        /// Initializes the client and generates authentication token for the API user.
        /// </summary>
        /// <param name="apiServer">API server to make requests against.</param>
        /// <param name="loginId">Login id of the API user.</param>
        /// <param name="apiKey">API key of the API user.</param>
        /// <returns>Asynchronous task, which returns the authentication token.</returns>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<string> InitializeAsync(ApiServer apiServer, string loginId, string apiKey)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiServer.Url);

            credentials = new
            {
                LoginId = loginId,
                ApiKey = apiKey
            };

            return await AuthorizeAsync();
        }

        /// <summary>
        /// Closes current session and resets the client.
        /// </summary>
        /// <returns>Asynchronous task.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task CloseAsync()
        {
            if (httpClient == null)
            {
                throw new InvalidOperationException("Client is not initialized.");
            }

            HttpResponseMessage res = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/v2/authenticate/close_session"));
            if (res.IsSuccessStatusCode)
            {
                credentials = null;

                httpClient.Dispose();
                httpClient = null;
            }
            else
            {
                throw await ApiExceptionFactory.FromHttpResponse(res);
            }
        }

        #endregion

        #region Accounts

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountName">Name of the account.</param>
        /// <param name="legalEntityType">Type of the account's legal entity.</param>
        /// <param name="optional">Optional parameters of the account.</param>
        /// <returns>Asynchronous task, which returns newly created account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Account> CreateAccountAsync(string accountName, string legalEntityType, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.AccountName = accountName;
            paramsObj.LegalEntityType = legalEntityType;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Account>("/v2/accounts/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of an account.
        /// </summary>
        /// <param name="id">Id of the requested account.</param>
        /// <param name="optional">Optional parameters of the requested account.</param>
        /// <returns>Asynchronous task, which returns the requested account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Account> GetAccountAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Account>("/v2/accounts/" + id, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Updates an existing account.
        /// </summary>
        /// <param name="id">Id of the updated account.</param>
        /// <param name="optional">Optional parameters of the updated account.</param>
        /// <returns>Asynchronous task, which returns the updated account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Account> UpdateAccountAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Account>("/v2/accounts/" + id, HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Finds accounts matching the search criteria for the active user.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought accounts.</param>
        /// <returns>Asynchronous task, which returns the list of the found accounts, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedAccounts> FindAccountsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedAccounts>("/v2/accounts/find", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets details of the active account.
        /// </summary>
        /// <returns>Asynchronous task, which returns the active account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Account> GetCurrentAccountAsync()
        {
            return await RequestAsync<Account>("/v2/accounts/current", HttpMethod.Get);
        }

        #endregion

        #region Balances

        /// <summary>
        /// Gets the balance for a currency.
        /// </summary>
        /// <param name="currency">Currency to get the balance for.</param>
        /// <param name="optional">Optional parameters of the requested balance.</param>
        /// <returns>Asynchronous task, which returns the requested balance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Balance> GetBalanceAsync(string currency, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Balance>("/v2/balances/" + currency, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Finds balances matching the search criteria.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought balances.</param>
        /// <returns>Asynchronous task, which returns the list of the found balances, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedBalances> FindBalancesAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedBalances>("/v2/balances/find", HttpMethod.Get, paramsObj);
        }

        #endregion

        #region Beneficiaries

        /// <summary>
        /// Validates beneficiary details without creating one.
        /// </summary>
        /// <param name="bankCountry">Country of the beneficiary's bank.</param>
        /// <param name="currency">Currency of the beneficiary.</param>
        /// <param name="beneficiaryCountry">Country of the beneficiary.</param>
        /// <param name="optional">Optional parameters of the beneficiary.</param>
        /// <returns>Asynchronous task, which returns the validated beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> ValidateBeneficiaryAsync(string bankCountry, string currency, string beneficiaryCountry, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.BankCountry = bankCountry;
            paramsObj.Currency = currency;
            paramsObj.BeneficiaryCountry = beneficiaryCountry;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/validate", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Creates a new beneficiary.
        /// </summary>
        /// <param name="bankAccountHolderName">Name of the beneficiary's bank account holder.</param>
        /// <param name="bankCountry">Country of the beneficiary's bank.</param>
        /// <param name="currency">Currency of the beneficiary.</param>
        /// <param name="name">Name of the beneficiary.</param>
        /// <param name="optional">Optional parameters of the beneficiary.</param>
        /// <returns>Asynchronous task, which returns newly created beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> CreateBeneficiaryAsync(string bankAccountHolderName, string bankCountry, string currency, string name, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.BankAccountHolderName = bankAccountHolderName;
            paramsObj.BankCountry = bankCountry;
            paramsObj.Currency = currency;
            paramsObj.Name = name;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a beneficiary.
        /// </summary>
        /// <param name="id">Id of the requested beneficiary.</param>
        /// <param name="optional">Optional parameters of the requested beneficiary.</param>
        /// <returns>Asynchronous task, which returns the requested beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> GetBeneficiaryAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/" + id, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Updates an existing beneficiary.
        /// </summary>
        /// <param name="id">Id of the updated beneficiary.</param>
        /// <param name="optional">Optional parameters of the updated beneficiary.</param>
        /// <returns>Asynchronous task, which returns the updated beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> UpdateBeneficiaryAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/" + id, HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Finds beneficiaries matching the search criteria for the active user.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought beneficiaries.</param>
        /// <returns>Asynchronous task, which returns the list of the found beneficiaries, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedBeneficiaries> FindBeneficiariesAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedBeneficiaries>("/v2/beneficiaries/find", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Deletes an existing beneficiary.
        /// </summary>
        /// <param name="id">Id of the deleted beneficiary.</param>
        /// <param name="optional">Optional parameters of the deleted beneficiary.</param>
        /// <returns>Asynchronous task, which returns the deleted beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> DeleteBeneficiaryAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/" + id + "/delete", HttpMethod.Post, paramsObj);
        }

        #endregion

        #region Contacts

        /// <summary>
        /// Creates reset token for the contact.
        /// </summary>
        /// <param name="loginId">Login id of the contact.</param>
        /// <returns>Asynchronous task.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task CreateResetTokenAsync(string loginId)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.LoginId = loginId;

            await RequestAsync("/v2/contacts/reset_token/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Creates a new contact.
        /// </summary>
        /// <param name="accountId">Id of the corresponding account.</param>
        /// <param name="firstName">First name of the contact.</param>
        /// <param name="lastName">Last name of the contact.</param>
        /// <param name="emailAddress">Email address of the contact.</param>
        /// <param name="phoneNumber">Phone number of the contact.</param>
        /// <param name="optional">Optional parameters of the contact.</param>
        /// <returns>Asynchronous task, which returns newly created contact.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Contact> CreateContactAsync(string accountId, string firstName, string lastName, string emailAddress, string phoneNumber, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.AccountId = accountId;
            paramsObj.FirstName = firstName;
            paramsObj.LastName = lastName;
            paramsObj.EmailAddress = emailAddress;
            paramsObj.PhoneNumber = phoneNumber;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Contact>("/v2/contacts/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a contact.
        /// </summary>
        /// <param name="id">Id of the requested contact.</param>
        /// <param name="optional">Optional parameters of the requested contact.</param>
        /// <returns>Asynchronous task, which returns the requested contact.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Contact> GetContactAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Contact>("/v2/contacts/" + id, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="id">Id of the updated contact.</param>
        /// <param name="optional">Optional parameters of the updated contact.</param>
        /// <returns>Asynchronous task, which returns the updated contact.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Contact> UpdateContactAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Contact>("/v2/contacts/" + id, HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Finds contacts matching the search criteria for the active user.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought contacts.</param>
        /// <returns>Asynchronous task, which returns the list of the found contacts, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedContacts> FindContactsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedContacts>("/v2/contacts/find", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets details of the active contact.
        /// </summary>
        /// <returns>Asynchronous task, which returns the active contact.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Contact> GetCurrentContactAsync()
        {
            return await RequestAsync<Contact>("/v2/contacts/current", HttpMethod.Get);
        }

        #endregion

        #region Conversions

        /// <summary>
        /// Creates a new conversion.
        /// </summary>
        /// <param name="buyCurrency">Currency to buy.</param>
        /// <param name="sellCurrency">Currency to sell.</param>
        /// <param name="fixedSide">Fixed conversion side: buy or sell.</param>
        /// <param name="amount">Amount to convert.</param>
        /// <param name="reason">Reason for conversion.</param>
        /// <param name="termAgreement">Agreement flag.</param>
        /// <param name="optional">Optional parameters of the conversion.</param>
        /// <returns>Asynchronous task, which returns newly created conversion.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Conversion> CreateConversionAsync(string buyCurrency, string sellCurrency, string fixedSide, decimal amount, string reason, bool termAgreement, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.BuyCurrency = buyCurrency;
            paramsObj.SellCurrency = sellCurrency;
            paramsObj.FixedSide = fixedSide;
            paramsObj.Amount = amount;
            paramsObj.Reason = reason;
            paramsObj.TermAgreement = termAgreement;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Conversion>("/v2/conversions/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a conversion.
        /// </summary>
        /// <param name="id">Id of the requested conversion.</param>
        /// <param name="optional">Optional parameters of the requested conversion.</param>
        /// <returns>Asynchronous task, which returns the requested conversion.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Conversion> GetConversionAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Conversion>("/v2/conversions/" + id, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Finds conversions matching the search criteria for the active user.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought conversions.</param>
        /// <returns>Asynchronous task, which returns the list of the found conversions, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedConversions> FindConversionsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedConversions>("/v2/conversions/find", HttpMethod.Get, paramsObj);
        }

        #endregion

        #region Payers

        /// <summary>
        /// Gets details of a payer.
        /// </summary>
        /// <param name="id">Id of the requested payer.</param>
        /// <param name="optional">Optional parameters of the requested payer.</param>
        /// <returns>Asynchronous task, which returns the requested payer.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payer> GetPayerAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Payer>("/v2/payers/" + id, HttpMethod.Get, paramsObj);
        }

        #endregion

        #region Payments

        /// <summary>
        /// Creates a new payment.
        /// </summary>
        /// <param name="currency">Payment currency.</param>
        /// <param name="beneficiaryId">Id of the payment beneficiary.</param>
        /// <param name="amount">Payment amount.</param>
        /// <param name="reason">Payment reason.</param>
        /// <param name="reference">Payment reference</param>
        /// <param name="optional">Optional parameters of the payment.</param>
        /// <returns>Asynchronous task, which returns newly created payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payment> CreatePaymentAsync(string currency, string beneficiaryId, int amount, string reason, string reference, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.Currency = currency;
            paramsObj.BeneficiaryId = beneficiaryId;
            paramsObj.Amount = amount;
            paramsObj.Reason = reason;
            paramsObj.Reference = reference;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Payment>("/v2/payments/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a payment.
        /// </summary>
        /// <param name="id">Id of the requested payment.</param>
        /// <param name="optional">Optional parameters of the requested payment.</param>
        /// <returns>Asynchronous task, which returns the requested payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payment> GetPaymentAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Payment>("/v2/payments/" + id, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Updates an existing payment.
        /// </summary>
        /// <param name="id">Id of the updated payment.</param>
        /// <param name="optional">Optional parameters of the updated payment.</param>
        /// <returns>Asynchronous task, which returns the updated payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payment> UpdatePaymentAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Payment>("/v2/payments/" + id, HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Finds payments matching the search criteria for the active user.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought payments.</param>
        /// <returns>Asynchronous task, which returns  the list of the found payments, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedPayments> FindPaymentsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedPayments>("/v2/payments/find", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Deletes an existing payment.
        /// </summary>
        /// <param name="id">Id of the deleted payment.</param>
        /// <param name="optional">Optional parameters of the deleted payment.</param>
        /// <returns>Asynchronous task, which returns the deleted payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payment> DeletePaymentAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Payment>("/v2/payments/" + id + "/delete", HttpMethod.Post, paramsObj);
        }

        #endregion

        #region Rates

        /// <summary>
        /// Gets a full quote for the requested currency based on the spread table of the active contact.
        /// </summary>
        /// <param name="buyCurrency">Currency to buy.</param>
        /// <param name="sellCurrency">Currency to sell.</param>
        /// <param name="fixedSide">Fixed conversion side: buy or sell.</param>
        /// <param name="amount">Amount to convert.</param>
        /// <param name="optional">Optional parameters of the requested rate.</param>
        /// <returns>Asynchronous task, which returns the requested rate.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Rate> GetRateAsync(string buyCurrency, string sellCurrency, string fixedSide, int amount, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.BuyCurrency = buyCurrency;
            paramsObj.SellCurrency = sellCurrency;
            paramsObj.FixedSide = fixedSide;
            paramsObj.Amount = amount;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Rate>("/v2/rates/detailed", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets core rate information for multiple currency pairs.
        /// </summary>
        /// <param name="currencyPair"></param>
        /// <param name="optional">Optional parameters of the sought rates.</param>
        /// <returns>Asynchronous task, which returns the list of the found rates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<RatesList> FindRatesAsync(string currencyPair, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.CurrencyPair = currencyPair;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<RatesList>("/v2/rates/find", HttpMethod.Get, paramsObj);
        }

        #endregion

        #region Reference

        /// <summary>
        /// Gets required beneficiary details and their basic validation formats.
        /// </summary>
        /// <param name="optional">Optional beneficiary parameters.</param>
        /// <returns>Asynchronous task, which returns the list of the required beneficiary details.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<BeneficiaryDetailsList> GetBeneficiaryRequiredDetailsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<BeneficiaryDetailsList>("/v2/reference/beneficiary_required_details", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets dates for which the given currency pair can not be traded.
        /// </summary>
        /// <param name="conversionPair">Currency conversion pair.</param>
        /// <param name="optional">Optional conversion parameters.</param>
        /// <returns>Asynchronous task, which returns the list of the conversion dates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<ConversionDatesList> GetConversionDatesAsync(string conversionPair, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.ConversionPair = conversionPair;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<ConversionDatesList>("/v2/reference/conversion_dates", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets a list of all the currencies that are tradeable.
        /// </summary>
        /// <returns>Asynchronous task, which returns the list of the currencies.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<CurrenciesList> GetAvailableCurrenciesAsync()
        {
            return await RequestAsync<CurrenciesList>("/v2/reference/currencies", HttpMethod.Get);
        }

        /// <summary>
        /// Gets dates for which the given currency can not be paid.
        /// </summary>
        /// <param name="currency">Currency name.</param>
        /// <param name="optional">Optional currency payment parameters.</param>
        /// <returns>Asynchronous task, which returns the list of the payment dates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaymentDatesList> GetPaymentDatesAsync(string currency, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.Currency = currency;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaymentDatesList>("/v2/reference/payment_dates", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets settlement account information, detailing where funds need to be sent to.
        /// </summary>
        /// <param name="optional">Optional settlement account parameters.</param>
        /// <returns>Asynchronous task, which returns the list of the found rates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<SettlementAccountsList> GetSettlementAccountsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<SettlementAccountsList>("/v2/reference/settlement_accounts", HttpMethod.Get, paramsObj);
        }

        #endregion

        #region Settlements

        /// <summary>
        /// Creates a new settlement.
        /// </summary>
        /// <param name="optional">Optional parameters of the settlement.</param>
        /// <returns>Asynchronous task, which returns newly created settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> CreateSettlementAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Settlement>("/v2/settlements/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a settlement.
        /// </summary>
        /// <param name="id">Id of the requested settlement.</param>
        /// <param name="optional">Optional parameters of the requested settlement.</param>
        /// <returns>Asynchronous task, which returns the requested settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> GetSettlementAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Settlement>("/v2/settlements/" + id, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Finds settlements matching the search criteria for the active user.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought settlements.</param>
        /// <returns>Asynchronous task, which returns  the list of the found settlements, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedSettlements> FindSettlementsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedSettlements>("/v2/settlements/find", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Deletes an existing settlement.
        /// </summary>
        /// <param name="id">Id of the deleted settlement.</param>
        /// <param name="optional">Optional parameters of the deleted settlement.</param>
        /// <returns>Asynchronous task, which returns the deleted settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> DeleteSettlementAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/delete", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Adds conversion to an open settlement.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <param name="conversionId">Id of the conversion.</param>
        /// <param name="optional">Optional parameters of the updated settlement.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> AddConversionToSettlementAsync(string id, string conversionId, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.ConversionId = conversionId;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/add_conversion", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Removes conversion from an open settlement.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <param name="conversionId">Id of the conversion.</param>
        /// <param name="optional">Optional parameters of the updated settlement.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> RemoveConversionFromSettlementAsync(string id, string conversionId, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            paramsObj.ConversionId = conversionId;
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/remove_conversion", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Moves the settlement to state "released", meaning it is ready to be processed.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <param name="optional">Optional parameters of the updated settlement.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> ReleaseSettlementAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/release", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Moves the settlement to state "open", allowing conversions to be added or removed.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <param name="optional">Optional parameters of the updated settlement.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> UnreleaseSettlementAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/unrelease", HttpMethod.Post, paramsObj);
        }

        #endregion

        #region Transactions

        /// <summary>
        /// Gets details of a transaction.
        /// </summary>
        /// <param name="id">Id of the requested transaction.</param>
        /// <param name="optional">Optional parameters of the requested transaction.</param>
        /// <returns>Asynchronous task, which returns the requested transaction.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Transaction> GetTransactionAsync(string id, dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<Transaction>("/v2/transactions/" + id, HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Finds transactions matching the search criteria for the active user.
        /// </summary>
        /// <param name="optional">Optional parameters of the sought transactions.</param>
        /// <returns>Asynchronous task, which returns the list of the found transactions, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedTransactions> FindTransactionsAsync(dynamic optional = null)
        {
            dynamic paramsObj = new ParamsObject();
            if (optional != null)
            {
                paramsObj.Add(optional);
            }

            return await RequestAsync<PaginatedTransactions>("/v2/transactions/find", HttpMethod.Get, paramsObj);
        }

        #endregion
    }

    internal static class ApiExceptionFactory
    {
        private static Request CreateRequest(HttpRequestMessage requestMessage)
        {
            var query = requestMessage.RequestUri.Query;
            var queryParams = HttpUtility.ParseQueryString(query);

            var parameters = queryParams.Cast<string>().ToDictionary(key => key.ToPascalCase(), value => queryParams[value]);
            var verb = requestMessage.Method.Method;
            var url = requestMessage.RequestUri.OriginalString;

            if (!string.IsNullOrEmpty(query))
            {
                url = url.Replace(query, string.Empty);
            }

            return new Request(parameters, verb, url);
        }

        private static Response CreateResponse(HttpStatusCode statusCode, HttpResponseHeaders responseHeaders)
        {
            IEnumerable<string> values;

            string requestId = string.Empty;
            if (responseHeaders.TryGetValues("X-Request-Id", out values))
            {
                requestId = values.First();
            }

            DateTime date = DateTime.MinValue;
            if (responseHeaders.TryGetValues("Date", out values))
            {
                DateTime.TryParse(values.First(), out date);
            }

            return new Response((int)statusCode, date, requestId);
        }

        private static async Task<List<Error>> CreateErrors(HttpContent content)
        {
            string errorString = await content.ReadAsStringAsync();

            JObject errorObject = JObject.Parse(errorString);

            var errors = from JProperty error in errorObject["error_messages"]
                         select new Error(error.Name,
                            (from errorMessage in error.Value
                             select new Error.ErrorMessage(errorMessage["code"].Value<string>(),
                                                           errorMessage["message"].Value<string>(),
                                                           (from JProperty param in errorMessage["params"]
                                                            select new KeyValuePair<string, string>(param.Name, param.Value.ToString()))
                                                           .ToDictionary(x => x.Key, x => x.Value)))
                            .ToList()
            );

            return errors.ToList();
        }

        public static async Task<ApiException> FromHttpResponse(HttpResponseMessage res)
        {
            var request = CreateRequest(res.RequestMessage);
            var response = CreateResponse(res.StatusCode, res.Headers);
            var errors = await CreateErrors(res.Content);

            switch (response.StatusCode)
            {
                case 400:
                    return new BadRequestException(request, response, errors);
                case 401:
                    return new AuthenticationException(request, response, errors);
                case 403:
                    return new ForbiddenException(request, response, errors);
                case 404:
                    return new NotFoundException(request, response, errors);
                case 429:
                    return new TooManyRequestsException(request, response, errors);
                case 500:
                    return new InternalApplicationException(request, response, errors);
                default:
                    return new UndefinedException(request, response, errors);
            }
        }
    }

    internal class ParamsObject : DynamicObject
    {
        private Dictionary<string, object> storage;

        public ParamsObject()
        {
            storage = new Dictionary<string, object>();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var key = binder.Name.ToSnakeCase();

            return storage.TryGetValue(key, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var key = binder.Name.ToSnakeCase();

            storage[key] = value;

            return true;
        }

        public bool IsEmpty
        {
            get
            {
                return !storage.Any();
            }
        }

        public void Add(dynamic obj)
        {
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                string key = prop.Name;
                object value = prop.GetValue(obj);

                storage.Add(key.ToSnakeCase(), value);
            }
        }

        public string ToQueryString()
        {
            return string.Join("&", storage.Select(param => 
            {
                string key = param.Key;

                if (param.Value is Array)
                {
                    var values = from object item in param.Value as Array
                                 select key + "[]=" + item.ToString();

                    return string.Join("&", values.ToList());
                }

                string value;
                if (param.Value is DateTime)
                {
                    value = ((DateTime)param.Value).ToString("yyyy-MM-dd");
                }
                else if (param.Value is bool)
                {
                    value = param.Value.ToString().ToLower();
                }
                else
                {
                    value = param.Value.ToString();
                }
                return key + "=" + value;
            }));
        }
    }

    internal class PascalContractResolver : DefaultContractResolver
    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);

            if(objectType.GenericTypeArguments[0] == typeof(string))
            {
                contract.Converter = new PascalDictionaryConverter();
            }

            return contract;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.PropertyName = property.PropertyName.ToSnakeCase();

            return property;
        }
    }

    internal class PascalDictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JObject obj = JObject.Load(reader);
            JsonReader objReader = obj.CreateReader();

            objReader.Culture = reader.Culture;
            objReader.DateFormatString = reader.DateFormatString;
            objReader.DateParseHandling = reader.DateParseHandling;
            objReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
            objReader.FloatParseHandling = reader.FloatParseHandling;
            objReader.SupportMultipleContent = reader.SupportMultipleContent;

            dynamic src = Activator.CreateInstance(objectType);
            dynamic res = Activator.CreateInstance(objectType);

            serializer.Populate(objReader, src);

            foreach (var prop in src)
            {
                res.Add((prop.Key as string).ToPascalCase(), prop.Value);
            }

            return res;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
