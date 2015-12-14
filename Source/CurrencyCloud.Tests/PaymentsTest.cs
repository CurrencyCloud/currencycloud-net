using System.Threading.Tasks;
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
    class PaymentsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Payments.json");

        private async Task<Payment> CreatePayment(dynamic payment)
        {
            var conversion1 = Conversions.Conversion1;
            var beneficiary1 = Beneficiaries.Beneficiary1;

            Conversion conversion = await client.CreateConversionAsync(conversion1.BuyCurrency, conversion1.SellCurrency, conversion1.FixedSide, conversion1.Amount, conversion1.reason, conversion1.TermAgreement);
            Beneficiary beneficiary = await client.CreateBeneficiaryAsync(beneficiary1.BankAccountHolderName, beneficiary1.BankCountry, beneficiary1.Currency, beneficiary1.Name, beneficiary1.Optional);

            payment.Optional.ConversionId = conversion.Id;

            return await client.CreatePaymentAsync(payment.Currency, beneficiary.Id, payment.Amount, payment.Reason, payment.reference, payment.Optional);
        }

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
        /// Successfully creates a payment.
        /// </summary>
        [Test]
        public async void Create()
        {
            player.Play("Create");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);

            Assert.AreEqual(payment1.Currency, created.Currency);
            Assert.AreEqual(payment1.Amount, created.Amount);
            Assert.AreEqual(payment1.Reason, created.Reason);
            Assert.AreEqual(payment1.Reference, created.Reference);
        }

        /// <summary>
        /// Successfully gets a payment.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);
            Payment gotten = await client.GetPaymentAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully updates a payment.
        /// </summary>
        [Test]
        public async void Update()
        {
            player.Play("Update");

            var payment1 = Payments.Payment1;
            var payment2 = Payments.Payment2;

            Payment created = await CreatePayment(payment1);
            Payment updated = await client.UpdatePaymentAsync(created.Id, payment2);
            Payment gotten = await client.GetPaymentAsync(created.Id);

            Assert.AreEqual(gotten, updated);
        }

        /// <summary>
        /// Successfully finds a payment.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);
            PaginatedPayments found = await client.FindPaymentsAsync(new
            {
                BeneficiaryId = created.BeneficiaryId,
                ConversionId = created.ConversionId,
                Order = "created_at",
                OrderAscDesc = "desc",
                PerPage = 5
            });

            Assert.Contains(created, found.Payments);
        }

        /// <summary>
        /// Successfully deletes a payment.
        /// </summary>
        [Test]
        public async void Delete()
        {
            player.Play("Delete");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);
            Payment deleted = await client.DeletePaymentAsync(created.Id);

            Assert.AreEqual(created, deleted);

            try
            {
                await client.GetPaymentAsync(created.Id);

                Assert.Fail();
            }
            catch (System.Exception ex)
            {
                Assert.IsInstanceOf(typeof(NotFoundException), ex);
            }
        }
    }
}
