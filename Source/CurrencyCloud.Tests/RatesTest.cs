using System;
using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity.List;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class RatesTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Rates.json");

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
        /// Successfully gets a rate.
        /// </summary>
        [Test]
        public void Get()
        {
            player.Play("Get");

            Assert.DoesNotThrowAsync(async () => {
                Rate gotten = await client.GetRateAsync(new DetailedRates("EUR", "GBP", "buy", 6700));
            });
        }

        /// <summary>
        /// Successfully gets a rate.
        /// </summary>
        [Test]
        public void GetWithConversionDatePreference()
        {
            player.Play("GetWithConversionDatePreference");

            Assert.DoesNotThrowAsync(async () =>
            {
                var request = new DetailedRates("GBP", "USD", "buy", 10000);
                request.ConversionDatePreference = "optimize_liquidity";
                var gotten = await client.GetRateAsync(request);
                Assert.That(gotten, Is.Not.Null);
                Assert.AreEqual(14081.0, gotten.ClientSellAmount);
                Assert.AreEqual(DateTime.Parse("2020-05-21T14:00:00"), gotten.SettlementCutOffTime);
            });
        }

        /// <summary>
        /// Successfully finds a rate.
        /// </summary>
        [Test]
        public void Find()
        {
            player.Play("Find");

            Assert.DoesNotThrowAsync(async () => {
                RatesList found = await client.FindRatesAsync("USDGBP");
            });
        }
    }
}
