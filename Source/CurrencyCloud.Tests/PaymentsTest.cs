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
        Player player = new Player("Mock/Http/Recordings/Payments.json");

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

            Assert.That(created.Currency, Is.EqualTo(payment1.Currency));
            Assert.That(created.Amount, Is.EqualTo(payment1.Amount));
            Assert.That(created.Reason, Is.EqualTo(payment1.Reason));
            Assert.That(created.Reference, Is.EqualTo(payment1.Reference));
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

            Assert.That(created, Is.EqualTo(gotten));
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

            Assert.That(updated, Is.EqualTo(gotten));
        }

        /// <summary>
        /// Successfully gets a payment submission info in MT103 format.
        /// </summary>
        [Test]
        public async Task GetSubmissionInfoMT103()
        {
            player.Play("GetSubmissionInfoMT103");

            var payment1 = Payments.Payment1;
            var submissionInfo1 = Payments.SubmissionInfo1;

            Payment created = await CreatePayment(payment1);
            PaymentSubmissionInfo gotten = await client.GetPaymentSubmissionInfoAsync(created.Id);

            Assert.That(submissionInfo1, Is.EqualTo(gotten));
        }

        /// <summary>
        /// Successfully gets a payment submission info in PACS008 format.
        /// </summary>
        [Test]
        public async Task GetSubmissionInfoPACS008()
        {
            player.Play("GetSubmissionInfoPACS008");

            var payment1 = Payments.Payment1;
            var submissionInfo2 = Payments.SubmissionInfo2;

            Payment created = await CreatePayment(payment1);
            PaymentSubmissionInfo gotten = await client.GetPaymentSubmissionInfoAsync(created.Id);

            Assert.That(submissionInfo2, Is.EqualTo(gotten));
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

            Assert.That(confirmation1, Is.EqualTo(gotten));
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

            Assert.That(Payments.Authorisation1, Is.EqualTo(gotten.Authorisations[0]));
            Assert.That(Payments.Authorisation2, Is.EqualTo(gotten.Authorisations[1]));
            Assert.That(Payments.Authorisation3, Is.EqualTo(gotten.Authorisations[2]));
        }

        /// <summary>
        /// Successfully finds a payment with search parameters.
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

            Assert.That(found.Payments, Does.Contain(created));
        }

        /// <summary>
        /// Successfully finds a payment with search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var payment1 = Payments.Payment1;

            Payment created = await CreatePayment(payment1);
            PaginatedPayments found = await client.FindPaymentsAsync();

            Assert.That(found.Payments, Does.Contain(created));
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

            Assert.That(deleted, Is.EqualTo(created));

            try
            {
                await client.GetPaymentAsync(created.Id);

                Assert.Fail();
            }
            catch (System.Exception ex)
            {
                Assert.That(ex, Is.InstanceOf(typeof(NotFoundException)));
            }
        }

        /// <summary>
        /// Successfully gets a payment delivery date
        /// </summary>
        [Test]
        public async Task GetPaymentDeliveryDates()
        {
            player.Play("GetPaymentDeliveryDates");

            var paymentDeliveryDates = new PaymentDeliveryDates(new DateOnly(2018, 1, 1), "regular", "EUR", "IT");

            PaymentDeliveryDates created = await client.GetPaymentDeliveryDatesAsync(paymentDeliveryDates);

            Assert.That(created, Is.Not.Null);
            Assert.That(created.Currency, Is.EqualTo("EUR"));
            Assert.That(created.BankCountry, Is.EqualTo("IT"));
            Assert.That(created.PaymentType, Is.EqualTo("regular"));
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

            Assert.That(created, Is.Not.Null);
            Assert.That(created.AccountId, Is.EqualTo("0534aaf2-2egg-0134-2f36-10b11cd33cfb"));
            Assert.That(created.PaymentCurrency, Is.EqualTo("USD"));
            Assert.That(created.PaymentDestinationCountry, Is.EqualTo("US"));
            Assert.That(created.PaymentType, Is.EqualTo("regular"));
            Assert.That(created.ChargeType, Is.Null);
            Assert.That(created.FeeCurrency, Is.EqualTo("EUR"));
            Assert.That(created.FeeAmount, Is.EqualTo(10.0));
        }


        /// <summary>
        /// Successfully gets a payment with fee.
        /// </summary>
        [Test]
        public async Task GetWithFee()
        {
            player.Play("GetWithFee");

            Payment gotten = await client.GetPaymentAsync("855fa573-1ace-4da2-a55b-912f10103056");

            Assert.That(gotten.FeeAmount, Is.EqualTo(100));
            Assert.That(gotten.FeeCurrency, Is.EqualTo("GBP"));
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
            Assert.That(received.ToJSON, Is.EqualTo(trackingInfo1.ToJSON()));
            Assert.That(received, Is.EqualTo(trackingInfo1));
        }

        /// <summary>
        /// Successfully validates and creates a payment with SCA.
        /// </summary>
        [Test]
        public async Task ValidateAndCreateWithSca()
        {
            player.Play("ValidateAndCreateWithSca");

            var scaPayment = Payments.ScaPayment;

            PaymentValidation validationResult = await client.ValidatePaymentAsync(scaPayment, true);

            Assert.That(validationResult, Is.Not.Null);
            Assert.That(validationResult.ValidationResult, Is.EqualTo("success"));
            Assert.That(validationResult.XScaRequired, Is.True);
            Assert.That(validationResult.XScaId, Is.Not.Null);
            Assert.That(validationResult.XScaType, Is.EqualTo("SMS"));


            Payment created = await client.CreatePaymentAsync(scaPayment, scaId: validationResult.XScaId, scaToken: "123456");
            Assert.That(created.Currency, Is.EqualTo(scaPayment.Currency));
            Assert.That(created.Reason, Is.EqualTo(scaPayment.Reason));
            Assert.That(created.Reference, Is.EqualTo(scaPayment.Reference));
        }
        
        /// <summary>
        /// Create Payment with Payer Ultimate Account Number.
        /// </summary>
        [Test]
        public async Task CreateWithPayerUltimateAccountNumber()
        {
            player.Play("CreateWithPayerUltimateAccountNumber");

            var payment = Payments.Payment1;
            Payer payer = new Entity.Payer { UltimateAccountNumber = "12345678" };
            
            Payment created = await client.CreatePaymentAsync(payment, payer);
            Assert.That(created.Currency, Is.EqualTo(payment.Currency));
            Assert.That(created.Reason, Is.EqualTo(payment.Reason));
            Assert.That(created.Reference, Is.EqualTo(payment.Reference));
        }
        
        /// <summary>
        /// Error response handling retrying payment notifications
        /// </summary>
        [Test]
        public async Task RetrySendingPaymentNotificationsError()
        {
            player.Play("RetryPaymentNotifications_Error");

            // Fails on invalid arguments
            try
            {
                await client.RetryPaymentNotificationsAsync("", "");
                Assert.Fail("Expected ArgumentException but no exception was thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.That(ex.Message, Does.Contain("cannot be null or empty"));
            }

            // Fails on invalid notification type
            try
            {
                await client.RetryPaymentNotificationsAsync(
                    "855fa573-1ace-4da2-a55b-912f10103055",
                    "payment_notification");
                Assert.Fail("Expected BadRequestException but no exception was thrown.");
            }
            catch (BadRequestException ex)
            {
                Assert.That(ex.Errors[0].ErrorMessages[0].Code, Is.EqualTo("notification_type_not_in_range"));
            }
        }
        
        /// <summary>
        /// Successfully retries sending payment notifications
        /// </summary>
        [Test]
        public async Task CanRetrySendingPaymentNotifications()
        {
            player.Play("RetryPaymentNotifications_Success");
            // Return 200 Success and Empty object
            var result = await client.RetryPaymentNotificationsAsync(
                id: "855fa573-1ace-4da2-a55b-912f10103055", 
                notificationType: "payment_released_notification"
            );
            Assert.That(result, Is.Not.Null);
        }
    }
}