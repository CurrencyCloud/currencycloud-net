using NUnit.Framework;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class ClientTest
    {
        Client client = new Client();

        [TestFixtureSetUp]
        protected void Setup()
        {

        }

        [TestFixtureTearDown]
        protected void TearDown()
        {

        }

        [Test]
        public async void Login()
        {
            var credentials = Mocks.Credentials;

            var token = await client.LoginAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            Assert.IsNotEmpty(token);
        }
    }
}
