using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Authentication
    {
        public static readonly Credentials Credentials = new Credentials(
            "test.it@mailinator.com",
            "b5266326b1855443544626f188b8a234da99e1c36d91819419e17091b4f0a7f4"
        );

        public static readonly ApiServer ApiServer = ApiServer.Mock;

}
}