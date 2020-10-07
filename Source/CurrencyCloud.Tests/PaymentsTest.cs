using System;
using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Entity.List;
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
        Player player = new Player("/../../Mock/Http/Recordings/Payments.json");

        private async Task<Payment> CreatePayment(Entity.Payment payment)
        {
            var conversion1 = Conversions.Conversion1;
            var beneficiary1 = Beneficiaries.Beneficiary1;
            var payment1 = Payments.Payment1;

            Conversion conversion = await client.CreateConversionAsync(conversion1);
            Beneficiary beneficiary = await client.CreateBeneficiaryAsync(beneficiary1);

            payment1.ConversionId = conversion.Id;
            payment1.BeneficiaryId = beneficiary.Id;

            return await client.CreatePaymentAsync(payment1, Payments.Payer1);
        }

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
        /// Successfully creates a payment.
        /// </summary>
        [Test]
        public async Task Create()
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
        public async Task Get()
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
        public async Task Update()
        {
            player.Play("Update");

            var payment1 = Payments.Payment1;
            var payment2 = Payments.Payment2;

            Payment created = await CreatePayment(payment1);

            payment2.Id = created.Id;
            payment2.BeneficiaryId = created.BeneficiaryId;

            Payment updated = await client.UpdatePaymentAsync(payment2, Payments.Payer2);
            Payment gotten = await client.GetPaymentAsync(created.Id);

            Assert.AreEqual(gotten, updated);
        }

        /// <summary>
        /// Successfully gets a payment submission.
        /// </summary>
        [Test]
        public async Task GetSubmission()
        {
            player.Play("GetSubmission");

            var payment1 = Payments.Payment1;
            var submission1 = Payments.Submission1;

            Payment created = await CreatePayment(payment1);
            PaymentSubmission gotten = await client.GetPaymentSubmissionAsync(created.Id);

            Assert.AreEqual(gotten, submission1);
        }

        /// <summary>
        /// Successfully gets a confirmation for a payment.
        /// </summary>
        [Test]
        public async Task GetConfirmation()
        {
            player.Play("GetConfirmation");

            var payment1 = Payments.Payment1;
            var confirmation1 = Payments.Confirmation1;

            Payment created = await CreatePayment(payment1);
            PaymentConfirmation gotten = await client.GetPaymentConfirmationAsync(created.Id);

            Assert.AreEqual(gotten, confirmation1);
        }

        /// <summary>
        /// Authorises a payment in awaiting_authorisation state.
        /// </summary>
        [Test]
        public async Task Authorise()
        {
            player.Play("Authorise");

            PaymentAuthorisationsList gotten = await client.PaymentAuthorisationAsync(new[]
            {
                "8e3aeeb8-deeb-4665-96de-54b880a953ac",
                "f16cafe4-1f8f-472e-99d9-8c828918d4f8",
                "d025f90f-a23c-46f9-979a-35a9f98d9491"
            });

            Assert.AreEqual(gotten.Authorisations[0], Payments.Authorisation1);
            Assert.AreEqual(gotten.Authorisations[1], Payments.Authorisation2);
            Assert.AreEqual(gotten.Authorisations[2], Payments.Authorisation3);
        }

        /// <summary>
        /// Successfully finds a payment with search paramaters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);
            PaginatedPayments found = await client.FindPaymentsAsync(new PaymentFindParameters
            {
                BeneficiaryId = created.BeneficiaryId,
                ConversionId = created.ConversionId,
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.Contains(created, found.Payments);
        }

        /// <summary>
        /// Successfully finds a payment with search paramaters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);
            PaginatedPayments found = await client.FindPaymentsAsync();

            Assert.Contains(created, found.Payments);
        }

        /// <summary>
        /// Successfully deletes a payment.
        /// </summary>
        [Test]
        public async Task Delete()
        {
            player.Play("Delete");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);
            Payment deleted = await client.DeletePaymentAsync(created.Id);

            //Temporary fix while server side does not return PayerDetailsSource for deletion.
            deleted.PayerDetailsSource = "payer";

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

        /// <summary>
        /// Successfully gets a payment delivery date
        /// </summary>
        [Test]
        public async Task GetPaymentDeliveryDates()
        {
            player.Play("GetPaymentDeliveryDates");

            var paymentDeliveryDates = new PaymentDeliveryDates(new DateTime(2018, 1, 1), "regular", "EUR", "IT");

            PaymentDeliveryDates created = await client.GetPaymentDeliveryDatesAsync(paymentDeliveryDates);

            Assert.NotNull(created);
            Assert.AreEqual(created.Currency, "EUR");
            Assert.AreEqual(created.BankCountry, "IT");
            Assert.AreEqual(created.PaymentType, "regular");
        }

        /// <summary>
        /// Successfully gets a quote payments fee
        /// </summary>
        [Test]
        public async Task GetQuotePaymentFee()
        {
            player.Play("GetQuotePaymentFee");

            var quotePaymentFee = new QuotePaymentFee(null, "USD", "US", "regular");

            QuotePaymentFee created = await client.GetQuotePaymentFee(quotePaymentFee);

            Assert.NotNull(created);
            Assert.AreEqual("0534aaf2-2egg-0134-2f36-10b11cd33cfb", created.AccountId);
            Assert.AreEqual("USD", created.PaymentCurrency);
            Assert.AreEqual("US", created.PaymentDestinationCountry);
            Assert.AreEqual("regular", created.PaymentType);
            Assert.Null(created.ChargeType);
            Assert.AreEqual("EUR", created.FeeCurrency);
            Assert.AreEqual(10.0, created.FeeAmount);
        }


        /// <summary>
        /// Successfully gets a payment with fee.
        /// </summary>
        [Test]
        public async Task GetWithFee()
        {
            player.Play("GetWithFee");

            Payment gotten = await client.GetPaymentAsync("855fa573-1ace-4da2-a55b-912f10103056");

            Assert.AreEqual(100, gotten.FeeAmount);
            Assert.AreEqual("GBP", gotten.FeeCurrency);
        }
        
        /// <summary>
        /// Successfully gets a tracking info for a payment.
        /// </summary>
        [Test]
        public async Task GetTrackingInfo()
        {
            player.Play("GetTrackingInfo");

            var trackingInfo1 = Payments.TrackingInfo1;

            PaymentTrackingInfo received = await client.GetPaymentTrackingInfoAsync(trackingInfo1.Uetr);
            Assert.AreEqual(trackingInfo1.ToJSON(), received.ToJSON());
            Assert.AreEqual(trackingInfo1, received);
        }

    }
}