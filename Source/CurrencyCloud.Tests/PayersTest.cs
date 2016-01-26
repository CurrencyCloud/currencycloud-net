using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class PayersTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Payers.json");

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
        /// Successfully gets a payer.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var conversion1 = Conversions.Conversion1;
            var beneficiary1 = Beneficiaries.Beneficiary1;
            var payment1 = Payments.Payment1;

            Conversion conversion = await client.CreateConversionAsync(conversion1.BuyCurrency, conversion1.SellCurrency, conversion1.FixedSide, conversion1.Amount, conversion1.Reason, conversion1.TermAgreement);
            Beneficiary beneficiary = await client.CreateBeneficiaryAsync(beneficiary1);

            dynamic paymentOptional1 = new ParamsObject(payment1.Optional);
            paymentOptional1.ConversionId = conversion.Id;

            Payment payment = await client.CreatePaymentAsync(payment1.Currency, beneficiary.Id, payment1.Amount, payment1.Reason, payment1.Reference, paymentOptional1);
            Payer gotten = await client.GetPayerAsync(payment.PayerId);

            Assert.AreEqual(payment1.Optional.PayerCompanyName, gotten.CompanyName);
            Assert.AreEqual(payment1.Optional.PayerFirstName, gotten.FirstName);
            Assert.AreEqual(payment1.Optional.PayerLastName, gotten.LastName);
            Assert.AreEqual(payment1.Optional.PayerCity, gotten.City);
            Assert.AreEqual(payment1.Optional.PayerAddress, gotten.Address);
            Assert.AreEqual(payment1.Optional.PayerPostcode, gotten.Postcode);
            Assert.AreEqual(payment1.Optional.PayerStateOrProvince, gotten.StateOrProvince);
            Assert.AreEqual(payment1.Optional.PayerCountry, gotten.Country);
            Assert.AreEqual(payment1.Optional.PayerDateOfBirth, gotten.DateOfBirth);
            Assert.AreEqual(payment1.Optional.PayerIdentificationType, gotten.IdentificationType);
        }
    }
}
