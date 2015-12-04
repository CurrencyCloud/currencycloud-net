using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;

namespace CurrencyCloud.Tests.EndPoint
{
    [TestFixture]
    class AccountsTest
    {
        Client client = new Client();

        [TestFixtureSetUp]
        public async void SetUp()
        {
            var credentials = Authentication.Credentials;

            await client.LoginAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
        }

        [TestFixtureTearDown]
        public async void TearDown()
        {
            await client.LogoutAsync();
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
    }
}
