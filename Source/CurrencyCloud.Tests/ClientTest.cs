using System;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity;
using CurrencyCloud.Exception;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class ClientTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Client.json");

        dynamic credentials = Authentication.Credentials;

        [TestFixtureSetUp]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            player.Close();
        }

        /// <summary>
        /// Fails to make an API call before logging in.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void FailBeforeInitialize()
        {
            await client.GetCurrentAccountAsync();
        }

        /// <summary>
        /// Successfully initializes the client and logs in.
        /// </summary>
        [Test]
        public async void Initialize()
        {
            player.Play("Initialize");

            var token = await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);

            Assert.IsNotEmpty(token);

            await client.CloseAsync();
        }

        /// <summary>
        /// Persists authentication token and so can make a subsequent API call.
        /// </summary>
        [Test]
        public async void PersistToken()
        {
            player.Play("PersistToken");

            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            await client.GetCurrentAccountAsync();
            await client.CloseAsync();
        }

        /// <summary>
        /// Silently re-authenticates if token has expired.
        /// </summary>
        [Test]
        public async void Reauthenticate()
        {
            player.Play("Reauthenticate");

            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);

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
        public async void Close()
        {
            player.Play("Close");

            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            await client.CloseAsync();

            Assert.IsFalse(client.IsInitialized);
        }

        /// <summary>
        /// Fails to make an API call once logged out.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void FailAfterClose()
        {
            player.Play("FailAfterClose");

            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
            await client.CloseAsync();
            await client.GetCurrentAccountAsync();
        }

        /// <summary>
        /// Returns full error information.
        /// </summary>
        [Test]
        public async void FailWithError()
        {
            player.Play("FailWithError");

            try
            {
                await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);
                await client.GetBalanceAsync("wrong");

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.IsNotNullOrEmpty(ex.Platform);

                Assert.IsNotNullOrEmpty(ex.Request.Verb);
                Assert.IsNotNullOrEmpty(ex.Request.Url);
                Assert.IsEmpty(ex.Request.Parameters);

                Assert.AreEqual(ex.Response.StatusCode, 400);
                Assert.IsFalse(DateTime.Equals(ex.Response.Date, DateTime.MinValue));
                Assert.IsNotNullOrEmpty(ex.Response.RequestId);

                Assert.IsNotEmpty(ex.Errors);

                await client.CloseAsync();
            }
        }

        /// <summary>
        /// Executes API calls on behalf of specified id; once completed, resets the id.
        /// </summary>
        [Test]
        public async void RunOnbehalfof()
        {
            player.Play("RunOnbehalfof");

            await client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey);

            var contactParams = Contacts.Contact1;
            var beneficiaryParams = Beneficiaries.Beneficiary1;

            Beneficiary beneficiary;

            Account account = await client.GetCurrentAccountAsync();
            Contact contact = await client.CreateContactAsync(account.Id, contactParams.FirstName, contactParams.LastName, contactParams.EmailAddress, contactParams.PhoneNumber, contactParams.Optional);
            await client.OnBehalfOf(contact.Id, async () =>
            {
                beneficiary = await client.CreateBeneficiaryAsync(beneficiaryParams.BankAccountHolderName, beneficiaryParams.BankCountry, beneficiaryParams.Currency, beneficiaryParams.Name, beneficiaryParams.Optional);

                Assert.AreEqual(contact.Id, beneficiary.CreatorContactId);
            });

            contact = await client.GetCurrentContactAsync();
            beneficiary = await client.CreateBeneficiaryAsync(beneficiaryParams.BankAccountHolderName, beneficiaryParams.BankCountry, beneficiaryParams.Currency, beneficiaryParams.Name, beneficiaryParams.Optional);

            Assert.AreEqual(contact.Id, beneficiary.CreatorContactId);

            await client.CloseAsync();
        }

        /// <summary>
        /// Fails if id parameter of OnBehalfOf is invalid.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public async void FailOnbehalfof()
        {
            await client.OnBehalfOf("wrong", null);
        }
    }
}

