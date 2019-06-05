using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class TransactionsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Transactions.json");

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
        /// Successfully finds a transaction with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            PaginatedTransactions found = await client.FindTransactionsAsync(new TransactionFindParameters
            {
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.AreEqual("conversion", found.Transactions[0].RelatedEntityType);
            Assert.AreEqual(conversion.Id, found.Transactions[0].RelatedEntityId);
        }

        /// <summary>
        /// Successfully finds a transaction without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            PaginatedTransactions found = await client.FindTransactionsAsync();

            Assert.AreEqual("conversion", found.Transactions[0].RelatedEntityType);
            Assert.AreEqual(conversion.Id, found.Transactions[0].RelatedEntityId);
        }

        /// <summary>
        /// Successfully gets a transaction.
        /// </summary>
        [Test]
        public async Task Get()
        {
            player.Play("Get");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            PaginatedTransactions found = await client.FindTransactionsAsync(new TransactionFindParameters
            {
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });
            Transaction gotten = await client.GetTransactionAsync(found.Transactions[0].Id);

            Assert.AreEqual(found.Transactions[0].ToJSON(), gotten.ToJSON());
        }

        /// <summary>
        /// Successfully gets details of the sender of funds.
        /// </summary>
        [Test]
        public async Task GetSenderDetails()
        {
            player.Play("GetSenderDetails");

            var senderDetails = Transactions.SenderDetails1;

            PaginatedTransactions found = await client.FindTransactionsAsync(new TransactionFindParameters
            {
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });
            SenderDetails gotten = await client.GetSenderDetailsAsync(found.Transactions[0].Id);

            Assert.AreEqual(senderDetails, gotten);
        }
    }
}
