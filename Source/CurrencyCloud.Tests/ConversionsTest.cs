using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;

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
    }
}
