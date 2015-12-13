using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

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
        public async void GetBeneficiaryRequiredDetails()
        {
            player.Play("GetBeneficiaryRequiredDetails");

            //var account1 = Accounts.Account1;

            //Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
            //Assert.IsTrue(AreEqual(account1, created));
        }

        /// <summary>
        /// Successfully gets conversion dates.
        /// </summary>
        [Test]
        public async void GetConversionDates()
        {
            player.Play("GetConversionDates");
        }

        /// <summary>
        /// Successfully gets available currencies.
        /// </summary>
        [Test]
        public async void GetAvailableCurrencies()
        {
            player.Play("GetAvailableCurrencies");
        }

        /// <summary>
        /// Successfully gets payment dates.
        /// </summary>
        [Test]
        public async void GetPaymentDates()
        {
            player.Play("GetPaymentDates");
        }

        /// <summary>
        /// Successfully gets settlement accounts.
        /// </summary>
        [Test]
        public async void GetSettlementAccounts()
        {
            player.Play("GetSettlementAccounts");
        }
    }
}
