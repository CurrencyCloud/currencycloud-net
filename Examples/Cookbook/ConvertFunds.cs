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
using CurrencyCloud.Entity;
using CurrencyCloud.Exception;
using CurrencyCloud.Environment;

namespace Cookbook
{
    public class ConvertFunds
    {
        public static void Run()
        {
            Console.WriteLine("*** Convert Funds ***");
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
                 * Convert funds from one currency to another
                 *  A conversion is a process whereby money held in one currency is traded for money in another currency. Currencycloud can
                 * convert money into currencies of all the world’s major economies.
                 *
                 * In this cookbook, you will:
                 *
                 * 1. Get a quote for trading Pound Sterling (GBP) for Euros (EUR).
                 * 2. Top up your Euros balance by trading some Pound Sterling.
                 */

                /**
                 * 1. Authenticate using valid credentials.
                 * If you do not have a valid Login ID and API Key, you can get one by registering at
                 * https://www.currencycloud.com/developers/register-for-an-api-key/
                 */
                var token = await client.InitializeAsync(ApiServer.Demo, Credentials.LoginId, Credentials.ApiKey);
                isAuthenticated = true;
                Console.WriteLine("Token: {0}", token);

                /**
                 * 2. Get a detailed quote
                 * Check how much it will cost to buy 10,000 Euros using funds from your Pound Sterling balance, by making a call to the
                 * Get Detailed Rates endpoint
                 */
                var detailedRates = await client.GetRateAsync(new DetailedRates
                {
                    BuyCurrency = "EUR",
                    SellCurrency = "GBP",
                    FixedSide = "buy",
                    Amount = 10000
                });
                Console.WriteLine("Detailed Rates: {0}\n", detailedRates.ToJSON());

                /**
                 * On success, the response payload will contain details of Currencycloud’s quotation to make the conversion.
                 */

                /**
                 * If you’re happy with the quote, you may create the conversion by calling the Create Conversion endpoint.
                 */
                var createConversion = await client.CreateConversionAsync(new Conversion("EUR", "GBP", "buy", 10000, true));
                Console.WriteLine("Create Conversion: {0}\n", createConversion.ToJSON());

                /**
                 * On success, the payload of the response message will contain full details of the conversion as recorded against your
                 * Currencycloud account.
                 *
                 * This conversion will settle automatically on the settlement_date as long as there are sufficient funds in the account’s
                 * GBP balance to cover the client_sell_amount. Please use your Cash Manager to top up your GBP balance if necessary.
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
                 * 3. Logout
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