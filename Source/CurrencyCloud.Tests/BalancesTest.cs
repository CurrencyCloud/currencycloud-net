using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class BalancesTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Balances.json");

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
        /// Successfully gets a balance.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var balance = await client.GetBalanceAsync("GBP");

            Assert.AreEqual(balance.Currency, "GBP");
        }

        /// <summary>
        /// Successfully finds a balance.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            var balance = await client.GetBalanceAsync("GBP");
            var found = await client.FindBalancesAsync();

            Assert.Contains(balance, found.Balances);
        }
    }
}
