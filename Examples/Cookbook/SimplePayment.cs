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
    public class SimplePayment
    {
        public static void Run()
        {
            Console.WriteLine("*** Simple Payment ***");
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
                 * Make simple payments to beneficiaries
                 *  A payment is a transfer of money from a payer’s account to a beneficiary.
                 *
                 * Payments cannot be made in one currency and received in another. To pay a beneficiary in a particular currency, the
                 * payer must hold funds in that currency. If necessary, the payer must convert funds from one currency to another before
                 * making a payment.
                 *
                 * In this cookbook, you will:
                 *
                 * 1. Check how much money you hold in various foreign currencies.
                 * 2. Use funds from your Euros balance to make a payment in Euros to a beneficiary in Germany.
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
                 * 2. Check available balances
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
                 * 3. Check payment requirements
                 *  Currencycloud supports two types of payments:
                 *
                 * a. Regular payments: Made using the local bank network. Regular payments are normally received by beneficiary’s within
                 * five working days of the settlement date. This is a good choice for low-value, non-urgent transactions.
                 *
                 * b. Priority payments: Made using the SWIFT network. Payments can be made to over 212 countries, and 95% of payments
                 * arrive within one working day.
                 *
                 * You want to make a regular payment to a supplier based in Germany. You will pay the beneficiary in Euros. You have
                 * enough funds in your Euros balance already to make this payment, so there is no need to top-up your Euros balance
                 * beforehand.
                 *
                 * First, check what details are required to make a regular payment in Euros to a beneficiary with a bank account in
                 * Germany. To do that, call the Get Beneficiary Requirements endpoint.
                 *
                 */
                var beneficiaryDetails = await client.GetBeneficiaryRequiredDetailsAsync("EUR");
                Console.WriteLine("Beneficiary Details: {0}\n", beneficiaryDetails.ToJSON());

                /**
                 * The response tells us that, to make a regular payment to a German bank account in Euros, we need two pieces of
                 information: the IBAN and BIC/SWIFT numbers for the beneficiary. The beneficiary could be either a company or
                 an individual. Either way, the same information is required.
                 */

                /**
                 * 4. Add a beneficiary
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
                    Iban = "DE89370400440532013000"
                });
                Console.WriteLine("Create Beneficiary: {0}\n", createBeneficiary.ToJSON());

                /**
                 * If the beneficiary is successfully created, the response message will contain full details about the beneficiary as
                 * recorded in your Currencycloud account. Note the beneficiary’s unique ID (id). You’ll need this to make a payment to
                 * the beneficiary, in the next step.
                 */

                /**
                 * 5. Make a payment
                 * Authorize a payment by calling the Create Payment endpoint. Optionally, you may provide an idempotency key (via the
                 * unique_request_id parameter). This helps protect against accidental duplicate payments.
                 */
                var createPayment = await client.CreatePaymentAsync(new Payment
                {
                    Currency = createBeneficiary.Currency,
                    BeneficiaryId = createBeneficiary.Id,
                    Amount = 10000,
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
                 * 6. Logout
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