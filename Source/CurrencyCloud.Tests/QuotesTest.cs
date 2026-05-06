using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;
using System;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class QuotesTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Quotes.json");

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
        /// Successfully creates a quote.
        /// </summary>
        [Test]
        public async Task Create()
        {
            player.Play("Create");

            var quote1 = Quotes.Quote1;

            Quote created = await client.CreateQuoteAsync(quote1);

            Assert.AreEqual(quote1.BuyCurrency, created.BuyCurrency);
            Assert.AreEqual(quote1.SellCurrency, created.SellCurrency);
            Assert.AreEqual(quote1.FixedSide, created.FixedSide);
            Assert.IsNotNull(created.QuoteId);
        }
    }
}
