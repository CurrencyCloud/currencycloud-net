using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class TransactionsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Transactions.json");

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
        /// Successfully gets a transaction.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1.BuyCurrency, conversion1.SellCurrency, conversion1.FixedSide, conversion1.Amount, conversion1.Reason, conversion1.TermAgreement);
            PaginatedTransactions found = await client.FindTransactionsAsync(new ParamsObject(new
            {
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            }));
            Transaction gotten = await client.GetTransactionAsync(found.Transactions[0].Id);

            Assert.AreEqual(found.Transactions[0], gotten);
        }

        /// <summary>
        /// Successfully finds a transaction.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1.BuyCurrency, conversion1.SellCurrency, conversion1.FixedSide, conversion1.Amount, conversion1.Reason, conversion1.TermAgreement);
            PaginatedTransactions found = await client.FindTransactionsAsync(new ParamsObject(new
            {
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            }));

            Assert.AreEqual("conversion", found.Transactions[0].RelatedEntityType);
            Assert.AreEqual(conversion.Id, found.Transactions[0].RelatedEntityId);
        }
    }
}
