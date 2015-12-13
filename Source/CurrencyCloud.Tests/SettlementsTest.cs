using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

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
        public async void Create()
        {
            player.Play("Create");
        }

        /// <summary>
        /// Successfully gets a settlement.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            //var account1 = Accounts.Account1;

            //Account created = await client.CreateAccountAsync(account1.AccountName, account1.LegalEntityType, account1.Optional);
            //Assert.IsTrue(AreEqual(account1, created));
        }

        /// <summary>
        /// Successfully finds a settlement.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");
        }

        /// <summary>
        /// Successfully deletes a settlement.
        /// </summary>
        [Test]
        public async void Delete()
        {
            player.Play("Delete");
        }

        /// <summary>
        /// Successfully adds conversion to a settlement.
        /// </summary>
        [Test]
        public async void AddConversion()
        {
            player.Play("AddConversion");
        }

        /// <summary>
        /// Successfully removes conversion from a settlement.
        /// </summary>
        [Test]
        public async void RemoveConversion()
        {
            player.Play("RemoveConversion");
        }

        /// <summary>
        /// Successfully releases a settlement.
        /// </summary>
        [Test]
        public async void Release()
        {
            player.Play("Release");
        }

        /// <summary>
        /// Successfully unreleases a settlement.
        /// </summary>
        [Test]
        public async void Unrelease()
        {
            player.Play("Unrelease");
        }
    }
}
