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

namespace CurrencyCloud
{
    public class Client
    {
        private Environment environment;
        private string loginId;
        private string apiKey;
        private HttpClient httpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="loginId"></param>
        /// <param name="apiKey"></param>
        /// <returns>Authentication token</returns>
        public async Task<string> InitializeAsync(Environment environment, string loginId, string apiKey)
        {
            this.environment = environment;
            this.loginId = loginId;
            this.apiKey = apiKey;

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(environment.Url);

            dynamic paramsObj = new ParamsObject();
            paramsObj.login_id = loginId;
            paramsObj.api_key = apiKey;

            HttpResponseMessage res = await httpClient.PostAsync("/v2/authenticate/api" + paramsObj.ToQueryString(), null);
            if (res.IsSuccessStatusCode)
            {
                string resString = await res.Content.ReadAsStringAsync();
                var resObjectSchema = new
                {
                    auth_token = ""
                };
                var resObject = JsonConvert.DeserializeAnonymousType(resString, resObjectSchema);

                var token = resObject.auth_token;

                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", token);

                return token;
            }
            else
            {
                throw await ApiExceptionFactory.Create(res);
            }
        }

        public async Task<T> RequestAsync<T>(string path, HttpMethod method, ParamsObject paramsObj = null)
        {
            string requestUri = path;
            if(paramsObj != null)
            {
                requestUri += paramsObj.ToQueryString();
            }

            HttpResponseMessage res = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri));
            if (res.IsSuccessStatusCode)
            {
                string resString = await res.Content.ReadAsStringAsync();
                T resObject = JsonConvert.DeserializeObject<T>(resString);

                return resObject;
            }
            else
            {
                throw await ApiExceptionFactory.Create(res);
            }
        }

        public async Task CloseAsync()
        {
            HttpResponseMessage res = await httpClient.PostAsync("/v2/authenticate/close_session", null);
            if (res.IsSuccessStatusCode)
            {
                environment = null;
                loginId = null;
                apiKey = null;

                httpClient.Dispose();
            }
            else
            {
                throw await ApiExceptionFactory.Create(res);
            }
        }

        #region Accounts

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="legalEntityType"></param>
        /// <param name="optional"></param>
        /// <returns></returns>
        public async Task<Account> CreateAccountAsync(string accountName, string legalEntityType, ParamsObject optional)
        {
            dynamic parameters = optional; //todo clone
            parameters.account_name = accountName;
            parameters.legal_entity_type = legalEntityType;

            return await RequestAsync<Account>("/v2/accounts/create", HttpMethod.Post, parameters);
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

    }

    internal static class ApiExceptionFactory
    {
        private static Request CreateRequest(HttpRequestMessage requestMessage)
        {
            var query = requestMessage.RequestUri.Query;

            var parameters = HttpUtility.ParseQueryString(query);
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
            //((JObject)errorObject)["error_messages"]

            return null;
        }

        public static async Task<ApiException> Create(HttpResponseMessage res)
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

    /// <summary>
    /// Represents dynamic parameters object.
    /// </summary>
    public class ParamsObject : DynamicObject
    {
        private Dictionary<string, object> storage = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return storage.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            storage[binder.Name] = value;

            return true;
        }

        //public ParamsObject Clone()
        //{
        //    return new Dictionary<string, object>(storage);
        //}

        public string ToQueryString()
        {
            return "?" + string.Join("&", storage.Select(param => HttpUtility.UrlEncode(param.Key) + "=" + param.Value));
        }
    }

    /// <summary>
    /// Represents environment to run API calls against.
    /// </summary>
    public class Environment
    {
        private Environment(string url)
        {
            Url = url;
        }

        public readonly string Url;

        public static readonly Environment Demo = new Environment("https://devapi.thecurrencycloud.com");
        public static readonly Environment Production = new Environment("https://api.thecurrencycloud.com");
        public static readonly Environment UAT = new Environment("https://api-uat1.ccycloud.com");
    } 
}
