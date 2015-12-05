using System;
using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class ClientTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Client.json");

        dynamic credentials = Authentication.Credentials;

        [TestFixtureSetUp]
        public async void SetUp()
        {
            await Task.Run(() => 
            {
                player.Start(ApiServer.Mock.Url);
            });
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            player.Stop();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void FailAfterClose()
        {
            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            await client.CloseAsync();
            await client.GetCurrentAccountAsync();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void FailBeforeInitialize()
        {
            await client.GetCurrentAccountAsync();
        }

        [Test]
        public async void Initialize()
        {
            var token = await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            Assert.IsNotEmpty(token);

            await client.CloseAsync();
        }

        [Test]
        public async void Close()
        {
            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            await client.CloseAsync();

            //
        }

        [Test]
        public async void PersistToken()
        {
            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            await client.GetCurrentAccountAsync();
            await client.CloseAsync();
        }

        [Test]
        public async void Reauthenticate()
        {
        }

        [Test]
        public async void FailOnbehalfof()
        {
        }

        [Test]
        public async void RunOnbehalfof()
        {
        }

        [Test]
        public async void FailWithError()
        {
        }
    }
}
