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
    class BeneficiariesTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Beneficiaries.json");

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
        /// Successfully validates a beneficiary.
        /// </summary>
        [Test]
        public async void Validate()
        {
            player.Play("Validate");

            Beneficiary validated = await client.ValidateBeneficiaryAsync("GB", "GBP", "GB", new
            {
                AccountNumber = "13071472",
                RoutingCodeType1 = "sort_code",
                RoutingCodeValue1 = "200605"
            });

            Assert.IsNull(validated.Id);
        }

        /// <summary>
        /// Successfully creates a beneficiary.
        /// </summary>
        [Test]
        public async void Create()
        {
            player.Play("Create");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1.BankAccountHolderName, beneficiary1.BankCountry, beneficiary1.Currency, beneficiary1.Name, beneficiary1.Optional);

            Assert.AreEqual(beneficiary1.BankAccountHolderName, created.BankAccountHolderName);
            Assert.AreEqual(beneficiary1.BankCountry, created.BankCountry);
            Assert.AreEqual(beneficiary1.Currency, created.Currency);
            Assert.AreEqual(beneficiary1.Name, created.Name);
            Assert.Contains(beneficiary1.Optional.BeneficiaryAddress, created.BeneficiaryAddress);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryCountry, created.BeneficiaryCountry);
            Assert.AreEqual(beneficiary1.Optional.BicSwift, created.BicSwift);
            Assert.AreEqual(beneficiary1.Optional.Iban, created.Iban);
            Assert.AreEqual(beneficiary1.Optional.DefaultBeneficiary, created.DefaultBeneficiary);
            Assert.Contains(beneficiary1.Optional.BankAddress, created.BankAddress);
            Assert.AreEqual(beneficiary1.Optional.BankName, created.BankName);
            Assert.AreEqual(beneficiary1.Optional.BankAccountType, created.BankAccountType);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryEntityType, created.BeneficiaryEntityType);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryCompanyName, created.BeneficiaryCompanyName);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryFirstName, created.BeneficiaryFirstName);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryLastName, created.BeneficiaryLastName);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryCity, created.BeneficiaryCity);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryPostcode, created.BeneficiaryPostcode);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryStateOrProvince, created.BeneficiaryStateOrProvince);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryDateOfBirth, created.BeneficiaryDateOfBirth);
            Assert.AreEqual(beneficiary1.Optional.BeneficiaryIdentificationType, created.BeneficiaryIdentificationType);
        }

        /// <summary>
        /// Successfully gets a beneficiary.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1.BankAccountHolderName, beneficiary1.BankCountry, beneficiary1.Currency, beneficiary1.Name, beneficiary1.Optional);
            Beneficiary gotten = await client.GetBeneficiaryAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully updates a beneficiary.
        /// </summary>
        [Test]
        public async void Update()
        {
            player.Play("Update");

            var beneficiary1 = Beneficiaries.Beneficiary1;
            var beneficiary2 = Beneficiaries.Beneficiary2;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1.BankAccountHolderName, beneficiary1.BankCountry, beneficiary1.Currency, beneficiary1.Name, beneficiary1.Optional);
            Beneficiary updated = await client.UpdateBeneficiaryAsync(created.Id, beneficiary2);
            Beneficiary gotten = await client.GetBeneficiaryAsync(created.Id);

            Assert.AreEqual(gotten, updated);
        }

        /// <summary>
        /// Successfully finds a beneficiary.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1.BankAccountHolderName, beneficiary1.BankCountry, beneficiary1.Currency, beneficiary1.Name, beneficiary1.Optional);
            PaginatedBeneficiaries found = await client.FindBeneficiariesAsync(new
            {
                Name = created.Name,
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            });

            Assert.Contains(created, found.Beneficiaries);
        }

        /// <summary>
        /// Successfully deletes a beneficiary.
        /// </summary>
        [Test]
        public async void Delete()
        {
            player.Play("Delete");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1.BankAccountHolderName, beneficiary1.BankCountry, beneficiary1.Currency, beneficiary1.Name, beneficiary1.Optional);
            Beneficiary deleted = await client.DeleteBeneficiaryAsync(created.Id);

            Assert.AreEqual(created, deleted);

            try
            {
                await client.GetBeneficiaryAsync(created.Id);

                Assert.Fail();
            }
            catch (System.Exception ex)
            {
                Assert.IsInstanceOf(typeof(NotFoundException), ex);
            }
        }
    }
}
