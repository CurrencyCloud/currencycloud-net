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
    public class EmulatorsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Emulators.json");

        [OneTimeSetUp]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;
            client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey).Wait();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            player.Play("TearDown");

            client.CloseAsync().Wait();

            player.Close();
        }

        /// <summary>
        /// Successfully emulates inbound funds.
        /// </summary>
        [Test]
        public async Task EmulateInboundFunds()
        {
            player.Play("EmulateInboundFunds");

            var funding = new DemoFunding(
                              amount: 150.53m,
                              currency: "USD",
                              receiverAccountNumber: "0334273394"
                          )
            {
                Id = "8bd7ba19-eca0-425d-a3f0-968577ba2a81", // Override for testing purposes
                SenderName = "Test sender",
                SenderAddress = "Some Street",
                SenderCountry = "GB",
                SenderReference = "sender-ref",
                ReceiverRoutingCode = "026073150",
                Action = "approve"
            };

            DemoFunding result = await client.EmulateFundingAsync(funding);

            Assert.AreEqual("8bd7ba19-eca0-425d-a3f0-968577ba2a81", result.Id);
            Assert.AreEqual("72970a7c-7921-431c-b95f-3438724ba16f", result.AccountId);
            Assert.AreEqual("approved", result.State);
            Assert.AreEqual("Test sender", result.SenderName);
            Assert.AreEqual("Some Street", result.SenderAddress);
            Assert.AreEqual("GB", result.SenderCountry);
            Assert.AreEqual("sender-ref", result.SenderReference);
            Assert.AreEqual("0334273394", result.ReceiverAccountNumber);
            Assert.AreEqual("026073150", result.ReceiverRoutingCode);
            Assert.AreEqual(150.53m, result.Amount);
            Assert.AreEqual("USD", result.Currency);
            Assert.AreEqual("approve", result.Action);
            Assert.AreEqual("IF-20210917-17HB4L", result.ShortReference);
            Assert.AreEqual(DateTime.Parse("2021-09-17T15:35:17+00:00"), result.CreatedAt);
            Assert.AreEqual(DateTime.Parse("2021-09-17T15:35:18+00:00"), result.UpdatedAt);
        }
    }
}