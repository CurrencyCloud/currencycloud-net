using System;
using System.Threading.Tasks;
using CurrencyCloud.Entity;
using CurrencyCloud.Environment;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using NUnit.Framework;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class CollectionsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Collections.json");

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;
            client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey).Wait();
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            player.Play("TearDown");

            client.CloseAsync().Wait();

            player.Close();
        }

        /// <summary>
        /// Successfully completes collections screening by accepting a transaction.
        /// </summary>
        [Test]
        public async Task CompleteCollectionsScreeningAccept()
        {
            player.Play("CompleteCollectionsScreeningAccept");

            string transactionId = "bdcca5e6-32fe-45f6-9476-6f8f518e6270";
            bool accepted = true;
            string reason = "accepted";

            CollectionsScreeningResult result = await client.CompleteCollectionsScreeningAsync(transactionId, accepted, reason);

            Assert.AreEqual(transactionId, result.TransactionId);
            Assert.AreEqual("7a116d7d-6310-40ae-8d54-0ffbe41dc1c9", result.AccountId);
            Assert.AreEqual("7a116d7d-6310-40ae-8d54-0ffbe41dc1c9", result.HouseAccountId);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual("Accepted", result.Result.Reason);
            Assert.IsTrue(result.Result.Accepted);
        }

        /// <summary>
        /// Successfully completes collections screening by rejecting a transaction.
        /// </summary>
        [Test]
        public async Task CompleteCollectionsScreeningReject()
        {
            player.Play("CompleteCollectionsScreeningReject");

            string transactionId = "bdcca5e6-32fe-45f6-9476-6f8f518e6270";
            bool accepted = false;
            string reason = "suspected_fraud";

            CollectionsScreeningResult result = await client.CompleteCollectionsScreeningAsync(transactionId, accepted, reason);

            Assert.AreEqual(transactionId, result.TransactionId);
            Assert.AreEqual("7a116d7d-6310-40ae-8d54-0ffbe41dc1c9", result.AccountId);
            Assert.AreEqual("7a116d7d-6310-40ae-8d54-0ffbe41dc1c9", result.HouseAccountId);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual("suspected_fraud", result.Result.Reason);
            Assert.IsFalse(result.Result.Accepted);
        }
    }
}
