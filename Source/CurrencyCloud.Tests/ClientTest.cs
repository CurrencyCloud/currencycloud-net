using System;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity;
using CurrencyCloud.Exception;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ClientTest
    {
        Client client = new Client();
        Player player = new Player("Mock/Http/Recordings/Client.json");

        Credentials credentials = Authentication.Credentials;

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            player.Close();
        }

        /// <summary>
        /// Fails to make an API call before logging in.
        /// </summary>
        [Test]
        public void FailBeforeInitialize()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await client.GetCurrentAccountAsync());
        }

        /// <summary>
        /// Successfully initializes the client and logs in.
        /// </summary>
        [Test]
        public async Task Initialize()
        {
            player.Play("Initialize");

            var token = await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);

            Assert.That(token, Is.Not.Empty);

            await client.CloseAsync();
        }

        /// <summary>
        /// Persists authentication token and so can make a subsequent API call.
        /// </summary>
        [Test]
        public async Task PersistToken()
        {
            player.Play("PersistToken");

            await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);
            await client.GetCurrentAccountAsync();
            await client.CloseAsync();
        }

        /// <summary>
        /// Silently re-authenticates if token has expired.
        /// </summary>
        [Test]
        public async Task Reauthenticate()
        {
            player.Play("Reauthenticate");

            await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);

            var expired = "3907f05da86533710efc589d58f51f45";
            client.Token = expired;

            await client.GetCurrentAccountAsync();

            Assert.That(client.Token, Is.Not.EqualTo(expired));

            await client.CloseAsync();
        }

        /// <summary>
        /// Successfully logs out.
        /// </summary>
        [Test]
        public async Task Close()
        {
            player.Play("Close");

            await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);
            await client.CloseAsync();

            Assert.That(client.IsInitialized, Is.False);
        }

        /// <summary>
        /// Fails to make an API call once logged out.
        /// </summary>
        [Test]
        public void FailAfterClose()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                player.Play("FailAfterClose");

                await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);
                await client.CloseAsync();
                await client.GetCurrentAccountAsync();
            });
        }

        /// <summary>
        /// Returns full error information.
        /// </summary>
        [Test]
        public async Task FailWithError()
        {
            player.Play("FailWithError");

            try
            {
                await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);
                await client.GetBalanceAsync("wrong");

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.That(ex.Platform, Is.Not.Null.And.Not.Empty);

                Assert.That(ex.Request.Verb, Is.Not.Null.And.Not.Empty);
                Assert.That(ex.Platform, Is.Not.Null.And.Not.Empty);
                Assert.That(ex.Request.Parameters, Is.Empty);

                Assert.That(ex.Response.StatusCode, Is.EqualTo(400));
                Assert.That(DateTime.Equals(ex.Response.Date, DateTime.MinValue), Is.False);
                Assert.That(ex.Response.RequestId, Is.Not.Null.And.Not.Empty);

                Assert.That(ex.Errors, Is.Not.Empty);

                await client.CloseAsync();
            }
        }
        
        /// <summary>
        /// Returns full error information.
        /// </summary>
        [Test]
        public async Task FailWithMalFormedError()
        {
            player.Play("FailWithMalformedError");

            try
            {
                await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);
                await client.GetBankDetailsAsync("iban", "123abc456xyz");

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.That(ex.Platform, Is.Not.Null.And.Not.Empty);

                Assert.That(ex.Request.Verb, Is.Not.Null.And.Not.Empty);
                Assert.That(ex.Platform, Is.Not.Null.And.Not.Empty);
                
                Assert.That(ex.Request.Parameters.Count, Is.EqualTo(0));

                Assert.That(ex.Response.StatusCode, Is.EqualTo(400));
                Assert.That(DateTime.Equals(ex.Response.Date, DateTime.MinValue), Is.False);
                Assert.That(ex.Response.RequestId, Is.Not.Null.And.Not.Empty);

                Assert.That(ex.Errors, Is.Not.Empty);
                Assert.That(ex.Errors.Count, Is.EqualTo(1));
                Assert.That(ex.Errors[0].Field, Is.EqualTo("base"));
                Assert.That(ex.Errors[0].ErrorMessages.Count, Is.EqualTo(1));
                Assert.That(ex.Errors[0].ErrorMessages[0].Code, Is.EqualTo("invalid_iban"));
                Assert.That(ex.Errors[0].ErrorMessages[0].Message, Is.EqualTo("IBAN is invalid."));
                Assert.That(ex.Errors[0].ErrorMessages[0].Params, Is.Empty);

                await client.CloseAsync();
            }
        }

        /// <summary>
        /// Executes API calls on behalf of specified id; once completed, resets the id.
        /// </summary>
        [Test]
        public async Task RunOnbehalfof()
        {
            player.Play("RunOnbehalfof");

            await client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey);

            var contactParams = Contacts.Contact1;
            var beneficiaryParams = Beneficiaries.Beneficiary1;

            Beneficiary beneficiary;

            Account account = await client.GetCurrentAccountAsync();
            contactParams.AccountId = account.Id;
            if (!Authentication.ApiServer.Url.Contains("localhost"))
                contactParams.LoginId = ContactsTest.RandomString(10);
            Contact contact = await client.CreateContactAsync(contactParams);
            await client.OnBehalfOf(contact.Id, async () =>
            {
                beneficiary = await client.CreateBeneficiaryAsync(beneficiaryParams);

                Assert.That(beneficiary.CreatorContactId, Is.EqualTo(contact.Id));
            });

            contact = await client.GetCurrentContactAsync();
            beneficiary = await client.CreateBeneficiaryAsync(beneficiaryParams);

            Assert.That(beneficiary.CreatorContactId, Is.EqualTo(contact.Id));

            await client.CloseAsync();
        }

        /// <summary>
        /// Fails if id parameter of OnBehalfOf is invalid.
        /// </summary>
        [Test]
        public void FailOnbehalfof() {
            Assert.ThrowsAsync<ArgumentException>(async () => {
                await client.OnBehalfOf("wrong", null);
            });
        }
    }
}

