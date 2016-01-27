using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity;
using CurrencyCloud.Entity.Pagination;

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
        public void Get()
        {
            player.Play("Get");

            Assert.DoesNotThrow(async () =>
            {
                Balance balance = await client.GetBalanceAsync("GBP");
            });
        }

        /// <summary>
        /// Successfully finds a balance.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            Balance balance = await client.GetBalanceAsync("GBP");
            PaginatedBalances found = await client.FindBalancesAsync(new BalanceFindParameters
            {
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.desc,
                PerPage = 5
            });

            Assert.Contains(balance, found.Balances);
        }
    }
}
