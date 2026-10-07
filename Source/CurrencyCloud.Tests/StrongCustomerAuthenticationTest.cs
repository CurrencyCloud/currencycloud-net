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
using CurrencyCloud.Exception;

namespace CurrencyCloud.Tests
{
    /// <summary>
    /// Fork behaviour: a 401 caused by strong customer authentication is a StrongCustomerAuthenticationException and is
    /// never answered by re-authenticating and re-sending, while any other 401 still is.
    /// </summary>
    [TestFixture]
    class StrongCustomerAuthenticationTest
    {
        private const string AuthResponse = "{\"auth_token\":\"4df5b3e5882a412f148dcd08fa4e5b73\"}";
        private const string PlainAuthFailure = "{\"error_code\":\"auth_failed\",\"error_messages\":{\"username\":[{\"code\":\"invalid_supplied_credentials\",\"message\":\"Authentication failed with the supplied credentials\",\"params\":{}}]}}";
        private const string ChargeSettings = "{\"payment_charges_settings\":[{\"charge_settings_id\":\"844c5c2f-0000-0000-0000-000000000001\",\"account_id\":\"f26fca0d-0000-0000-0000-000000000001\",\"charge_type\":\"ours\",\"enabled\":false,\"default\":false},{\"charge_settings_id\":\"b771f49f-0000-0000-0000-000000000002\",\"account_id\":\"f26fca0d-0000-0000-0000-000000000001\",\"charge_type\":\"shared\",\"enabled\":true,\"default\":true}]}";
        private const string PaymentResponse = "{\"id\":\"543477161-91de-012f-e284-1e0030c7f352\",\"currency\":\"GBP\",\"amount\":\"100.00\"}";

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
            handler.Requests.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            handler.Dispose();
        }

        [Test]
        public void A_401_on_a_request_carrying_an_sca_token_is_not_retried()
        {
            handler.Respond(HttpStatusCode.Unauthorized, PlainAuthFailure);

            Assert.ThrowsAsync<StrongCustomerAuthenticationException>(async () =>
                await client.CreatePaymentAsync(new Payment("GBP", "bene-1", 100m, "Invoice", "INV-1"), scaId: "sca-1", scaToken: "123456"));

            Assert.That(handler.Requests.Select(r => r.Path), Is.EqualTo(new[] { "/v2/payments/create" }));
            Assert.That(handler.Requests.Single().Headers["x-sca-token"], Is.EqualTo("123456"));
        }

        [TestCase("{\"error_code\":\"sca_required\",\"error_messages\":{\"base\":[{\"code\":\"auth_failed\",\"message\":\"Authentication failed\",\"params\":{}}]}}")]
        [TestCase("{\"error_code\":\"auth_failed\",\"error_messages\":{\"base\":[{\"code\":\"sca_token_invalid\",\"message\":\"Authentication failed\",\"params\":{}}]}}")]
        [TestCase("{\"error_code\":\"auth_failed\",\"error_messages\":{\"base\":[{\"code\":\"auth_failed\",\"message\":\"Strong Customer Authentication is required\",\"params\":{}}]}}")]
        [TestCase("{\"error_code\":\"strong_customer_authentication_required\"}")]
        public void A_401_that_names_sca_is_not_retried(string body)
        {
            handler.Respond(HttpStatusCode.Unauthorized, body);

            Assert.ThrowsAsync<StrongCustomerAuthenticationException>(async () => await client.GetPaymentChargesSettingsAsync("acc-1"));

            Assert.That(handler.Requests.Count, Is.EqualTo(1));
        }

        [TestCase("{\"error_code\":\"auth_failed\",\"error_messages\":{\"base\":[{\"code\":\"description_scaffold\",\"message\":\"Escalated\",\"params\":{}}]}}")]
        [TestCase(PlainAuthFailure)]
        public void A_plain_401_still_re_authenticates_and_re_sends(string body)
        {
            handler.Respond(HttpStatusCode.Unauthorized, body);
            handler.Respond(HttpStatusCode.OK, AuthResponse);
            handler.Respond(HttpStatusCode.OK, ChargeSettings);

            var settings = client.GetPaymentChargesSettingsAsync("acc-1").GetAwaiter().GetResult();

            Assert.That(settings.PaymentChargesSettings, Has.Count.EqualTo(2));
            Assert.That(handler.Requests.Select(r => r.Path), Is.EqualTo(new[]
            {
                "/v2/accounts/acc-1/payment_charges_settings",
                "/v2/authenticate/api",
                "/v2/accounts/acc-1/payment_charges_settings"
            }));
        }

        [Test]
        public void A_plain_401_on_a_payment_without_sca_headers_still_re_authenticates()
        {
            handler.Respond(HttpStatusCode.Unauthorized, PlainAuthFailure);
            handler.Respond(HttpStatusCode.OK, AuthResponse);
            handler.Respond(HttpStatusCode.OK, PaymentResponse);

            var payment = client.CreatePaymentAsync(new Payment("GBP", "bene-1", 100m, "Invoice", "INV-1")).GetAwaiter().GetResult();

            Assert.That(payment.Id, Is.EqualTo("543477161-91de-012f-e284-1e0030c7f352"));
            Assert.That(handler.Requests.Count(r => r.Path == "/v2/payments/create"), Is.EqualTo(2));
        }

        [Test]
        public async Task Payment_charge_settings_carry_their_charge_type()
        {
            handler.Respond(HttpStatusCode.OK, ChargeSettings);

            var settings = await client.GetPaymentChargesSettingsAsync("f26fca0d-0000-0000-0000-000000000001");

            var ours = settings.PaymentChargesSettings.Single(s => s.ChargeType == "ours");
            var shared = settings.PaymentChargesSettings.Single(s => s.ChargeType == "shared");
            Assert.That(ours.Enabled, Is.False);
            Assert.That(ours.Default, Is.False);
            Assert.That(shared.Enabled, Is.True);
            Assert.That(shared.Default, Is.True);
        }

        private sealed class StubHandler : HttpMessageHandler
        {
            private readonly Queue<(HttpStatusCode Status, string Body)> responses = new Queue<(HttpStatusCode, string)>();

            public List<(string Path, Dictionary<string, string> Headers)> Requests { get; } = new List<(string, Dictionary<string, string>)>();

            public void Respond(HttpStatusCode status, string body)
            {
                responses.Enqueue((status, body));
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Requests.Add((request.RequestUri.AbsolutePath,
                              request.Headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => string.Join(",", h.Value))));

                var (status, body) = responses.Dequeue();
                return Task.FromResult(new HttpResponseMessage(status)
                {
                    RequestMessage = request,
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                });
            }
        }
    }
}
