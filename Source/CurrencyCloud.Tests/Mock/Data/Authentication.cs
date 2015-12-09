using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Authentication
    {
        public static readonly dynamic Credentials = new
        {
            ApiServer = ApiServer.Mock,
            LoginId = "test.it@mailinator.com",
            APIkey = "b5266326b1855443544626f188b8a234da99e1c36d91819419e17091b4f0a7f4"
        };
    }
}