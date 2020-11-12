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
        Player player = new Player("/../../Mock/Http/Recordings/Reference.json");

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
        /// Successfully gets conversion dates.
        /// </summary>
        [Test]
        public void GetConversionDates()
        {
            player.Play("GetConversionDates");

            Assert.DoesNotThrowAsync(async () => {
                ConversionDatesList conversionDates = await client.GetConversionDatesAsync("USDGBP");
                Assert.AreEqual(DateTime.Parse("2020-11-12T00:00:00"), conversionDates.DefaultConversionDate);
                Assert.AreEqual(DateTime.Parse("2020-11-10T00:00:00"), conversionDates.FirstConversionDate);
                Assert.AreEqual(DateTime.Parse("2020-11-10T23:19:00+00:00"), conversionDates.FirstConversionCutoffDatetime);
                Assert.AreEqual(DateTime.Parse("2020-11-12T00:00:00"), conversionDates.OptimizeLiquidityConversionDate);
                Assert.AreEqual(241, conversionDates.InvalidConversionDates.Count);
            });
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
                BankDetails bankDetails = await client.GetBankDetailsAsync("iban", "GB19TCCL00997901654515");
                Assert.That(bankDetails, Is.Not.Null);
                Assert.AreEqual("iban", bankDetails.IdentifierType);
                Assert.AreEqual("GB19TCCL00997901654515", bankDetails.IdentifierValue);
                Assert.AreEqual("GB19TCCL00997901654515", bankDetails.AccountNumber);
                Assert.AreEqual("12 STEWARD STREET  THE STEWARD BUILDING FLOOR 0", bankDetails.BankAddress);
                Assert.AreEqual("", bankDetails.BankBranch);
                Assert.AreEqual("LONDON", bankDetails.BankCity);
                Assert.AreEqual("UNITED KINGDOM", bankDetails.BankCountry);
                Assert.AreEqual("THE CURRENCY CLOUD LIMITED", bankDetails.BankName);
                Assert.AreEqual("LONDON", bankDetails.BankState);
                Assert.AreEqual("TCCLGB22XXX", bankDetails.BicSwift);
                Assert.AreEqual("GB", bankDetails.BankCountryISO);
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
                Assert.AreEqual(3, rules1.PaymentFeeRules.Count);
                PaymentFeeRulesList.PaymentFeeRule feeRule11 = rules1.PaymentFeeRules[0];
                Assert.AreEqual("shared", feeRule11.ChargeType);
                Assert.AreEqual(2.0, feeRule11.FeeAmount);
                Assert.AreEqual("AED", feeRule11.FeeCurrency);
                Assert.AreEqual("priority", feeRule11.PaymentType);
                PaymentFeeRulesList.PaymentFeeRule feeRule12 = rules1.PaymentFeeRules[1];
                Assert.AreEqual("shared", feeRule12.ChargeType);
                Assert.AreEqual(12.0, feeRule12.FeeAmount);
                Assert.AreEqual("USD", feeRule12.FeeCurrency);
                Assert.AreEqual("regular", feeRule12.PaymentType);
                PaymentFeeRulesList.PaymentFeeRule feeRule13 = rules1.PaymentFeeRules[2];
                Assert.AreEqual("ours", feeRule13.ChargeType);
                Assert.AreEqual(5.25, feeRule13.FeeAmount);
                Assert.AreEqual("GBP", feeRule13.FeeCurrency);
                Assert.AreEqual("priority", feeRule13.PaymentType);
                
                PaymentFeeRulesList rules2 = await client.GetPaymentFeeRulesAsync(null, "regular");
                Assert.That(rules2, Is.Not.Null);
                Assert.That(rules2.PaymentFeeRules, Is.Not.Null);
                Assert.AreEqual(1, rules2.PaymentFeeRules.Count);
                PaymentFeeRulesList.PaymentFeeRule feeRule21 = rules2.PaymentFeeRules[0];
                Assert.AreEqual("shared", feeRule21.ChargeType);
                Assert.AreEqual(12.0, feeRule21.FeeAmount);
                Assert.AreEqual("USD", feeRule21.FeeCurrency);
                Assert.AreEqual("regular", feeRule21.PaymentType);
                
                PaymentFeeRulesList rules3 = await client.GetPaymentFeeRulesAsync(null, null,"ours");
                Assert.That(rules3, Is.Not.Null);
                Assert.That(rules3.PaymentFeeRules, Is.Not.Null);
                Assert.AreEqual(1, rules3.PaymentFeeRules.Count);
                PaymentFeeRulesList.PaymentFeeRule feeRule31 = rules3.PaymentFeeRules[0];
                Assert.AreEqual("ours", feeRule31.ChargeType);
                Assert.AreEqual(5.25, feeRule31.FeeAmount);
                Assert.AreEqual("GBP", feeRule31.FeeCurrency);
                Assert.AreEqual("priority", feeRule31.PaymentType);
            });
        }
    }
}
