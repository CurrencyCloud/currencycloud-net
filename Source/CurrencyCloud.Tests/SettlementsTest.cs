using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Exception;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class SettlementsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Settlements.json");

        [TestFixtureSetUp]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;

            client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey).Wait();
        }

        [TestFixtureTearDown]
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

            Assert.DoesNotThrow(async () =>
            {
                Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
                {
                    Type = "net"
                }));
            });
        }

        /// <summary>
        /// Successfully gets a settlement.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
            {
                Type = "net"
            }));
            Settlement gotten = await client.GetSettlementAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully finds a settlement.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
            {
                Type = "net"
            }));
            PaginatedSettlements found = await client.FindSettlementsAsync(new ParamsObject(new
            {
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            }));

            Assert.Contains(created, found.Settlements);
        }

        /// <summary>
        /// Successfully deletes a settlement.
        /// </summary>
        [Test]
        public async void Delete()
        {
            player.Play("Delete");

            Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
            {
                Type = "net"
            }));
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
        public async void AddConversion()
        {
            player.Play("AddConversion");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
            {
                Type = "net"
            }));
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);

            Assert.Contains(conversion.Id, updated.ConversionIds);
            Assert.AreEqual(1, updated.ConversionIds.Count);
        }

        /// <summary>
        /// Successfully removes conversion from a settlement.
        /// </summary>
        [Test]
        public async void RemoveConversion()
        {
            player.Play("RemoveConversion");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
            {
                Type = "net"
            }));
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);
            updated = await client.RemoveConversionFromSettlementAsync(created.Id, conversion.Id);

            Assert.IsEmpty(updated.ConversionIds);
        }

        /// <summary>
        /// Successfully releases a settlement.
        /// </summary>
        [Test]
        public async void Release()
        {
            player.Play("Release");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
            {
                Type = "net"
            }));
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);
            updated = await client.ReleaseSettlementAsync(created.Id);

            Assert.AreEqual("released", updated.Status);
        }

        /// <summary>
        /// Successfully unreleases a settlement.
        /// </summary>
        [Test]
        public async void Unrelease()
        {
            player.Play("Unrelease");

            var conversion1 = Conversions.Conversion1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Settlement created = await client.CreateSettlementAsync(new ParamsObject(new
            {
                Type = "net"
            }));
            Settlement updated = await client.AddConversionToSettlementAsync(created.Id, conversion.Id);
            updated = await client.ReleaseSettlementAsync(created.Id);
            updated = await client.UnreleaseSettlementAsync(created.Id);

            Assert.AreEqual("open", updated.Status);
        }
    }
}
