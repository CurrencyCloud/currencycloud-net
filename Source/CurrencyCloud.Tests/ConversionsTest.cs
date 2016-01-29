using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Collections;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ConversionsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Conversions.json");

        [TestFixtureSetUp]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;

            client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey).Wait();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            player.Play("TearDown");

            client.CloseAsync().Wait();

            player.Close();
        }

        /// <summary>
        /// Successfully creates a conversion.
        /// </summary>
        [Test]
        public async void Create()
        {
            player.Play("Create");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);

            Assert.AreEqual(conversion1.BuyCurrency, created.BuyCurrency);
            Assert.AreEqual(conversion1.SellCurrency, created.SellCurrency);
            Assert.AreEqual(conversion1.FixedSide, created.FixedSide);
        }

        /// <summary>
        /// Successfully gets a conversion.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            Conversion gotten = await client.GetConversionAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully finds a conversion.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            PaginatedConversions found = await client.FindConversionsAsync(new ConversionFindParameters
            {
                ConversionIds = new string[]
                {
                    created.Id
                },
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.desc,
                PerPage = 5
            });

            Assert.Contains(created, found.Conversions);
        }
    }
}
