using CurrencyCloud.Entity;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests.EndPoint
{
    [TestFixture]
    class AccountsTest
    {
        Client client = new Client();

        [TestFixtureSetUp]
        protected void Setup()
        {
            var credentials = Mocks.Credentials;

            var task = client.LoginAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            Task.WaitAll(task);
        }

        [TestFixtureTearDown]
        protected void TearDown()
        {
            var task = client.LogoutAsync();
            Task.WaitAll(task);
        }

        [Test]
        public async void Create()
        {
            var account1 = Mocks.Account1;

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
