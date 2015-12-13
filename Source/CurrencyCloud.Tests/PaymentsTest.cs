using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class PaymentsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Payments.json");

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
        /// Successfully creates a payment.
        /// </summary>
        [Test]
        public async void Create()
        {
            player.Play("Create");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            //Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
           // Assert.IsTrue(AreEqual(account1, created));
        }

        /// <summary>
        /// Successfully gets a payment.
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
        /// Successfully updates a payment.
        /// </summary>
        [Test]
        public async void Update()
        {
            player.Play("Update");

            //var account1 = Accounts.Account1;

            //Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
            //Assert.IsTrue(AreEqual(account1, created));
        }

        /// <summary>
        /// Successfully finds a payment.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            Account current = await client.GetCurrentAccountAsync();
            PaginatedAccounts accounts = await client.FindAccountsAsync(new
            {
                AccountName = current.AccountName,
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            });
        }

        /// <summary>
        /// Successfully deletes a payment.
        /// </summary>
        [Test]
        public async void Delete()
        {
            player.Play("Delete");

            //var account1 = Accounts.Account1;

            //Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
            //Assert.IsTrue(AreEqual(account1, created));
        }
    }
}
