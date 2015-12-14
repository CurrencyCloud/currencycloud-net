using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class AccountsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Accounts.json");

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
        /// Successfully creates an account.
        /// </summary>
        [Test]
        public async void Create()
        {
            player.Play("Create");

            var account1 = Accounts.Account1;

            Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);

            Assert.AreEqual(account1.AccountName, created.AccountName);
            Assert.AreEqual(account1.LegalEntityType, created.LegalEntityType);
            Assert.AreEqual(account1.Optional.YourReference, created.YourReference);
            Assert.AreEqual(account1.Optional.Status, created.Status);
            Assert.AreEqual(account1.Optional.Street, created.Street);
            Assert.AreEqual(account1.Optional.City, created.City);
            Assert.AreEqual(account1.Optional.StateOrProvince, created.StateOrProvince);
            Assert.AreEqual(account1.Optional.PostalCode, created.PostalCode);
            Assert.AreEqual(account1.Optional.Country, created.Country);
            Assert.AreEqual(account1.Optional.SpreadTable, created.SpreadTable);
            Assert.AreEqual(account1.Optional.IdentificationType, created.IdentificationType);
        }

        /// <summary>
        /// Successfully gets an account.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var account1 = Accounts.Account1;

            Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
            Account gotten = await client.GetAccountAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully updates an account.
        /// </summary>
        [Test]
        public async void Update()
        {
            player.Play("Update");

            var account1 = Accounts.Account1;
            var account2 = Accounts.Account2;

            Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
            Account updated = await client.UpdateAccountAsync(created.Id, account2);
            Account gotten = await client.GetAccountAsync(created.Id);

            Assert.AreEqual(gotten, updated);
        }

        /// <summary>
        /// Successfully finds an account.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            Account current = await client.GetCurrentAccountAsync();
            PaginatedAccounts found = await client.FindAccountsAsync(new
            {
                AccountName = current.AccountName,
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            });

            Assert.Contains(current, found.Accounts);
        }

        /// <summary>
        /// Successfully gets current account.
        /// </summary>
        [Test]
        public void GetCurrent()
        {
            player.Play("GetCurrent");

            Assert.DoesNotThrow(async () => {
                Account current = await client.GetCurrentAccountAsync();
            });
        }
    }
}
