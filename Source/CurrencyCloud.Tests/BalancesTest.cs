using System;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity;
using CurrencyCloud.Entity.Pagination;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class BalancesTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Balances.json");

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
        /// Successfully gets a balance.
        /// </summary>
        [Test]
        public void Get()
        {
            player.Play("Get");

            Assert.DoesNotThrowAsync(async () =>
            {
                Balance balance = await client.GetBalanceAsync("GBP");
            });
        }

        /// <summary>
        /// Successfully finds a balance with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            Balance balance = await client.GetBalanceAsync("GBP");
            PaginatedBalances found = await client.FindBalancesAsync(new BalanceFindParameters
            {
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.Contains(balance, found.Balances);
        }

        /// <summary>
        /// Successfully finds a balance without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            Balance balance = await client.GetBalanceAsync("GBP");
            PaginatedBalances found = await client.FindBalancesAsync();

            Assert.Contains(balance, found.Balances);
        }
        
        /// <summary>
        /// Successfully Top Up Margin Balance.
        /// </summary>
        [Test]
        public async Task TopUpMarginBalance()
        {
            player.Play("TopUpMarginBalance");

            MarginBalanceTopUp topUp = await client.TopUpMarginBalanceAsync("GBP", 450);
            Assert.NotNull(topUp);
            Assert.AreEqual("GBP", topUp.Currency);
            Assert.AreEqual(450, topUp.TransferredAmount);
            Assert.AreEqual("6c046c51-2387-4004-8e87-4bf97102e36d", topUp.AccountId);
            Assert.AreEqual(DateTime.Parse("2007-11-19 14:37:48"), topUp.TransferCompletedAt);


        }
    }
}
