/**
 * This is a C# implementation of the Currencycloud API v2.0 Cookbook example available at
 * https://www.currencycloud.com/developers/cookbooks/
 *
 * Additional documentation for each API endpoint can be found at
 * https://www.currencycloud.com/developers/overview
 *
 * If you have any queries or you require support, please contact our Support team at
 * support@currencycloud.com
 */

namespace Cookbook
{
    public static class Credentials
    {
        //ToDo: Replace LoginId and ApiKey with valid credentials
        public const string LoginId = "development@currencycloud.com";
        public const string ApiKey = "deadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeef";
    }

    class Program
    {
        static void Main()
        {
            CheckBalance.Run();
            CheckRate.Run();
            ConvertFunds.Run();
            PayBeneficiary.Run();
            SimplePayment.Run();
        }
    }
}