using System.Threading.Tasks;
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

        private bool AreEqual(dynamic accountParams, Account account)
        {
            return accountParams.AccountName == account.AccountName &&
                   accountParams.LegalEntityType == account.LegalEntityType &&
                   accountParams.Optional.YourReference == account.YourReference &&
                   accountParams.Optional.Status == account.Status &&
                   accountParams.Optional.Street == account.Street &&
                   accountParams.Optional.City == account.City &&
                   accountParams.Optional.StateOrProvince == account.StateOrProvince &&
                   accountParams.Optional.PostalCode == account.PostalCode &&
                   accountParams.Optional.Country == account.Country &&
                   accountParams.Optional.SpreadTable == account.SpreadTable &&
                   accountParams.Optional.IdentificationType == account.IdentificationType;
        }

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
            Assert.IsTrue(AreEqual(account1, created));
        }

        //[Test]
        public async void GetCurrent()
        {
            Account current = await client.GetCurrentAccountAsync();
            Assert.IsNotNull(current);
        }

        //[Test]
        public async void Find()
        {
            Account current = await client.GetCurrentAccountAsync();
            PaginatedAccounts accounts = await client.FindAccountsAsync(new
            {
                AccountName = current.AccountName,
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            });
        }

    }
}
