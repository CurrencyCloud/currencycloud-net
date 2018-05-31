using System;
using System.Threading.Tasks;
using CurrencyCloud;
using CurrencyCloud.Entity;
using CurrencyCloud.Entity.List;
using CurrencyCloud.Exception;
using CurrencyCloud.Environment;

namespace Cookbook
{
    class Program
    {
        static void Main()
        {
            MainAsync().Wait();

            Console.ReadKey(false);
        }

        static async Task MainAsync()
        {
            //Data to use in example
            var conversionData = new
            {
                BuyCurrency = "EUR",
                SellCurrency = "GBP",
                Amount = 10000,
                FixedSide = "buy",
                TermAgreement = true
            };
            var beneficiaryData = new
            {
                Name = "Employee Funds",
                Country = "DE",
                Account = "Acme GmbH",
                BicSwift = "COBADEFF",
                Iban = "DE89370400440532013000"
            };
            var paymentData = new
            {
                Type = "regular",
                Reason = "Invoice Payment",
                Reference = "Invoice 1234"
            };

            Client client = new Client();

            bool isAuthenticated = false;

            try
            {
                // TODO: Before running the example please replace loginId and apiKey with valid credentials
                var token = await client.InitializeAsync(ApiServer.Demo, "development@currencycloud.com", "deadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeef");

                Console.WriteLine("Token: {0}", token);

                isAuthenticated = true;

                var rateParams = new DetailedRates(conversionData.BuyCurrency,
                    conversionData.SellCurrency,
                    conversionData.FixedSide,
                    conversionData.Amount);
                Console.WriteLine(Environment.NewLine + "Rate:");
                Rate rate = await client.GetRateAsync(rateParams);
                Console.WriteLine(rate.ToJSON());

                var conversionParams = new Conversion(conversionData.BuyCurrency,
                    conversionData.SellCurrency,
                    conversionData.FixedSide,
                    conversionData.Amount,
                    true);
                Console.WriteLine(Environment.NewLine + "Conversion:");
                Conversion conversion = await client.CreateConversionAsync(conversionParams);
                Console.WriteLine(conversion.ToJSON());

                Console.WriteLine(Environment.NewLine + "Beneficiary required details:");
                BeneficiaryDetailsList beneficiaryDetails = await client.GetBeneficiaryRequiredDetailsAsync(
                    currency: conversionData.BuyCurrency,
                    bankAccountCountry: beneficiaryData.Country
                );

                Console.WriteLine(beneficiaryDetails.ToJSON());

                Console.WriteLine(Environment.NewLine + "Beneficiary:");
                Beneficiary beneficiary = await client.CreateBeneficiaryAsync(new Beneficiary( beneficiaryData.Account, beneficiaryData.Country, conversionData.BuyCurrency, beneficiaryData.Name)
                {
                    BicSwift = beneficiaryData.BicSwift,
                    Iban = beneficiaryData.Iban
                });
                Console.WriteLine(beneficiary.ToJSON());

                Console.WriteLine(Environment.NewLine + "Payment:");
                Payment payment = await client.CreatePaymentAsync(new Payment(conversionData.BuyCurrency, beneficiary.Id, conversionData.Amount, paymentData.Reason, paymentData.Reference)
                {
                    ConversionId = conversion.Id,
                    PaymentType = paymentData.Type
                });
                Console.WriteLine(payment.ToJSON());
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
                if (isAuthenticated)
                {
                    await client.CloseAsync();
                }
            }
        }
    }
}
