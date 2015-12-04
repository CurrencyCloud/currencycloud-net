using System;
using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class AuthenticationTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Authentication.json");

        dynamic credentials = Authentication.Credentials;

        [TestFixtureSetUp]
        public async void SetUp()
        {
            await Task.Run(() => 
            {
                player.Start(credentials.ApiServer.Url);
            });
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            player.Stop();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void FailsToMakeAPIcallBeforeLogin()
        {
            await client.GetCurrentAccountAsync();
        }

        [Test]
        public async void Login()
        {
            var token = await client.LoginAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            Assert.IsNotEmpty(token);

            await client.LogoutAsync();
        }

    }
}
