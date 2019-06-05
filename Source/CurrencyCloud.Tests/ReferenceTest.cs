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
                ConversionDatesList gotten = await client.GetConversionDatesAsync("USDGBP");
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
    }
}
