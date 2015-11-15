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
            var task = client.InitializeAsync(Mocks.Credentials.Environment, Mocks.Credentials.LoginId, Mocks.Credentials.APIkey);
            Task.WaitAll(task);
        }

        [TestFixtureTearDown]
        protected void TearDown()
        {
            var task = client.CloseAsync();
            Task.WaitAll(task);
        }

        [Test]
        public async void Creates()
        {
            dynamic optional = new ParamsObject();
            optional.status = "enabled";
            optional.city = "London";

            Account created = await client.CreateAccountAsync("Vive", "company", optional);
        }

        [Test]
        public async void GetsCurrent()
        {
            Account current = await client.GetCurrentAccountAsync();
            Assert.IsNotNull(current);
        }
    }
}
