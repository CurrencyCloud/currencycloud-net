using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Page;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class AccountsTest
    {
        Client client = new Client();

        [TestFixtureSetUp]
        public void SetUp()
        {
            var credentials = Authentication.Credentials;

            var task = client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);

            Task.WaitAll(task);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            var task = client.CloseAsync();

            Task.WaitAll(task);
        }

        [Test]
        public async void Create()
        {
            var account1 = Accounts.Account1;

            Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
        }

        [Test]
        public async void GetCurrent()
        {
            Account current = await client.GetCurrentAccountAsync();
            Assert.IsNotNull(current);
        }

        [Test]
        public async void Find()
        {
            Account current = await client.GetCurrentAccountAsync();
            AccountsPage accounts = await client.FindAccountsAsync(new
            {
                AccountName = current.AccountName,
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            });
        }

    }
}
