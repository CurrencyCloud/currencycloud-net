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
    class VirtualAccountsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/VirtualAccounts.json");

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
        /// Successfully finds VANS.
        /// </summary>
        [Test]
        public async Task Find()
        {
            player.Play("Find");

            var van = VirtualAccounts.Van1;

            PaginatedVirtualAccounts found = await client.FindVirtualAccountsAsync();

            Assert.AreEqual(van.Id, found.VirtualAccounts[0].Id);
            Assert.AreEqual(van.VirtualAccountNumber, found.VirtualAccounts[0].VirtualAccountNumber);
            Assert.AreEqual(van.AccountId, found.VirtualAccounts[0].AccountId);
            Assert.AreEqual(van.AccountHolderName, found.VirtualAccounts[0].AccountHolderName);
            Assert.AreEqual(van.BankInstitutionName, found.VirtualAccounts[0].BankInstitutionName);
            Assert.AreEqual(van.BankInstitutionAddress, found.VirtualAccounts[0].BankInstitutionAddress);
            Assert.AreEqual(van.BankInstitutionCountry, found.VirtualAccounts[0].BankInstitutionCountry);
            Assert.AreEqual(van.RoutingCode, found.VirtualAccounts[0].RoutingCode);
        }

    }
}