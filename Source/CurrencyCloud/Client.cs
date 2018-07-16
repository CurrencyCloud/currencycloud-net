using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
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

[assembly: InternalsVisibleTo("Currencycloud.Tests")]

namespace CurrencyCloud
{
    /// <summary>
    /// Represents API client
    /// </summary>
    public class Client
    {
        private HttpClient httpClient;
        private Credentials credentials;
        private string onBehalfOf;
        private string userAgent = "CurrencyCloudSDK/2.0 .NET/2.2.1";

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
            string requestUri = string.Format("/v2/authenticate/api");

            ParamsObject authParams = new ParamsObject();
            authParams.Add("login_id", credentials.LoginId);
            authParams.Add("api_key", credentials.ApiKey);

            HttpRequestMessage httpAuthRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = authParams.buildFormUrlBodyFromParams()
            };
            HttpResponseMessage res = await httpClient.SendAsync(httpAuthRequestMessage);
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

        private async Task<TResult> RequestAsync<TResult>(string path, HttpMethod method, ParamsObject obj = null)
        {
            if (httpClient == null)
            {
                throw new InvalidOperationException("Client is not initialized.");
            }

            var paramsObj = new ParamsObject();
            if(obj != null)
            {
                paramsObj += obj;
            }

            paramsObj.AddNotNull("OnBehalfOf", onBehalfOf);

            string requestUri = path;
            if (paramsObj.Count > 0)
            {
                requestUri += "?" + paramsObj.ToQueryString();
            }

            Func<Task<TResult>> requestAsyncDelegate = async () =>
            {
                Debug.WriteLine(httpClient.BaseAddress + " " + method.Method + " -> " + requestUri);
                HttpRequestMessage httpRequestMessage = null;
                if (method == HttpMethod.Get)
                {
                    httpRequestMessage = new HttpRequestMessage(method, requestUri);
                } else {
                    httpRequestMessage = new HttpRequestMessage(method, path)
                    {
                        Content = paramsObj.buildFormUrlBodyFromParams()
                    };
                }
                HttpResponseMessage res = await httpClient.SendAsync(httpRequestMessage);
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

        private Task RequestAsync(string path, HttpMethod method, ParamsObject obj = null)
        {
            return RequestAsync<object>(path, method, obj);
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
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);

            httpClient.BaseAddress = new Uri(apiServer.Url);

            credentials = new Credentials(loginId,apiKey);

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
        /// <param name="account">Account object</param>
        /// <returns>Asynchronous task, which returns newly created account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Account> CreateAccountAsync(Account account)
        {
            var paramsObj = ParamsObject.CreateFromStaticObject(account);

            return await RequestAsync<Account>("/v2/accounts/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of an account.
        /// </summary>
        /// <param name="id">Id of the requested account.</param>
        /// <returns>Asynchronous task, which returns the requested account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Account> GetAccountAsync(string id)
        {
            return await RequestAsync<Account>("/v2/accounts/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Updates an existing account.
        /// </summary>
        /// <param name="account">Account object to be updated</param>
        /// <returns>Asynchronous task, which returns the updated account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Account> UpdateAccountAsync(Account account)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(account);
            string id = account.Id;
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Account id can not be null");

            return await RequestAsync<Account>("/v2/accounts/" + id, HttpMethod.Post, optional);
        }

        /// <summary>
        /// Finds accounts matching the search criteria for the active user.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the list of the found accounts, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedAccounts> FindAccountsAsync(AccountFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedAccounts>("/v2/accounts/find", HttpMethod.Get, optional);
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
        /// <returns>Asynchronous task, which returns the requested balance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Balance> GetBalanceAsync(string currency)
        {
            return await RequestAsync<Balance>("/v2/balances/" + currency, HttpMethod.Get, null);
        }

        /// <summary>
        /// Finds balances matching the search criteria.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the list of the found balances, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedBalances> FindBalancesAsync(BalanceFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedBalances>("/v2/balances/find", HttpMethod.Get, optional);
        }

        #endregion

        #region Beneficiaries

        /// <summary>
        /// Validates beneficiary details without creating one.
        /// </summary>
        /// <param name="beneficiary">Beneficiary data to be validated</param>
        /// <returns>Asynchronous task, which returns the validated beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> ValidateBeneficiaryAsync(Beneficiary beneficiary)
        {
            var paramsObj = ParamsObject.CreateFromStaticObject(beneficiary);

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/validate", HttpMethod.Post, paramsObj);
        }

        [Obsolete("Method ValidateBeneficiaryAsync(BeneficiaryValidateParameters) is deprecated. Use ValidateBeneficiaryAsync(Beneficiary) instead", false)]
        public async Task<Beneficiary> ValidateBeneficiaryAsync(BeneficiaryValidateParameters validateParameters)
        {
            var paramsObj = ParamsObject.CreateFromStaticObject(validateParameters);

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/validate", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Creates a new beneficiary.
        /// </summary>
        /// <param name="beneficiary">Beneficiary object to be created</param>
        /// <returns>Asynchronous task, which returns newly created beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> CreateBeneficiaryAsync(Beneficiary beneficiary)
        {
            var paramsObj = ParamsObject.CreateFromStaticObject(beneficiary);

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a beneficiary.
        /// </summary>
        /// <param name="id">Id of the requested beneficiary.</param>
        /// <returns>Asynchronous task, which returns the requested beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> GetBeneficiaryAsync(string id)
        {

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Updates an existing beneficiary.
        /// </summary>
        /// <param name="beneficiary">Beneficiary object to be updated</param>
        /// <returns>Asynchronous task, which returns the updated beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        /// <exception cref="ArgumentException">Thrown if Beneficiary.Id is NULL</exception>
        public async Task<Beneficiary> UpdateBeneficiaryAsync(Beneficiary beneficiary)
        {
            string id = beneficiary.Id;
            if (id == null)
                throw new ArgumentException("beneficiary.Id can not be null");

            var optional = ParamsObject.CreateFromStaticObject(beneficiary);

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/" + id, HttpMethod.Post, optional);
        }

        /// <summary>
        /// Finds beneficiaries matching the search criteria for the active user.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the list of the found beneficiaries, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedBeneficiaries> FindBeneficiariesAsync(BeneficiaryFindParameters parameters = null)
        {
            var optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedBeneficiaries>("/v2/beneficiaries/find", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Deletes an existing beneficiary.
        /// </summary>
        /// <param name="id">Id of the deleted beneficiary.</param>
        /// <returns>Asynchronous task, which returns the deleted beneficiary.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Beneficiary> DeleteBeneficiaryAsync(string id)
        {
            var optional = new ParamsObject();

            return await RequestAsync<Beneficiary>("/v2/beneficiaries/" + id + "/delete", HttpMethod.Post, optional);
        }

        #endregion

        #region Contacts

        /// <summary>
        /// Creates a new contact.
        /// </summary>
        /// <param name="contact">Contact object to be created</param>
        /// <returns>Asynchronous task, which returns newly created contact.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            var paramsObj = ParamsObject.CreateFromStaticObject(contact);

            return await RequestAsync<Contact>("/v2/contacts/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a contact.
        /// </summary>
        /// <param name="id">Id of the requested contact.</param>
        /// <returns>Asynchronous task, which returns the requested contact.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Contact> GetContactAsync(string id)
        {
            return await RequestAsync<Contact>("/v2/contacts/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="contact">Contact object to be updated</param>
        /// <returns>Asynchronous task, which returns the updated contact.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        /// <exception cref="ArgumentException">Thrown if Contact.Id is NULL</exception>
        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            string id = contact.Id;
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Contact.Id can not be null");

            ParamsObject optional = ParamsObject.CreateFromStaticObject(contact);
            //remove account_id. Not required by server while update.
            optional.Remove("AccountId");

            return await RequestAsync<Contact>("/v2/contacts/" + id, HttpMethod.Post, optional);
        }

        /// <summary>
        /// Finds contacts matching the search criteria for the active user.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the list of the found contacts, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedContacts> FindContactsAsync(ContactFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedContacts>("/v2/contacts/find", HttpMethod.Get, optional);
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
        /// <param name="conversion">Data object for new conversion</param>
        /// <returns>Asynchronous task, which returns newly created conversion.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Conversion> CreateConversionAsync(Conversion conversion)
        {
            var paramsObj = ParamsObject.CreateFromStaticObject(conversion);

            return await RequestAsync<Conversion>("/v2/conversions/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a conversion.
        /// </summary>
        /// <param name="id">Id of the requested conversion.</param>
        /// <returns>Asynchronous task, which returns the requested conversion.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Conversion> GetConversionAsync(string id)
        {
            return await RequestAsync<Conversion>("/v2/conversions/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Finds conversions matching the search criteria for the active user.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the list of the found conversions, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedConversions> FindConversionsAsync(ConversionFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedConversions>("/v2/conversions/find", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Cancels the conversion identified by the provided unique id.
        /// </summary>
        /// <param name="id">Id of the conversion that is being cancelled</param>
        /// <param name="notes">Notes describing the reason for cancellation</param>
        /// <returns>Asynchronous task, which returns the details of the cancelled conversion</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<ConversionCancellation> CancelConversionsAsync(string id, string notes = null)
        {
            ParamsObject paramsObj = new ParamsObject();
            paramsObj.AddNotNull("notes", notes);

            return await RequestAsync<ConversionCancellation>("/v2/conversions/" + id + "/cancel", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Changes the date ofthe conversion identified by the provided unique id.
        /// </summary>
        /// <param name="id">Id of the conversion that is being changed</param>
        /// <param name="newSettlementDate">New conversion settlement date</param>
        /// <returns>Asynchronous task, which returns the details of the conversion date change</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<ConversionDateChange> DateChangeConversionAsync(string id, DateTime newSettlementDate)
        {
            ParamsObject paramsObj = new ParamsObject();
            paramsObj.Add("NewSettlementDate", newSettlementDate);

            return await RequestAsync<ConversionDateChange>("/v2/conversions/" + id + "/date_change", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Splits a conversion.
        /// </summary>
        /// <param name="id">Id of the conversion that is being splt</param>
        /// <param name="amount">The amount at which to split this conversion</param>
        /// <returns>Asynchronous task, which returns the details of the split conversion</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<ConversionSplit> SplitConversionsAsync(string id, decimal amount)
        {
            ParamsObject paramsObj = new ParamsObject();
            paramsObj.Add("amount", amount);

            return await RequestAsync<ConversionSplit>("/v2/conversions/" + id + "/split", HttpMethod.Post, paramsObj);
        }

        #endregion

        #region Ibans

        /// <summary>
        /// Find IBANs assigned to the logged in account.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns structure containing the details of the IBAN assigned to the logged in account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedIbans> FindIbansAsync(IbanFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedIbans>("/v2/ibans", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Find IBANs of the sub-accounts linked to the logged in account.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns structure containing the details of the IBAN assigned to the sub-accounts.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedIbans> FindSubAccountsIbansAsync(IbanFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedIbans>("/v2/ibans/subaccounts/find", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Retrieve IBAN of the sub-account.
        /// </summary>
        /// <param name="id">Sub-Account ID</param>
        /// <returns>Asynchronous task, which returns structure containing the details of the IBAN assigned to the sub-accounts.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedIbans> GetSubAccountsIbansAsync(string id)
        {
            return await RequestAsync<PaginatedIbans>("/v2/ibans/subaccounts/" + id, HttpMethod.Get, null);
        }

        #endregion

        #region Payers

        /// <summary>
        /// Gets details of a payer.
        /// </summary>
        /// <param name="id">Id of the requested payer.</param>
        /// <returns>Asynchronous task, which returns the requested payer.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payer> GetPayerAsync(string id)
        {
            return await RequestAsync<Payer>("/v2/payers/" + id, HttpMethod.Get, null);
        }

        #endregion

        #region Payments

        /// <summary>
        /// Creates a new payment.
        /// </summary>
        /// <param name="payment">Payment object to be created</param>
        /// <param name="payer">Optional payer info</param>
        /// <returns>Asynchronous task, which returns newly created payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        /// <remarks>Payment.PayerDetailsSource not passed to server while creation</remarks>
        public async Task<Payment> CreatePaymentAsync(Payment payment, Payer payer = null)
        {
            ParamsObject paramsObj = ParamsObject.CreateFromStaticObject(payment);
            if (payer != null)
            {
                paramsObj.AddNotNull("PayerEntityType", payer.LegalEntityType);
                paramsObj.AddNotNull("PayerCompanyName", payer.CompanyName);
                paramsObj.AddNotNull("PayerFirstName", payer.FirstName);
                paramsObj.AddNotNull("PayerLastName", payer.LastName);
                paramsObj.AddNotNull("PayerCity", payer.City);
                paramsObj.AddNotNull("PayerAddress", payer.Address);
                paramsObj.AddNotNull("PayerPostcode", payer.Postcode);
                paramsObj.AddNotNull("PayerStateOrProvince", payer.StateOrProvince);
                paramsObj.AddNotNull("PayerCountry", payer.Country);
                paramsObj.AddNotNull("PayerDateOfBirth", payer.DateOfBirth);
                paramsObj.AddNotNull("PayerIdentificationType", payer.IdentificationType);
                paramsObj.AddNotNull("PayerIdentificationValue", payer.IdentificationValue);
            }

            return await RequestAsync<Payment>("/v2/payments/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a payment.
        /// </summary>
        /// <param name="id">Id of the requested payment.</param>
        /// <returns>Asynchronous task, which returns the requested payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payment> GetPaymentAsync(string id)
        {

            return await RequestAsync<Payment>("/v2/payments/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Updates an existing payment.
        /// </summary>
        /// <param name="payment">Payment object to be updated</param>
        /// <param name="payer">Optional payer data to be updated for payment</param>
        /// <returns>Asynchronous task, which returns the updated payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        /// <exception cref="ArgumentException">Thrown when Payment.Id is NULL</exception>
        public async Task<Payment> UpdatePaymentAsync(Payment payment, Payer payer = null)
        {
            string id = payment.Id;
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Payment.ID can not be null");

            ParamsObject optional = ParamsObject.CreateFromStaticObject(payment);
            optional.AddNotNull("PayerDetailsSource", payment.PayerDetailsSource);
            if (payer != null)
            {
                optional.AddNotNull("PayerEntityType", payer.LegalEntityType);
                optional.AddNotNull("PayerCompanyName", payer.CompanyName);
                optional.AddNotNull("PayerFirstName", payer.FirstName);
                optional.AddNotNull("PayerLastName", payer.LastName);
                optional.AddNotNull("PayerCity", payer.City);
                optional.AddNotNull("PayerAddress", payer.Address);
                optional.AddNotNull("PayerPostcode", payer.Postcode);
                optional.AddNotNull("PayerStateOrProvince", payer.StateOrProvince);
                optional.AddNotNull("PayerCountry", payer.Country);
                optional.AddNotNull("PayerDateOfBirth", payer.DateOfBirth);
                optional.AddNotNull("PayerIdentificationType", payer.IdentificationType);
                optional.AddNotNull("PayerIdentificationValue", payer.IdentificationValue);
            }
            return await RequestAsync<Payment>("/v2/payments/" + id, HttpMethod.Post, optional);
        }

        /// <summary>
        /// Finds payments matching the search criteria for the active user.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns  the list of the found payments, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedPayments> FindPaymentsAsync(FindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedPayments>("/v2/payments/find", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Deletes an existing payment.
        /// </summary>
        /// <param name="id">Id of the deleted payment.</param>
        /// <returns>Asynchronous task, which returns the deleted payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Payment> DeletePaymentAsync(string id)
        {
            return await RequestAsync<Payment>("/v2/payments/" + id + "/delete", HttpMethod.Post, null);
        }

        /// <summary>
        /// Returns a hash containing the details of MT103 information for a SWIFT payments.
        /// </summary>
        /// <param name="id">Id payment.</param>
        /// <returns>Asynchronous task, which returns the MT103 information for a SWIFT payment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaymentSubmission> GetPaymentSubmissionAsync(string id)
        {
            return await RequestAsync<PaymentSubmission>("/v2/payments/" + id + "/submission", HttpMethod.Get, null);
        }

        #endregion

        #region Rates

        /// <summary>
        /// Gets a full quote for the requested currency based on the spread table of the active contact.
        /// </summary>
        /// <param name="detailedRates">Rate parameters object</param>
        /// <returns>Asynchronous task, which returns the requested rate.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Rate> GetRateAsync(DetailedRates detailedRates)
        {
            ParamsObject paramsObj = ParamsObject.CreateFromStaticObject(detailedRates);

            return await RequestAsync<Rate>("/v2/rates/detailed", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets core rate information for multiple currency pairs.
        /// </summary>
        /// <param name="currencyPair">Currency pair</param>
        /// <param name="ignoreInvalidPairs">Optional: Ignore invalid pairs</param>
        /// <returns>Asynchronous task, which returns the list of the found rates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<RatesList> FindRatesAsync(string currencyPair, bool? ignoreInvalidPairs = null)
        {
            ParamsObject paramsObj = new ParamsObject();
            paramsObj.Add("CurrencyPair",currencyPair);
            paramsObj.AddNotNull("IgnoreInvalidPairs", ignoreInvalidPairs);


            return await RequestAsync<RatesList>("/v2/rates/find", HttpMethod.Get, paramsObj);
        }

        #endregion

        #region Reference

        /// <summary>
        /// Gets required beneficiary details and their basic validation formats.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="bankAccountCountry">Optional: Bank account country</param>
        /// <param name="beneficiaryCountry">Optional: Beneficiary country</param>
        /// <returns>Asynchronous task, which returns the list of the required beneficiary details.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<BeneficiaryDetailsList> GetBeneficiaryRequiredDetailsAsync(string currency = null, string bankAccountCountry = null, string beneficiaryCountry = null)
        {
            ParamsObject optional = null;
            if (!string.IsNullOrEmpty(currency)
                || !string.IsNullOrEmpty(bankAccountCountry)
                || !string.IsNullOrEmpty(beneficiaryCountry)
                )
            {
                optional = new ParamsObject();
                optional.AddNotNull("Currency", currency);
                optional.AddNotNull("BankAccountCountry", bankAccountCountry);
                optional.AddNotNull("BeneficiaryCountry", beneficiaryCountry);
            }

            return await RequestAsync<BeneficiaryDetailsList>("/v2/reference/beneficiary_required_details", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Gets dates for which the given currency pair can not be traded.
        /// </summary>
        /// <param name="conversionPair">Currency conversion pair.</param>
        /// <param name="startDate">Optional: start date</param>
        /// <returns>Asynchronous task, which returns the list of the conversion dates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<ConversionDatesList> GetConversionDatesAsync(string conversionPair, DateTime? startDate = null)
        {
            var paramsObj = new ParamsObject();
            paramsObj.Add("ConversionPair", conversionPair);
            paramsObj.AddNotNull("StartDate", startDate);

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
        /// Gets a list of purpose codes for a given currency.
        /// </summary>
        /// <param name="currency">Currency to get the purpose codes for</param>
        /// <param name="entityType">Optional: entity (individual or company)</param>
        /// <returns>Asynchronous task, which returns the list purpose codes.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PurposeCodesList> GetPaymentPurposeCodes(string currency, string entityType = null)
        {
            var paramsObj = new ParamsObject();
            paramsObj.Add("Currency", currency);
            paramsObj.AddNotNull("EntityType", entityType);

            return await RequestAsync<PurposeCodesList>("/v2/reference/payment_purpose_codes", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets dates for which the given currency can not be paid.
        /// </summary>
        /// <param name="currency">Currency name.</param>
        /// <param name="startDate">Start date</param>
        /// <returns>Asynchronous task, which returns the list of the payment dates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaymentDatesList> GetPaymentDatesAsync(string currency, DateTime? startDate = null)
        {
            var paramsObj = new ParamsObject();
            paramsObj.Add("Currency", currency);
            paramsObj.AddNotNull("StartDate", startDate);


            return await RequestAsync<PaymentDatesList>("/v2/reference/payment_dates", HttpMethod.Get, paramsObj);
        }

        /// <summary>
        /// Gets settlement account information, detailing where funds need to be sent to.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <returns>Asynchronous task, which returns the list of the found rates.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<SettlementAccountsList> GetSettlementAccountsAsync(string currency = null)
        {
            ParamsObject optional = null;
            if (!string.IsNullOrEmpty(currency))
            {
                optional = new ParamsObject();
                optional.Add("Currency", currency);
            }

            return await RequestAsync<SettlementAccountsList>("/v2/reference/settlement_accounts", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Gets required payer details and their basic validation formats.
        /// </summary>
        /// <param name="payerCountry">ISO 3166-1 country code</param>
        /// <param name="payerEntityType">Optional: Payer Entity Type (could be company or individual)</param>
        /// <param name="paymentType">Optional: Payment Type (could be priority or regular)</param>
        /// <returns>Asynchronous task, which returns required payer details and their basic validation formats.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PayerDetailsList> GetPayerRequiredDetailsAsync(string payerCountry, string payerEntityType = null, string paymentType = null)
        {
            ParamsObject optional = null;
            if (!string.IsNullOrEmpty(payerEntityType)
                || !string.IsNullOrEmpty(paymentType)
            )
            {
                optional = new ParamsObject();
                optional.AddNotNull("PayerEntityType", payerEntityType);
                optional.AddNotNull("PaymentType", paymentType);
            }

            var paramsObj = new ParamsObject();
            paramsObj.Add("PayerCountry", payerCountry);
            paramsObj.AddNotNull("PayerEntityType", payerEntityType);
            paramsObj.AddNotNull("PaymentType", paymentType);

            return await RequestAsync<PayerDetailsList>("/v2/reference/payer_required_details", HttpMethod.Get, paramsObj);
        }

        #endregion

        #region Settlements

        /// <summary>
        /// Creates a new settlement.
        /// </summary>
        /// <param name="type">net (also: bulk)</param>
        /// <returns>Asynchronous task, which returns newly created settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> CreateSettlementAsync(string type = null)
        {
            ParamsObject optional = null;
            if (!string.IsNullOrEmpty(type))
            {
                optional = new ParamsObject();
                optional.Add("Type", type);
            }

            return await RequestAsync<Settlement>("/v2/settlements/create", HttpMethod.Post, optional);
        }

        /// <summary>
        /// Gets details of a settlement.
        /// </summary>
        /// <param name="id">Id of the requested settlement.</param>
        /// <returns>Asynchronous task, which returns the requested settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> GetSettlementAsync(string id)
        {
            return await RequestAsync<Settlement>("/v2/settlements/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Finds settlements matching the search criteria for the active user.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns  the list of the found settlements, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedSettlements> FindSettlementsAsync(SettlementFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedSettlements>("/v2/settlements/find", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Deletes an existing settlement.
        /// </summary>
        /// <param name="id">Id of the deleted settlement.</param>
        /// <returns>Asynchronous task, which returns the deleted settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> DeleteSettlementAsync(string id)
        {
            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/delete", HttpMethod.Post, null);
        }

        /// <summary>
        /// Adds conversion to an open settlement.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <param name="conversionId">Id of the conversion.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> AddConversionToSettlementAsync(string id, string conversionId)
        {
            var paramsObj = new ParamsObject();
            paramsObj.Add("ConversionId", conversionId);

            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/add_conversion", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Removes conversion from an open settlement.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <param name="conversionId">Id of the conversion.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> RemoveConversionFromSettlementAsync(string id, string conversionId)
        {
            var paramsObj = new ParamsObject();
            paramsObj.Add("ConversionId", conversionId);


            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/remove_conversion", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Moves the settlement to state "released", meaning it is ready to be processed.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> ReleaseSettlementAsync(string id)
        {
            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/release", HttpMethod.Post, null);
        }

        /// <summary>
        /// Moves the settlement to state "open", allowing conversions to be added or removed.
        /// </summary>
        /// <param name="id">Id of the settlement.</param>
        /// <returns>Asynchronous task, which returns the updated settlement.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Settlement> UnreleaseSettlementAsync(string id)
        {
            return await RequestAsync<Settlement>("/v2/settlements/" + id + "/unrelease", HttpMethod.Post, null);
        }

        #endregion

        #region Transactions

        /// <summary>
        /// Gets details of a transaction.
        /// </summary>
        /// <param name="id">Id of the requested transaction.</param>
        /// <returns>Asynchronous task, which returns the requested transaction.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Transaction> GetTransactionAsync(string id)
        {
            return await RequestAsync<Transaction>("/v2/transactions/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Finds transactions matching the search criteria for the active user.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the list of the found transactions, as well as pagination information.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedTransactions> FindTransactionsAsync(TransactionFindParameters parameters = null)
        {
            ParamsObject optional = parameters == null ? null : ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedTransactions>("/v2/transactions/find", HttpMethod.Get, optional);
        }

        #endregion

        #region Transfers

        /// <summary>
        /// Creates a new Transfer.
        /// </summary>
        /// <param name="transfer">Data object for new Transfer</param>
        /// <returns>Asynchronous task, which returns newly created conversion.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Transfer> CreateTransferAsync(Transfer transfer)
        {
            var paramsObj = ParamsObject.CreateFromStaticObject(transfer);

            return await RequestAsync<Transfer>("/v2/transfers/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of a transfer.
        /// </summary>
        /// <param name="id">Id of the requested conversion.</param>
        /// <returns>Asynchronous task, which returns the requested conversion.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<Transfer> GetTransferAsync(string id)
        {
            return await RequestAsync<Transfer>("/v2/transfers/" + id, HttpMethod.Get, null);
        }

        /// <summary>
        /// Find all Transfers matching the given search criteria
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns structure containing the details of the IBAN assigned to the logged in account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedTransfers> FindTransfersAsync(TransferFindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedTransfers>("/v2/transfers/find", HttpMethod.Get, optional);
        }

        #endregion

        #region VirtualAccounts

        /// <summary>
        /// Find Virtual Accounts assigned to the logged in account.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the details of the Virtual Accounts assigned to the logged in account.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedVirtualAccounts> FindVirtualAccountsAsync(FindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedVirtualAccounts>("/v2/virtual_accounts", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Find Virtual Accounts of the sub-accounts linked to the logged in account.
        /// </summary>
        /// <param name="parameters">Find parameters</param>
        /// <returns>Asynchronous task, which returns the details of the Virtual Accounts assigned to the sub-accounts.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedVirtualAccounts> FindSubAccountsVirtualAccountsAsync(FindParameters parameters = null)
        {
            ParamsObject optional = ParamsObject.CreateFromStaticObject(parameters);

            return await RequestAsync<PaginatedVirtualAccounts>("/v2/virtual_accounts/subaccounts/find", HttpMethod.Get, optional);
        }

        /// <summary>
        /// Retrieve Virtual Accounts of the sub-account.
        /// </summary>
        /// <param name="id">Sub-Account ID</param>
        /// <returns>Asynchronous task, which returns the details of the Virtual Accounts assigned to the sub-accounts.</returns>
        /// <exception cref="InvalidOperationException">Thrown when client is not initialized.</exception>
        /// <exception cref="ApiException">Thrown when API call fails.</exception>
        public async Task<PaginatedVirtualAccounts> GetSubAccountVirtualAccountsAsync(string id)
        {
            return await RequestAsync<PaginatedVirtualAccounts>("/v2/virtual_accounts/subaccounts/" + id, HttpMethod.Get, null);
        }

        #endregion
    }

    internal static class ApiExceptionFactory
    {
        private static Request CreateRequest(HttpRequestMessage requestMessage)
        {
            var query = requestMessage.RequestUri.Query;
            var queryParams = HttpUtility.ParseQueryString(query);

            var parameters = queryParams.Cast<string>().ToDictionary(key => key, value => queryParams[value]);
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
