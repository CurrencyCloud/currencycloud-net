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

using System;
using System.Threading.Tasks;
using CurrencyCloud;
using CurrencyCloud.Exception;
using CurrencyCloud.Environment;

namespace Cookbook
{
    public class CheckBalance
    {
        public static void Run()
        {
            Console.WriteLine("*** Check Balance ***");
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            Retry.Enabled = true;
            Retry.NumRetries = 5;

            var client = new Client();
            var isAuthenticated = false;

            try
            {
                /**
                 * Checking your balances
                 * In this cookbook, you will check how much money you hold in various foreign currencies in your
                 * Currencycloud account.
                 */

                /**
                 * 1. Authenticate using valid credentials.
                 * If you do not have a valid Login ID and API Key, you can get one by registering at
                 * https://www.currencycloud.com/developers/register-for-an-api-key/
                 */
                var token = await client.InitializeAsync(ApiServer.Demo, Credentials.LoginId, Credentials.ApiKey);
                isAuthenticated = true;
                Console.WriteLine("Token: {0}\n", token);

                /**
                 * 2. Check your balance for a specific currency
                 * To find out how many Euros you have, call the Get Balance endpoint, passing EUR as the third URI path parameter.
                 */
                var retrieveBalance = await client.GetBalanceAsync("EUR");
                Console.WriteLine("Retrieve Balance: {0}\n", retrieveBalance.ToJSON());

                /**
                 * To get a balance for any of your client sub-accounts, simply provide the sub-account UUID via the on_behalf_of query
                 * string parameter.
                 */

                /**
                 * 3. Get detailed currency balances
                 * Alternatively, the Find Balances endpoint will tell you the value of all foreign currencies that you hold in your main
                 * Currencycloud account.
                 */
                var findBalances = await client.FindBalancesAsync();
                Console.WriteLine("Find Balances: {0}\n", findBalances.ToJSON());

                /**
                 * To fetch balances for any of your client sub-accounts, simply provide the sub-account UUID via the on_behalf_of query
                 * string parameter.
                 */
            }
            catch (ApiException ex)
            {
                if(ex is AuthenticationException)
                {
                    isAuthenticated = false;
                }

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                /**
                 * 4. Logout
                 * It is good security practice to retire authentication tokens when they are no longer needed, rather than let them
                 * expire. Send a request to the Logout endpoint to terminate an authentication token immediately.
                 */
                if (isAuthenticated)
                {
                    await client.CloseAsync();
                    Console.WriteLine("Logout\n");
                }

                Retry.Enabled = false;
            }
        }
    }
}