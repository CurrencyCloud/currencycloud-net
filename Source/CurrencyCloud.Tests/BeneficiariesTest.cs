using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Exception;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class BeneficiariesTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Beneficiaries.json");

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
        /// Successfully validates a beneficiary.
        /// </summary>
        [Test]
        public async Task Validate()
        {
            player.Play("Validate");

            Beneficiary validated = await client.ValidateBeneficiaryAsync(new Beneficiary
            {
                BankCountry = "GB",
                Currency = "GBP",
                AccountNumber = "13071472",
                RoutingCodeType1 = "sort_code",
                RoutingCodeValue1 = "200605",
                PaymentTypes = new string[] { "regular", "priority" }
            });

            Assert.IsNull(validated.Id);
        }

        /// <summary>
        /// Successfully creates a beneficiary.
        /// </summary>
        [Test]
        public async Task Create()
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
            Assert.AreEqual(beneficiary1.CompanyWebsite, created.CompanyWebsite);
            Assert.AreEqual(beneficiary1.BusinessNature, created.BusinessNature);
        }

        /// <summary>
        /// Successfully gets a beneficiary.
        /// </summary>
        [Test]
        public async Task Get()
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
        public async Task Update()
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
        /// Successfully finds a beneficiary with search paramaters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1);
            PaginatedBeneficiaries found = await client.FindBeneficiariesAsync(new BeneficiaryFindParameters
            {
                Name = created.Name,
                Order = "created_at",
                OrderAscDesc = BeneficiaryFindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.Contains(created, found.Beneficiaries);
        }

        /// <summary>
        /// Successfully finds a beneficiary without search paramaters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var beneficiary1 = Beneficiaries.Beneficiary1;

            Beneficiary created = await client.CreateBeneficiaryAsync(beneficiary1);
            PaginatedBeneficiaries found = await client.FindBeneficiariesAsync();

            Assert.Contains(created, found.Beneficiaries);
        }

        /// <summary>
        /// Successfully deletes a beneficiary.
        /// </summary>
        [Test]
        public async Task Delete()
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
