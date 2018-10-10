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
    public class CheckRate
    {
        public static void Run()
        {
            Console.WriteLine("*** Check Rate ***");
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
                 * Check foreign exchange rates
                 * A foreign exchange (FX) rate is a rate at which one currency is exchanged for another. For example, an exchange rate of
                 * 114 Japanese Yen to the US Dollar means that ¥114 can be bought for US$1, or US$1 can be bought for ¥114.
                 *
                 * Currencycloud provides two web services for checking foreign exchange rates. In this cookbook, you will:
                 *
                 * 1. Use Currencycloud’s basic rate lookup service to get basic foreign exchange rate information for a pair of
                 * currencies.
                 * 2. Use Currencycloud’s detailed rate lookup service to get a full quotation for converting money from one currency
                 * to another.
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
                 * 2: Get basic exchange rate information
                 *  Currencycloud’s Get Basic Rates endpoint provides real-time exchange rate data.
                 *
                 *  Quotes are given on currency pairs. A currency pair is two standard currency codes joined together: “EURUSD”, “GBPUSD”,
                 * “GBPJPY”, etc. The first currency in the pair is the base currency. The second is the quote currency. The result
                 * indicates how much of the quote currency is needed to buy one unit of the base currency.
                 *
                 * For example, to find out how many Pound Sterling (GBP) are needed to buy EUR €1.00, make the following call. Note the
                 * use of the currency pair “EURGBP”.
                 */
                var findRates = await client.FindRatesAsync("EURGBP");
                Console.WriteLine("Find Rates: {0}\n", findRates.ToJSON());

                /**
                 * The two rates in the response are the “bid” and “offer” prices. The bid price is applicable if you are selling the base
                 * currency. The offer rate is applicable if you are buying the base currency.
                 */

                /**
                 * 3. Get a detailed quote
                 * To find out exactly how much it will cost you to trade funds in one currency for another, use Currencycloud’s
                 * Get Detailed Rates endpoint. For example, to get a quote to buy 10,000 Euros using funds from your Pound Sterling
                 * balance, make the following call:
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
                 *
                 * When you fetch exchange rate information from the Get Basic Rates endpoint, the returned currency pair string will match
                 * exactly the value of the currency_pair input parameter. However, when you get a quote from the Get Detailed Rates
                 * endpoint, the value of the currency_pair property in the response will be standardised, adhering to market conventions
                 * for currency pair notation.
                 *
                 * It is conventional to represent a pairing of Euros to Pound Sterling as “EURGBP”, never “GBPEUR”, regardless which of
                 * the two currencies you are buying and selling. By default, the least valuable currency is the second unit in a currency
                 * pair. But there are some exceptions. If any of the following currencies are quoted against each other, then the currency
                 * appearing first in the list will be the first in the currency pair.
                 *
                 * – EUR
                 * – GBP
                 * – AUD
                 * – NZD
                 * – USD
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