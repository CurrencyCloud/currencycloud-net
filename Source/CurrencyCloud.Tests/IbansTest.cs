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
    class IbansTest
    {
        Client client = new Client();
        Player player = new Player("Mock/Http/Recordings/Ibans.json");

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
        /// Successfully finds IBANS with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var iban = Ibans.Iban1;

            PaginatedIbans found = await client.FindIbansAsync(new IbanFindParameters
            {
                Currency = iban.Currency
            });

            Assert.That(found.Ibans[0].Id, Is.EqualTo(iban.Id));
            Assert.That(found.Ibans[0].IbanCode, Is.EqualTo(iban.IbanCode));
            Assert.That(found.Ibans[0].AccountId, Is.EqualTo(iban.AccountId));
            Assert.That(found.Ibans[0].Currency, Is.EqualTo(iban.Currency));
            Assert.That(found.Ibans[0].AccountHolderName, Is.EqualTo(iban.AccountHolderName));
            Assert.That(found.Ibans[0].BankInstitutionName, Is.EqualTo(iban.BankInstitutionName));
            Assert.That(found.Ibans[0].BankInstitutionAddress, Is.EqualTo(iban.BankInstitutionAddress));
            Assert.That(found.Ibans[0].BankInstitutionCountry, Is.EqualTo(iban.BankInstitutionCountry));
            Assert.That(found.Ibans[0].BicSwift, Is.EqualTo(iban.BicSwift));
        }

        /// <summary>
        /// Successfully finds IBANS without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var iban = Ibans.Iban1;

            PaginatedIbans found = await client.FindIbansAsync();

            Assert.That(found.Ibans[0].Id, Is.EqualTo(iban.Id));
            Assert.That(found.Ibans[0].IbanCode, Is.EqualTo(iban.IbanCode));
            Assert.That(found.Ibans[0].AccountId, Is.EqualTo(iban.AccountId));
            Assert.That(found.Ibans[0].Currency, Is.EqualTo(iban.Currency));
            Assert.That(found.Ibans[0].AccountHolderName, Is.EqualTo(iban.AccountHolderName));
            Assert.That(found.Ibans[0].BankInstitutionName, Is.EqualTo(iban.BankInstitutionName));
            Assert.That(found.Ibans[0].BankInstitutionAddress, Is.EqualTo(iban.BankInstitutionAddress));
            Assert.That(found.Ibans[0].BankInstitutionCountry, Is.EqualTo(iban.BankInstitutionCountry));
            Assert.That(found.Ibans[0].BicSwift, Is.EqualTo(iban.BicSwift));
        }

    }
}
