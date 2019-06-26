using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Exception;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class BackoffRetryTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/BackoffRetry.json");
        Credentials credentials = new Credentials("development@currencycloud.com", "deadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeef");

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            Retry.Enabled = true;
            Retry.NumRetries = 3;
            Retry.MinWait = 10;
            Retry.MaxWait = 100;
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            player.Close();
            Retry.Enabled = false;
        }

        /// <summary>
        /// Unauthorized Access Error (401)
        /// </summary>
        [Test]
        public void AuthenticationError()
        {
            player.Play("AuthenticationError");

            var credentials = new Credentials("nobody@nowhere.com", "deadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeefdeadbeef");
            Assert.ThrowsAsync<AuthenticationException>(async () => await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey));
        }

        /// <summary>
        /// Too Many Requests Error (429)
        /// </summary>
        [Test]
        public void TooManyRequestsError()
        {
            player.Play("TooManyRequestsError");
            client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey).Wait();

            Assert.ThrowsAsync<TooManyRequestsException>(async () => await client.GetCurrentAccountAsync());
        }

        /// <summary>
        /// Internal Server Error (500)
        /// </summary>
        [Test]
        public void InternalApplicationError()
        {
            player.Play("InternalApplicationError");
            Assert.ThrowsAsync<InternalApplicationException>(async () => await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey));
        }
    }
}
