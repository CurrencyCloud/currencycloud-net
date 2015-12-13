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

            //var account1 = Accounts.Account1;

            //Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
            //Assert.IsTrue(AreEqual(account1, created));
        }

        /// <summary>
        /// Successfully finds a transaction.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            //Account current = await client.GetCurrentAccountAsync();
            //PaginatedAccounts accounts = await client.FindAccountsAsync(new
            //{
            //    AccountName = current.AccountName,
            //    Order = "created_at",
            //    OrderAscDesc = "desc",
            //    PerPage = 5
            //});
        }
    }
}
