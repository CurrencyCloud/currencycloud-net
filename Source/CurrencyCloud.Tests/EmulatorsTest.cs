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
        Player player = new Player("Mock/Http/Recordings/Emulators.json");

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

            Assert.That(result.Id, Is.EqualTo("8bd7ba19-eca0-425d-a3f0-968577ba2a81"));
            Assert.That(result.AccountId, Is.EqualTo("72970a7c-7921-431c-b95f-3438724ba16f"));
            Assert.That(result.State, Is.EqualTo("approved"));
            Assert.That(result.SenderName, Is.EqualTo("Test sender"));
            Assert.That(result.SenderAddress, Is.EqualTo("Some Street"));
            Assert.That(result.SenderCountry, Is.EqualTo("GB"));
            Assert.That(result.SenderReference, Is.EqualTo("sender-ref"));
            Assert.That(result.ReceiverAccountNumber, Is.EqualTo("0334273394"));
            Assert.That(result.ReceiverRoutingCode, Is.EqualTo("026073150"));
            Assert.That(result.Amount, Is.EqualTo(150.53m));
            Assert.That(result.Currency, Is.EqualTo("USD"));
            Assert.That(result.Action, Is.EqualTo("approve"));
            Assert.That(result.ShortReference, Is.EqualTo("IF-20210917-17HB4L"));
            Assert.That(result.CreatedAt, Is.EqualTo(DateTime.Parse("2021-09-17T15:35:17+00:00")));
            Assert.That(result.UpdatedAt, Is.EqualTo(DateTime.Parse("2021-09-17T15:35:18+00:00")));
        }
    }
}