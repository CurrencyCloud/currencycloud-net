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
        Player player = new Player("../../Mock/Http/Recordings/Reference.json");

        [TestFixtureSetUp]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;
            client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey).Wait();
        }

        [TestFixtureTearDown]
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

            Assert.DoesNotThrow(async () => {
                BeneficiaryDetailsList gotten = await client.GetBeneficiaryRequiredDetailsAsync(new
                {
                    Currency = "GBP",
                    BankAccountCountry = "GB",
                    BeneficiaryCountry = "GB"
                });
            });
        }

        /// <summary>
        /// Successfully gets conversion dates.
        /// </summary>
        [Test]
        public void GetConversionDates()
        {
            player.Play("GetConversionDates");

            Assert.DoesNotThrow(async () => {
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

            Assert.DoesNotThrow(async () => {
                CurrenciesList gotten = await client.GetAvailableCurrenciesAsync();
            });
        }

        /// <summary>
        /// Successfully gets payment dates.
        /// </summary>
        [Test]
        public void GetPaymentDates()
        {
            player.Play("GetPaymentDates");

            Assert.DoesNotThrow(async () => {
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

            Assert.DoesNotThrow(async () => {
                SettlementAccountsList gotten = await client.GetSettlementAccountsAsync("EUR");
            });
        }
    }
}
