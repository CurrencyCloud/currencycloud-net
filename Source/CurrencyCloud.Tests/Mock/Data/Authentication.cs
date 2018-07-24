using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Authentication
    {
        public static readonly Credentials Credentials = new Credentials(
            "development@currencycloud.com",
            "deadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeef"
        );
        
        public static readonly Credentials AuthorisePaymentCredentials = new Credentials(
            "development_authorisor@currencycloud.com",
            "deadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeef"
        );
        

        public static readonly ApiServer ApiServer = ApiServer.Mock;

}
}
