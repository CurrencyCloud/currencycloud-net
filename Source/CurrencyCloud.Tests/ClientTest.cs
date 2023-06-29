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
        Player player = new Player("/../../Mock/Http/Recordings/Client.json");

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

            Assert.IsNotEmpty(token);

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

            Assert.AreNotEqual(expired, client.Token);

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

            Assert.IsFalse(client.IsInitialized);
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
                Assert.IsEmpty(ex.Request.Parameters);

                Assert.AreEqual(ex.Response.StatusCode, 400);
                Assert.IsFalse(DateTime.Equals(ex.Response.Date, DateTime.MinValue));
                Assert.That(ex.Response.RequestId, Is.Not.Null.And.Not.Empty);

                Assert.IsNotEmpty(ex.Errors);

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
                
                Assert.AreEqual(0,ex.Request.Parameters.Count);

                Assert.AreEqual(400, ex.Response.StatusCode);
                Assert.IsFalse(DateTime.Equals(ex.Response.Date, DateTime.MinValue));
                Assert.That(ex.Response.RequestId, Is.Not.Null.And.Not.Empty);

                Assert.IsNotEmpty(ex.Errors);
                Assert.AreEqual(1,ex.Errors.Count);
                Assert.AreEqual("base",ex.Errors[0].Field);
                Assert.AreEqual(1,ex.Errors[0].ErrorMessages.Count);
                Assert.AreEqual("invalid_iban",ex.Errors[0].ErrorMessages[0].Code);
                Assert.AreEqual("IBAN is invalid.",ex.Errors[0].ErrorMessages[0].Message);
                Assert.IsEmpty(ex.Errors[0].ErrorMessages[0].Params);

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

                Assert.AreEqual(contact.Id, beneficiary.CreatorContactId);
            });

            contact = await client.GetCurrentContactAsync();
            beneficiary = await client.CreateBeneficiaryAsync(beneficiaryParams);

            Assert.AreEqual(contact.Id, beneficiary.CreatorContactId);

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

