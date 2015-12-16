using System;
using System.Threading.Tasks;
using System.Linq;
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
            var conversionParams = new
            {
                BuyCurrency = "EUR",
                SellCurrency = "GBP",
                Amount = 10000,
                FixedSide = "buy",
                TermAgreement = true
            };
            var beneficiaryParams = new
            {
                Name = "Employee Funds",
                Country = "DE",
                Account = "Acme GmbH",
                BicSwift = "COBADEFF",
                Iban = "DE89370400440532013000"
            };
            var paymentParams = new
            {
                Type = "regular",
                Reason = "Invoice Payment",
                Reference = "Invoice 1234"
            };

            Client client = new Client();

            bool isAuthenticated = false;

            try
            {
                var token = await client.InitializeAsync(ApiServer.Demo, "test.it@mailinator.com", "b5266326b1855443544626f188b8a234da99e1c36d91819419e17091b4f0a7f4");
                Console.WriteLine("Token: {0}", token);

                isAuthenticated = true;

                Console.WriteLine(Environment.NewLine + "Rate:");
                Rate rate = await client.GetRateAsync(conversionParams.BuyCurrency, conversionParams.SellCurrency, conversionParams.FixedSide, conversionParams.Amount);
                Console.WriteLine(rate);

                Console.WriteLine(Environment.NewLine + "Conversion:");
                Conversion conversion = await client.CreateConversionAsync(conversionParams.BuyCurrency, conversionParams.SellCurrency, conversionParams.FixedSide, conversionParams.Amount, paymentParams.Reason, conversionParams.TermAgreement);
                Console.WriteLine(conversion);

                Console.WriteLine(Environment.NewLine + "Beneficiary required details:");
                BeneficiaryDetailsList beneficiaryDetails = await client.GetBeneficiaryRequiredDetailsAsync(new ParamsObject(new
                {
                    Currency = conversionParams.BuyCurrency,
                    BankAccountCountry = beneficiaryParams.Country
                }));
                var details = (from detailsList in beneficiaryDetails.Details
                               select string.Join(", ", (from detail in detailsList
                                                         select string.Format("{0}={1}", detail.Key, detail.Value))
                                                        .ToList()));
                Console.WriteLine(string.Join(Environment.NewLine, details.ToList()));

                Console.WriteLine(Environment.NewLine + "Beneficiary:");
                Beneficiary beneficiary = await client.CreateBeneficiaryAsync(beneficiaryParams.Account, beneficiaryParams.Country, conversionParams.BuyCurrency, beneficiaryParams.Name, new ParamsObject(new
                {
                    BicSwift = beneficiaryParams.BicSwift,
                    Iban = beneficiaryParams.Iban
                }));
                Console.WriteLine(beneficiary);

                Console.WriteLine(Environment.NewLine + "Payment:");
                Payment payment = await client.CreatePaymentAsync(conversionParams.BuyCurrency, beneficiary.Id, conversionParams.Amount, paymentParams.Reason, paymentParams.Reference, new ParamsObject(new
                {
                    ConversionId = conversion.Id,
                    PaymentType = paymentParams.Type
                }));
                Console.WriteLine(payment);
            }
            catch (ApiException ex)
            {
                if(ex is AuthenticationException)
                {
                    isAuthenticated = false;
                }

                Console.WriteLine(ex.ToYamlString());
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
