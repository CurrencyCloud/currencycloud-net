using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Exception;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class SettlementsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Settlements.json");

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
        /// Successfully creates a settlement.
        /// </summary>
        [Test]
        public void Create()
        {
            player.Play("Create");

            Assert.DoesNotThrowAsync(async () =>
            {
                Settlement created = await client.CreateSettlementAsync("net");
            });
        }

        /// <summary>
        /// Successfully gets a settlement.
        /// </summary>
        [Test]
        public async Task Get()
        {
            player.Play("Get");

            Settlement created = await client.CreateSettlementAsync("net");
            Settlement gotten = await client.GetSettlementAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully finds a settlement with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            Settlement created = await client.CreateSettlementAsync("net");
            PaginatedSettlements found = await client.FindSettlementsAsync(new SettlementFindParameters
            {
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.Contains(created, found.Settlements);
        }

        /// <summary>
        /// Successfully finds a settlement without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            Settlement created = await client.CreateSettlementAsync("net");
            PaginatedSettlements found = await client.FindSettlementsAsync();

            Assert.Contains(created, found.Settlements);
        }

        /// <summary>
        /// Successfully deletes a settlement.
        /// </summary>
        [Test]
        public async Task Delete()
        {
            player.Play("Delete");

            Settlement created = await client.CreateSettlementAsync("net");
            Settlement deleted = await client.DeleteSettlementAsync(created.Id);

            Assert.AreEqual(created, deleted);

            try
            {
                await client.GetSettlementAsync(created.Id);

                Assert.Fail();
            }
            catch (System.Exception ex)
            {
                Assert.IsInstanceOf(typeof(NotFoundException), ex);
            }
        }

        /// <summary>
        /// Successfully adds conversion to a settlement.
        /// </summary>
        [Test]
        public async Task AddConversion()
        {
            player.Play("AddConversion");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync("net");
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);

            Assert.Contains(conversion.Id, updated.ConversionIds);
            Assert.AreEqual(1, updated.ConversionIds.Count);
        }

        /// <summary>
        /// Successfully removes conversion from a settlement.
        /// </summary>
        [Test]
        public async Task RemoveConversion()
        {
            player.Play("RemoveConversion");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync("net");
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);
            updated = await client.RemoveConversionFromSettlementAsync(created.Id, conversion.Id);

            Assert.IsEmpty(updated.ConversionIds);
        }

        /// <summary>
        /// Successfully releases a settlement.
        /// </summary>
        [Test]
        public async Task Release()
        {
            player.Play("Release");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync("net");
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);
            updated = await client.ReleaseSettlementAsync(created.Id);

            Assert.AreEqual("released", updated.Status);
        }

        /// <summary>
        /// Successfully unreleases a settlement.
        /// </summary>
        [Test]
        public async Task Unrelease()
        {
            player.Play("Unrelease");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync("net");
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);
            updated = await client.ReleaseSettlementAsync(created.Id);
            updated = await client.UnreleaseSettlementAsync(created.Id);

            Assert.AreEqual("open", updated.Status);
        }
    }
}
