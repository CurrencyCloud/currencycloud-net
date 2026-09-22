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
        Player player = new Player("Mock/Http/Recordings/Beneficiaries.json");

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

            Assert.That(validated.Id, Is.Null);
        }

        /// <summary>
        /// Successfully verifies a account.
        /// </summary>
        [Test]
        public async Task AccountVerification()
        {
            player.Play("AccountVerification");

            BeneficiaryAccountVerification accountVerification = await client.VerifyAccountAsync(new BeneficiaryAccountVerificationRequest
            {
                BankCountry = "GB",
                AccountNumber = "1234567890",
                RoutingCodeValue1 = "123456",
                BeneficiaryEntityType = "individual",
                BeneficiaryFirstName = "Test",
                BeneficiaryLastName = "User",
                PaymentType = "regular",
                Currency = "GBP",
                Iban = "GB33BUKB20201555555555",
                
            });

            Assert.That(accountVerification.Answer, Is.EqualTo("full_match"));
            Assert.That(accountVerification.ReasonType, Is.EqualTo("okay"));
            Assert.That(accountVerification.ActualName, Is.EqualTo("Test User"));
            Assert.That(accountVerification.ReasonCode, Is.EqualTo("FMCH"));
            Assert.That(accountVerification.Reason, Is.EqualTo("Full match"));
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

            Assert.That(created.BankAccountHolderName, Is.EqualTo(beneficiary1.BankAccountHolderName));
            Assert.That(created.BankCountry, Is.EqualTo(beneficiary1.BankCountry));
            Assert.That(created.Currency, Is.EqualTo(beneficiary1.Currency));
            Assert.That(created.Name, Is.EqualTo(beneficiary1.Name));
            Assert.That(created.BeneficiaryAddress, Does.Contain(beneficiary1.BeneficiaryAddress[0]));
            Assert.That(created.BeneficiaryCountry, Is.EqualTo(beneficiary1.BeneficiaryCountry));
            Assert.That(created.BicSwift, Is.EqualTo(beneficiary1.BicSwift));
            Assert.That(created.Iban, Is.EqualTo(beneficiary1.Iban));
            Assert.That(created.DefaultBeneficiary, Is.EqualTo(beneficiary1.DefaultBeneficiary));
            Assert.That(created.BankAddress, Does.Contain(beneficiary1.BankAddress[0]));
            Assert.That(created.BankName, Is.EqualTo(beneficiary1.BankName));
            Assert.That(created.BankAccountType, Is.EqualTo(beneficiary1.BankAccountType));
            Assert.That(created.BeneficiaryEntityType, Is.EqualTo(beneficiary1.BeneficiaryEntityType));
            Assert.That(created.BeneficiaryCompanyName, Is.EqualTo(beneficiary1.BeneficiaryCompanyName));
            Assert.That(created.BeneficiaryFirstName, Is.EqualTo(beneficiary1.BeneficiaryFirstName));
            Assert.That(created.BeneficiaryLastName, Is.EqualTo(beneficiary1.BeneficiaryLastName));
            Assert.That(created.BeneficiaryCity, Is.EqualTo(beneficiary1.BeneficiaryCity));
            Assert.That(created.BeneficiaryPostcode, Is.EqualTo(beneficiary1.BeneficiaryPostcode));
            Assert.That(created.BeneficiaryStateOrProvince, Is.EqualTo(beneficiary1.BeneficiaryStateOrProvince));
            Assert.That(created.BeneficiaryDateOfBirth, Is.EqualTo(beneficiary1.BeneficiaryDateOfBirth));
            Assert.That(created.BeneficiaryIdentificationType, Is.EqualTo(beneficiary1.BeneficiaryIdentificationType));
            Assert.That(created.CompanyWebsite, Is.EqualTo(beneficiary1.CompanyWebsite));
            Assert.That(created.BusinessNature, Is.EqualTo(beneficiary1.BusinessNature));
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

            Assert.That(created, Is.EqualTo(gotten));
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

            Assert.That(updated, Is.EqualTo(gotten));
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

            Assert.That(found.Beneficiaries, Does.Contain(created));
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

            Assert.That(found.Beneficiaries, Does.Contain(created));
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



            Assert.That(deleted, Is.EqualTo(created));

            try
            {
                await client.GetBeneficiaryAsync(created.Id);

                Assert.Fail();
            }
            catch (System.Exception ex)
            {
                Assert.That(ex, Is.InstanceOf(typeof(NotFoundException)));
            }
        }
    }
}
