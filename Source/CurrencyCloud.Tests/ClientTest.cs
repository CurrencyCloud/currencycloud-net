using NUnit.Framework;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class ClientTest
    {
        Client client;

        [SetUp]
        protected void Setup()
        {
            client = new Client();
        }

        [TearDown]
        protected void TearDown()
        {

        }

        [Test]
        public async void Initializes()
        {
            var token = await client.InitializeAsync(Mocks.Credentials.Environment, Mocks.Credentials.LoginId, Mocks.Credentials.APIkey);
            Assert.IsNotEmpty(token);
        }
    }
}
