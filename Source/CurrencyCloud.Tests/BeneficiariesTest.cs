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
        /// Successfully validates a beneficiary.
        /// </summary>
        [Test]
        public async void Validate()
        {
            player.Play("Validate");

            Beneficiary validated = await client.ValidateBeneficiaryAsync(
                new BeneficiaryValidateParameters("GB", "GBP", "GB")
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

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1);

            Assert.AreEqual(beneficiary1.BankAccountHolderName, created.BankAccountHolderName);
            Assert.AreEqual(beneficiary1.BankCountry, created.BankCountry);
            Assert.AreEqual(beneficiary1.Currency, created.Currency);
            Assert.AreEqual(beneficiary1.Name, created.Name);
            Assert.Contains(beneficiary1.BeneficiaryAddress[0], created.BeneficiaryAddress);
            Assert.AreEqual(beneficiary1.BeneficiaryCountry, created.BeneficiaryCountry);
            Assert.AreEqual(beneficiary1.BicSwift, created.BicSwift);
            Assert.AreEqual(beneficiary1.Iban, created.Iban);
            Assert.AreEqual(beneficiary1.DefaultBeneficiary, created.DefaultBeneficiary);
            Assert.Contains(beneficiary1.BankAddress[0], created.BankAddress);
            Assert.AreEqual(beneficiary1.BankName, created.BankName);
            Assert.AreEqual(beneficiary1.BankAccountType, created.BankAccountType);
            Assert.AreEqual(beneficiary1.BeneficiaryEntityType, created.BeneficiaryEntityType);
            Assert.AreEqual(beneficiary1.BeneficiaryCompanyName, created.BeneficiaryCompanyName);
            Assert.AreEqual(beneficiary1.BeneficiaryFirstName, created.BeneficiaryFirstName);
            Assert.AreEqual(beneficiary1.BeneficiaryLastName, created.BeneficiaryLastName);
            Assert.AreEqual(beneficiary1.BeneficiaryCity, created.BeneficiaryCity);
            Assert.AreEqual(beneficiary1.BeneficiaryPostcode, created.BeneficiaryPostcode);
            Assert.AreEqual(beneficiary1.BeneficiaryStateOrProvince, created.BeneficiaryStateOrProvince);
            Assert.AreEqual(beneficiary1.BeneficiaryDateOfBirth, created.BeneficiaryDateOfBirth);
            Assert.AreEqual(beneficiary1.BeneficiaryIdentificationType, created.BeneficiaryIdentificationType);
        }

        /// <summary>
        /// Successfully gets a beneficiary.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1);
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

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1);
            beneficiary2.Id = created.Id;
            Beneficiary updated = await client.UpdateBeneficiaryAsync(beneficiary2);
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

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1);
            PaginatedBeneficiaries found = await client.FindBeneficiariesAsync(new BeneficiaryFindParameters
            {
                Name = created.Name,
                Order = "created_at",
                OrderAscDesc = BeneficiaryFindParameters.OrderDirection.desc,
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

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1);
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
