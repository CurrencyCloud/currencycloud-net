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
    public class PayBeneficiary
    {
        public static void Run()
        {
            Console.WriteLine("*** Pay Beneficiary ***");
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
                 * Pay a beneficiary using funds in a different currency.
                 *
                 * In this cookbook, you will:
                 *
                 * 1. Check how much money you hold in various foreign currencies in your Currencycloud account.
                 * 2. Top up your Euros balance by trading some Pound Sterling.
                 * 3. Make a payment in Euros to a beneficiary in Germany.
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
                 * You can also check the balances for all foreign currencies that you hold in your Currencycloud account by calling the
                 * Find Balances endpoint
                 */
                var findBalances = await client.FindBalancesAsync();
                Console.WriteLine("Find Balances: {0}\n", findBalances.ToJSON());

                /**
                 * 3. Top up
                 * Check how much it will cost to buy 10,000 Euros using funds from your Pound Sterling balance, by making a call to the
                 * Get Detailed Rates endpoint.
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
                 * If you’re happy with the quote, you may authorize the conversion by calling the Create Conversion endpoint.
                 */
                var createConversion = await client.CreateConversionAsync(new Conversion("EUR", "GBP", "buy", 10000, true));
                Console.WriteLine("Create Conversion: {0}\n", createConversion.ToJSON());

                /**
                 * On success, the payload of the response message will contain full details of the conversion as recorded against your
                 * Currencycloud account. Note the unique conversion id (the id field). You’ll need this to complete the conversion.
                 */

                /**
                 * 4. Check payment requirements
                 * You want to make a regular payment to a supplier based in Germany. First, check what details are required
                 * to make a regular payment in Euros to a beneficiary with a bank account in Germany. To do that, call the
                 * Get Beneficiary Requirements endpoint.
                 */
                var beneficiaryDetails = await client.GetBeneficiaryRequiredDetailsAsync("EUR", "DE", "DE");
                Console.WriteLine("Beneficiary Details: {0}\n", beneficiaryDetails.ToJSON());

                /**
                 * The response tells us that, to make a regular payment to a German bank account in Euros, we need two pieces of
                 * information: the IBAN and BIC/SWIFT numbers for the beneficiary. The beneficiary could be either a company or
                 * an individual. Either way, the same information is required.
                 */

                /**
                 * 5. Add a beneficiary
                 * If you know the required details, you can go ahead and create a record for the beneficiary via the
                 * Create Beneficiary endpoint.
                 */
                var createBeneficiary = await client.CreateBeneficiaryAsync(new Beneficiary
                {
                    Name = "Acme GmbH",
                    BankAccountHolderName = "Acme GmbH",
                    Currency = "EUR",
                    BeneficiaryCountry = "DE",
                    BankCountry = "DE",
                    BicSwift = "COBADEFF",
                    Iban = "DE75512108001245126199"
                });
                Console.WriteLine("Create Beneficiary: {0}\n", createBeneficiary.ToJSON());

                /**
                 * If the beneficiary is successfully created, the response message will contain full details about the beneficiary as
                 * recorded in your Currencycloud account. Note the beneficiary’s unique ID (id). You’ll need this to make a payment to
                 * the beneficiary, in the next step.
                 */

                /**
                 * 6. Make a payment
                 * Authorize a payment by calling the Create Payment endpoint. Optionally, you may provide an idempotency key (via the
                 * unique_request_id parameter). This helps protect against accidental duplicate payments.
                 */
                var createPayment = await client.CreatePaymentAsync(new Payment
                {
                    Currency = createConversion.BuyCurrency,
                    BeneficiaryId = createBeneficiary.Id,
                    Amount = createConversion.ClientBuyAmount,
                    Reason = "Invoice Payment",
                    Reference = "2018-014",
                    PaymentType = "regular",
                    UniqueRequestId = Guid.NewGuid().ToString()
                });
                Console.WriteLine("Create Payment: {0}\n", createPayment.ToJSON());

                /**
                 * If the payment is successfully queued, the response payload will contain all the information about the payment as
                 * recorded in your Currencycloud account. This does not mean that the payment was made. It just means that it is ready
                 * for processing.
                 *
                 * Payments are processed asynchronously. Currencycloud will process payments on the payment_date specified, provided
                 * you hold enough money in the relevant currency at the time. It is possible to instruct payments even if you don’t hold
                 * enough money in the relevant currency. The payments will be queued in the normal way but will not be processed until
                 * your account balance is topped up.
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
                 * 7. Logout
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