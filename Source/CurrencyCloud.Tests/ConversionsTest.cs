using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;
using System;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ConversionsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Conversions.json");

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
        /// Successfully creates a conversion.
        /// </summary>
        [Test]
        public async Task Create()
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
        public async Task Get()
        {
            player.Play("Get");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            Conversion gotten = await client.GetConversionAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully finds a conversion with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            PaginatedConversions found = await client.FindConversionsAsync(new ConversionFindParameters
            {
                ConversionIds = new string[]
                {
                    created.Id
                },
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.Contains(created, found.Conversions);
        }

        /// <summary>
        /// Successfully finds a conversion without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            PaginatedConversions found = await client.FindConversionsAsync();

            Assert.Contains(created, found.Conversions);
        }
        
        /// <summary>
        /// Successfully cancels a conversion.
        /// </summary>
        [Test]
        public async Task Cancel()
        {
            player.Play("Cancel");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            ConversionCancellation cancelled = await client.CancelConversionsAsync(created.Id, "some notes");

            Assert.AreEqual(cancelled.ConversionId, created.Id);
        }


        /// <summary>
        /// Successfully changes the date of a conversion.
        /// </summary>
        [Test]
        public async Task DateChange()
        {
            player.Play("DateChange");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);

            DateTime newSettlementDate = DateTime.Parse("2017-11-10T12:18:56+00:00");
            ConversionDateChange dateChanged = await client.DateChangeConversionsAsync(created.Id, newSettlementDate);

            Assert.AreEqual(dateChanged.NewSettlementDate, newSettlementDate);
        }


        /// <summary>
        /// Successfully splits a conversion.
        /// </summary>
        [Test]
        public async Task Split()
        {
            player.Play("Split");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            ConversionSplit split = await client.SplitConversionsAsync(created.Id, 100);

            Assert.AreEqual(split.ParentConversion.Id, created.Id);
        }
    }
}
