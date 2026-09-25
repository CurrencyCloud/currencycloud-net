using System;
using CurrencyCloud.Entity;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity.List;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ReferenceTest
    {
        Client client = new Client();
        Player player = new Player("Mock/Http/Recordings/Reference.json");

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;
            client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey).Wait();
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            player.Play("TearDown");

            client.CloseAsync().Wait();

            player.Close();
        }

        /// <summary>
        /// Successfully gets beneficiary required details.
        /// </summary>
        [Test]
        public void GetBeneficiaryRequiredDetails()
        {
            player.Play("GetBeneficiaryRequiredDetails");

            Assert.DoesNotThrowAsync(async () =>
            {
                BeneficiaryDetailsList gotten = await client.GetBeneficiaryRequiredDetailsAsync("GBP","GB","GB");
            });
        }

        /// <summary>
        /// Successfully gets conversion dates using the default (RoundtripKind) DateTime handling.
        /// </summary>
        [Test]
        public void GetConversionDates()
        {
            player.Play("GetConversionDates");

            Assert.DoesNotThrowAsync(async () => {
                ConversionDatesList conversionDates = await client.GetConversionDatesAsync("USDGBP");
                Assert.That(conversionDates.DefaultConversionDate, Is.EqualTo(DateTime.Parse("2020-11-12T00:00:00")));
                Assert.That(conversionDates.FirstConversionDate, Is.EqualTo(DateTime.Parse("2020-11-10T00:00:00")));
                Assert.That(conversionDates.FirstConversionCutoffDatetime, Is.EqualTo(DateTime.Parse("2020-11-10T23:19:00+00:00")));
                Assert.That(conversionDates.OptimizeLiquidityConversionDate, Is.EqualTo(DateTime.Parse("2020-11-12T00:00:00")));
                Assert.That(conversionDates.InvalidConversionDates.Count, Is.EqualTo(241));
            });
        }

        /// <summary>
        /// Successfully gets conversion dates with Serialization.DateTimeZoneHandling set to Utc, so API dates are
        /// deserialized as UTC (Kind = Utc) rather than being converted to the machine's local time zone.
        /// </summary>
        [Test]
        public void GetConversionDatesWithUtcHandling()
        {
            player.Play("GetConversionDatesWithUtcHandling");

            // Opt into UTC deserialization for this test, then restore the default so other fixtures
            // continue to see the SDK's historical (RoundtripKind) behaviour.
            var previous = Serialization.DateTimeZoneHandling;
            Serialization.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            try
            {
                Assert.DoesNotThrowAsync(async () => {
                    ConversionDatesList conversionDates = await client.GetConversionDatesAsync("USDGBP");

                    // With DateTimeZoneHandling.Utc, API dates come back as UTC (Kind = Utc), matching the
                    // values the API returns, regardless of the machine's local time zone. DateTime.Equals
                    // ignores Kind, so the explicit Kind asserts are what guard against a local-time regression.
                    Assert.That(conversionDates.FirstConversionCutoffDatetime, Is.EqualTo(new DateTime(2020, 11, 10, 23, 19, 0, DateTimeKind.Utc)));
                    Assert.That(conversionDates.FirstConversionCutoffDatetime.Kind, Is.EqualTo(DateTimeKind.Utc));
                    Assert.That(conversionDates.FirstConversionDate, Is.EqualTo(new DateTime(2020, 11, 10, 0, 0, 0, DateTimeKind.Utc)));
                    Assert.That(conversionDates.FirstConversionDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    Assert.That(conversionDates.DefaultConversionDate, Is.EqualTo(new DateTime(2020, 11, 12, 0, 0, 0, DateTimeKind.Utc)));
                    Assert.That(conversionDates.OptimizeLiquidityConversionDate, Is.EqualTo(new DateTime(2020, 11, 12, 0, 0, 0, DateTimeKind.Utc)));
                    Assert.That(conversionDates.InvalidConversionDates.Count, Is.EqualTo(241));
                });
            }
            finally
            {
                Serialization.DateTimeZoneHandling = previous;
            }
        }

        /// <summary>
        /// Successfully gets available currencies.
        /// </summary>
        [Test]
        public void GetAvailableCurrencies()
        {
            player.Play("GetAvailableCurrencies");

            Assert.DoesNotThrowAsync(async () => {
                CurrenciesList gotten = await client.GetAvailableCurrenciesAsync();
            });
        }

        /// <summary>
        /// Successfully gets purpose codes.
        /// </summary>
        [Test]
        public void GetPaymentPurposeCodes()
        {
            player.Play("GetPaymentPurposeCodes");

            Assert.DoesNotThrowAsync(async () => {
                PaymentPurposeCodeList gotten = await client.GetPaymentPurposeCodes("INR", "IN");
            });
        }

        /// <summary>
        /// Successfully gets payment dates.
        /// </summary>
        [Test]
        public void GetPaymentDates()
        {
            player.Play("GetPaymentDates");

            Assert.DoesNotThrowAsync(async () => {
                PaymentDatesList gotten = await client.GetPaymentDatesAsync("USD");
            });
        }

        /// <summary>
        /// Successfully gets settlement accounts.
        /// </summary>
        [Test]
        public void GetSettlementAccounts()
        {
            player.Play("GetSettlementAccounts");

            Assert.DoesNotThrowAsync(async () => {
                SettlementAccountsList gotten = await client.GetSettlementAccountsAsync("EUR");
            });
        }
        
        /// <summary>
        /// Successfully gets bank details.
        /// </summary>
        [Test]
        public void GetBankDetails()
        {
            player.Play("GetBankDetails");

            Assert.DoesNotThrowAsync(async () => {
                BankDetails bankDetails = await client.GetBankDetailsAsync("iban", "GB33BUKB20201555555555");
                Assert.That(bankDetails, Is.Not.Null);
                Assert.That(bankDetails.IdentifierType, Is.EqualTo("iban"));
                Assert.That(bankDetails.IdentifierValue, Is.EqualTo("GB33BUKB20201555555555"));
                Assert.That(bankDetails.AccountNumber, Is.EqualTo("GB33BUKB20201555555555"));
                Assert.That(bankDetails.BankAddress, Is.EqualTo("12 STEWARD STREET  THE STEWARD BUILDING FLOOR 0"));
                Assert.That(bankDetails.BankBranch, Is.EqualTo(""));
                Assert.That(bankDetails.BankCity, Is.EqualTo("LONDON"));
                Assert.That(bankDetails.BankCountry, Is.EqualTo("UNITED KINGDOM"));
                Assert.That(bankDetails.BankName, Is.EqualTo("THE CURRENCY CLOUD LIMITED"));
                Assert.That(bankDetails.BankState, Is.EqualTo("LONDON"));
                Assert.That(bankDetails.BicSwift, Is.EqualTo("TCCLGB22XXX"));
                Assert.That(bankDetails.BankCountryISO, Is.EqualTo("GB"));
                Assert.That(bankDetails.Currency, Is.Null);
            });
        }
        
        /// <summary>
        /// Successfully Get Payment Fee Rules.
        /// </summary>
        [Test]
        public void GetPaymentFeeRules()
        {
            player.Play("GetPaymentFeeRules");

            Assert.DoesNotThrowAsync(async () => {
                PaymentFeeRulesList rules1 = await client.GetPaymentFeeRulesAsync();
                Assert.That(rules1, Is.Not.Null);
                Assert.That(rules1.PaymentFeeRules, Is.Not.Null);
                Assert.That(rules1.PaymentFeeRules.Count, Is.EqualTo(3));
                PaymentFeeRulesList.PaymentFeeRule feeRule11 = rules1.PaymentFeeRules[0];
                Assert.That(feeRule11.ChargeType, Is.EqualTo("shared"));
                Assert.That(feeRule11.FeeAmount, Is.EqualTo(2.0));
                Assert.That(feeRule11.FeeCurrency, Is.EqualTo("AED"));
                Assert.That(feeRule11.PaymentType, Is.EqualTo("priority"));
                PaymentFeeRulesList.PaymentFeeRule feeRule12 = rules1.PaymentFeeRules[1];
                Assert.That(feeRule12.ChargeType, Is.EqualTo("shared"));
                Assert.That(feeRule12.FeeAmount, Is.EqualTo(12.0));
                Assert.That(feeRule12.FeeCurrency, Is.EqualTo("USD"));
                Assert.That(feeRule12.PaymentType, Is.EqualTo("regular"));
                PaymentFeeRulesList.PaymentFeeRule feeRule13 = rules1.PaymentFeeRules[2];
                Assert.That(feeRule13.ChargeType, Is.EqualTo("ours"));
                Assert.That(feeRule13.FeeAmount, Is.EqualTo(5.25));
                Assert.That(feeRule13.FeeCurrency, Is.EqualTo("GBP"));
                Assert.That(feeRule13.PaymentType, Is.EqualTo("priority"));
                
                PaymentFeeRulesList rules2 = await client.GetPaymentFeeRulesAsync(null, "regular");
                Assert.That(rules2, Is.Not.Null);
                Assert.That(rules2.PaymentFeeRules, Is.Not.Null);
                Assert.That(rules2.PaymentFeeRules.Count, Is.EqualTo(1));
                PaymentFeeRulesList.PaymentFeeRule feeRule21 = rules2.PaymentFeeRules[0];
                Assert.That(feeRule21.ChargeType, Is.EqualTo("shared"));
                Assert.That(feeRule21.FeeAmount, Is.EqualTo(12.0));
                Assert.That(feeRule21.FeeCurrency, Is.EqualTo("USD"));
                Assert.That(feeRule21.PaymentType, Is.EqualTo("regular"));
                
                PaymentFeeRulesList rules3 = await client.GetPaymentFeeRulesAsync(null, null,"ours");
                Assert.That(rules3, Is.Not.Null);
                Assert.That(rules3.PaymentFeeRules, Is.Not.Null);
                Assert.That(rules3.PaymentFeeRules.Count, Is.EqualTo(1));
                PaymentFeeRulesList.PaymentFeeRule feeRule31 = rules3.PaymentFeeRules[0];
                Assert.That(feeRule31.ChargeType, Is.EqualTo("ours"));
                Assert.That(feeRule31.FeeAmount, Is.EqualTo(5.25));
                Assert.That(feeRule31.FeeCurrency, Is.EqualTo("GBP"));
                Assert.That(feeRule31.PaymentType, Is.EqualTo("priority"));
            });
        }
    }
}
