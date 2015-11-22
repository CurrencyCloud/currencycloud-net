using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using CurrencyCloud.Exception;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using CurrencyCloud.Entity;
using Newtonsoft.Json;
using CurrencyCloud.Extensions;
using CurrencyCloud.Environment;

namespace CurrencyCloud
{
    public class Client
    {
        private ApiServer apiServer;
        private string loginId;
        private string apiKey;
        private HttpClient httpClient;

        private async Task<T> RequestAsync<T>(string path, HttpMethod method, ParamsObject paramsObj = null)
        {
            string requestUri = path;
            if (paramsObj != null)
            {
                requestUri += paramsObj.ToQueryString();
            }

            HttpResponseMessage res = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri));
            if (res.IsSuccessStatusCode)
            {
                string resString = await res.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(resString);
            }
            else
            {
                throw await ApiExceptionFactory.FromHttpResponse(res);
            }
        }

        private Task RequestAsync(string path, HttpMethod method, ParamsObject paramsObj = null)
        {
            return RequestAsync<object>(path, method, paramsObj);
        }

        #region Authentication

        /// <summary>
        /// Generates an authentication token for the API user.
        /// </summary>
        /// <param name="apiServer">API server to make requests against</param>
        /// <param name="loginId">Login id of the API user</param>
        /// <param name="apiKey">API key of the API user</param>
        /// <returns>Authentication token</returns>
        /// <exception cref="CurrencyCloud.Exception.ApiException"/>
        public async Task<string> LoginAsync(ApiServer apiServer, string loginId, string apiKey)
        {
            this.apiServer = apiServer;
            this.loginId = loginId;
            this.apiKey = apiKey;

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiServer.Url);

            dynamic credentials = new
            {
                LoginId = loginId,
                ApiKey = apiKey
            };
            dynamic paramsObj = new ParamsObject(credentials);

            JObject res = await RequestAsync("/v2/authenticate/api", HttpMethod.Post, paramsObj);

            var token = res["auth_token"].Value<string>();
            httpClient.DefaultRequestHeaders.Add("X-Auth-Token", token);

            return token;
        }

        /// <summary>
        /// Closes current session.
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            await RequestAsync("/v2/authenticate/api", HttpMethod.Post);

            apiServer = null;
            loginId = null;
            apiKey = null;

            httpClient.Dispose();
        }

        #endregion

        #region Accounts

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="legalEntityType"></param>
        /// <param name="optional"></param>
        /// <returns></returns>
        public async Task<Account> CreateAccountAsync(string accountName, string legalEntityType, dynamic optional)
        {
            dynamic paramsObj = new ParamsObject(optional);
            paramsObj.AccountName = accountName;
            paramsObj.LegalEntityType = legalEntityType;

            return await RequestAsync<Account>("/v2/accounts/create", HttpMethod.Post, paramsObj);
        }

        /// <summary>
        /// Gets details of the active account.
        /// </summary>
        /// <returns></returns>
        public async Task<Account> GetCurrentAccountAsync()
        {
            return await RequestAsync<Account>("/v2/accounts/current", HttpMethod.Get);
        }

        #endregion

        #region Balances
        #endregion

        #region Beneficiaries
        #endregion

        #region Contacts
        #endregion

        #region Conversions
        #endregion

        #region Payers
        #endregion

        #region Payments
        #endregion

        #region Rates
        #endregion

        #region Reference
        #endregion

        #region Settlements
        #endregion

        #region Transactions
        #endregion
    }

    internal class ParamsObject : DynamicObject
    {
        private Dictionary<string, object> storage;

        public ParamsObject()
        {
            storage = new Dictionary<string, object>();
        }

        public ParamsObject(dynamic obj) : this()
        {
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                string key = prop.Name;
                object value = prop.GetValue(obj);

                storage.Add(key.ToSnakeCase(), value);
            }
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

        public string ToQueryString()
        {
            return "?" + string.Join("&", storage.Select(param => param.Key + "=" + param.Value.ToString()));
        }
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

            if(!string.IsNullOrEmpty(query))
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
                                                            select new KeyValuePair<string, object>(param.Name.ToPascalCase(), (param.Value as JValue).Value))
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
}
