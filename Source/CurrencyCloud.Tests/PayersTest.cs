using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class PayersTest
    {
        Client client = new Client();
        Player player = new Player("Mock/Http/Recordings/Payers.json");

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
        /// Successfully gets a payer.
        /// </summary>
        [Test]
        public async Task Get()
        {
            player.Play("Get");

            var conversion1 = Conversions.Conversion1;
            var beneficiary1 = Beneficiaries.Beneficiary1;
            var payment1 = Payments.Payment1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Beneficiary beneficiary = await client.CreateBeneficiaryAsync(beneficiary1);

            payment1.BeneficiaryId = beneficiary.Id;
            payment1.ConversionId = conversion.Id;

            Payment payment = await client.CreatePaymentAsync(payment1,Payments.Payer1);
            Payer gotten = await client.GetPayerAsync(payment.PayerId);

            Assert.That(gotten.CompanyName, Is.EqualTo(Payments.Payer1.CompanyName));
            Assert.That(gotten.FirstName, Is.EqualTo(Payments.Payer1.FirstName));
            Assert.That(gotten.LastName, Is.EqualTo(Payments.Payer1.LastName));
            Assert.That(gotten.City, Is.EqualTo(Payments.Payer1.City));
            Assert.That(gotten.Address, Is.EqualTo(Payments.Payer1.Address));
            Assert.That(gotten.Postcode, Is.EqualTo(Payments.Payer1.Postcode));
            Assert.That(gotten.StateOrProvince, Is.EqualTo(Payments.Payer1.StateOrProvince));
            Assert.That(gotten.Country, Is.EqualTo(Payments.Payer1.Country));
            Assert.That(gotten.DateOfBirth, Is.EqualTo(Payments.Payer1.DateOfBirth));
            Assert.That(gotten.IdentificationType, Is.EqualTo(Payments.Payer1.IdentificationType));
        }
    }
}
