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
        Player player = new Player("Mock/Http/Recordings/VirtualAccounts.json");

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

            Assert.That(found.VirtualAccounts[0].Id, Is.EqualTo(van.Id));
            Assert.That(found.VirtualAccounts[0].VirtualAccountNumber, Is.EqualTo(van.VirtualAccountNumber));
            Assert.That(found.VirtualAccounts[0].AccountId, Is.EqualTo(van.AccountId));
            Assert.That(found.VirtualAccounts[0].AccountHolderName, Is.EqualTo(van.AccountHolderName));
            Assert.That(found.VirtualAccounts[0].BankInstitutionName, Is.EqualTo(van.BankInstitutionName));
            Assert.That(found.VirtualAccounts[0].BankInstitutionAddress, Is.EqualTo(van.BankInstitutionAddress));
            Assert.That(found.VirtualAccounts[0].BankInstitutionCountry, Is.EqualTo(van.BankInstitutionCountry));
            Assert.That(found.VirtualAccounts[0].RoutingCode, Is.EqualTo(van.RoutingCode));
        }

    }
}