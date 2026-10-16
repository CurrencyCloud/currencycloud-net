using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    /// <summary>
    /// Fork behaviour: a tracking_info response in the shape the API really sends (made-up values) reads whole. charge_amount is
    /// a list, empty when nothing was charged, and exchange_rate is an empty string when there was no exchange.
    /// </summary>
    [TestFixture]
    class PaymentTrackingInfoTest
    {
        private const string AuthResponse = "{\"auth_token\":\"4df5b3e5882a412f148dcd08fa4e5b73\"}";

        private const string TrackingResponse = "{\"uetr\":\"1b4e7c2a-3f5d-4c6e-9a8b-0d1e2f3a4b5c\",\"transaction_status\":{\"status\":\"completed\",\"reason\":null}," +
            "\"initiation_time\":\"2026-10-14T12:49:14+00:00\",\"completion_time\":\"2026-10-15T07:16:11+00:00\",\"last_update_time\":\"2026-10-15T07:16:35+00:00\",\"payment_events\":[" +
            "{\"tracker_event_type\":\"customer_credit_transfer_payment_status_update\",\"valid\":true,\"transaction_status\":{\"status\":\"completed\",\"reason\":null}," +
            "\"funds_available\":null,\"forwarded_to_agent\":null,\"from\":\"BANKZAJJXXX\",\"to\":\"TRCKCHZZXXX\",\"originator\":null,\"serial_parties\":{\"debtor\":null,\"debtor_agent\":null," +
            "\"intermediary_agent1\":null,\"instructing_reimbursement_agent\":null,\"creditor_agent\":null,\"creditor\":null},\"sender_acknowledgement_receipt\":\"2026-10-15T07:16:11+00:00\"," +
            "\"instructed_amount\":null,\"confirmed_amount\":{\"currency\":\"ZAR\",\"amount\":\"14850.00\"},\"interbank_settlement_amount\":null,\"interbank_settlement_date\":null," +
            "\"charge_amount\":[],\"charge_type\":null,\"foreign_exchange_details\":{\"source_currency\":null,\"target_currency\":null,\"exchange_rate\":\"\"},\"last_update_time\":\"2026-10-15T07:16:35+00:00\"}," +
            "{\"tracker_event_type\":\"customer_credit_transfer_payment\",\"valid\":true,\"transaction_status\":{\"status\":\"processing\",\"reason\":\"transferred_and_tracked\"}," +
            "\"funds_available\":null,\"forwarded_to_agent\":null,\"from\":\"BANKGB22XXX\",\"to\":\"BANKZAJJXXX\",\"originator\":null,\"serial_parties\":{\"debtor\":null,\"debtor_agent\":\"SENDGB22\"," +
            "\"intermediary_agent1\":null,\"instructing_reimbursement_agent\":null,\"creditor_agent\":\"BANKZAJJ\",\"creditor\":null},\"sender_acknowledgement_receipt\":\"2026-10-14T12:49:38+00:00\"," +
            "\"instructed_amount\":{\"currency\":\"ZAR\",\"amount\":\"15000.00\"},\"confirmed_amount\":null,\"interbank_settlement_amount\":{\"currency\":\"ZAR\",\"amount\":\"15000.00\"}," +
            "\"interbank_settlement_date\":\"2026-10-15T00:00:00+00:00\",\"charge_amount\":[{\"currency\":\"ZAR\",\"amount\":\"150.00\"}],\"charge_type\":\"shared\"," +
            "\"foreign_exchange_details\":{\"source_currency\":\"USD\",\"target_currency\":\"ZAR\",\"exchange_rate\":\"17.42\"},\"last_update_time\":\"2026-10-14T12:50:07+00:00\"}]}";

        private StubHandler handler;
        private Client client;

        [SetUp]
        public async Task SetUp()
        {
            Retry.Enabled = false;
            handler = new StubHandler();
            client = new Client(handler);

            handler.Respond(HttpStatusCode.OK, AuthResponse);
            await client.InitializeAsync(ApiServer.Mock, "development@currencycloud.com", "deadbeef");
        }

        [TearDown]
        public async Task TearDown()
        {
            await client.CloseAsync();
        }

        [Test]
        public async Task ReadsAWholeResponse()
        {
            handler.Respond(HttpStatusCode.OK, TrackingResponse);

            var tracking = await client.GetPaymentTrackingInfoAsync("543477161-91de-012f-e284-1e0030c7f352");

            Assert.That(tracking.Uetr, Is.EqualTo("1b4e7c2a-3f5d-4c6e-9a8b-0d1e2f3a4b5c"));
            Assert.That(tracking.TransactionStatus.Status, Is.EqualTo("completed"));
            Assert.That(tracking.PaymentEvents, Has.Count.EqualTo(2));
            Assert.That(tracking.PaymentEvents[0].ConfirmedAmount.Amount, Is.EqualTo(14850.00m));
            Assert.That(tracking.PaymentEvents[1].SerialParties.CreditorAgent, Is.EqualTo("BANKZAJJ"));
        }

        [Test]
        public async Task ReadsChargesAsAList()
        {
            handler.Respond(HttpStatusCode.OK, TrackingResponse);

            var tracking = await client.GetPaymentTrackingInfoAsync("543477161-91de-012f-e284-1e0030c7f352");

            Assert.That(tracking.PaymentEvents[0].ChargeAmount, Is.Empty);
            Assert.That(tracking.PaymentEvents[1].ChargeAmount.Single().Currency, Is.EqualTo("ZAR"));
            Assert.That(tracking.PaymentEvents[1].ChargeAmount.Single().Amount, Is.EqualTo(150m));
        }

        [Test]
        public async Task ReadsTheExchangeRateAndNoneForAnEmptyOne()
        {
            handler.Respond(HttpStatusCode.OK, TrackingResponse);

            var tracking = await client.GetPaymentTrackingInfoAsync("543477161-91de-012f-e284-1e0030c7f352");

            Assert.That(tracking.PaymentEvents[0].ForeignExchangeDetails.ExchangeRate, Is.Null);
            Assert.That(tracking.PaymentEvents[1].ForeignExchangeDetails.ExchangeRate, Is.EqualTo(17.42m));
        }

        private sealed class StubHandler : HttpMessageHandler
        {
            private readonly Queue<(HttpStatusCode Status, string Body)> responses = new Queue<(HttpStatusCode, string)>();

            public void Respond(HttpStatusCode status, string body)
            {
                responses.Enqueue((status, body));
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var (status, body) = responses.Count > 0 ? responses.Dequeue() : (HttpStatusCode.OK, "{}");
                return Task.FromResult(new HttpResponseMessage(status)
                {
                    RequestMessage = request,
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                });
            }
        }
    }
}
