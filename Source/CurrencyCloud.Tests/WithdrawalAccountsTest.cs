using System;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;
using CurrencyCloud.Entity;

namespace CurrencyCloud.Tests.Mock
{
    [TestFixture]
    class WithdrawalAccountsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/WithdrawalAccounts.json");

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
        /// Successfully finds WithdrawalAccounts.
        /// </summary>
        [Test]
        public async Task Find()
        {
            player.Play("Find");


            PaginatedWithdrawalAccounts found =
                await client.FindWithdrawalAccountsAsync("72970a7c-7921-431c-b95f-3438724ba16f");
            Assert.AreEqual("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de2", found.WithdrawalAccounts[0].Id);
            Assert.AreEqual("currencycloud", found.WithdrawalAccounts[0].AccountName);
            Assert.AreEqual("The Currency Cloud", found.WithdrawalAccounts[0].AccountHolderName);
            Assert.Null(found.WithdrawalAccounts[0].AccountHolderDob);
            Assert.AreEqual("123456789", found.WithdrawalAccounts[0].RoutingCode);
            Assert.AreEqual("01234567890", found.WithdrawalAccounts[0].AccountNumber);
            Assert.AreEqual("USD", found.WithdrawalAccounts[0].Currency);
            Assert.AreEqual("72970a7c-7921-431c-b95f-3438724ba16f", found.WithdrawalAccounts[0].AccountId);

        }

        /// <summary>
        /// Successfully finds WithdrawalAccounts.
        /// </summary>
        [Test]
        public async Task Find2()
        {
            player.Play("Find2");


            PaginatedWithdrawalAccounts found = await client.FindWithdrawalAccountsAsync();

            Assert.AreEqual("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de2", found.WithdrawalAccounts[0].Id);
            Assert.AreEqual("currencycloud", found.WithdrawalAccounts[0].AccountName);
            Assert.AreEqual("The Currency Cloud", found.WithdrawalAccounts[0].AccountHolderName);
            Assert.Null(found.WithdrawalAccounts[0].AccountHolderDob);
            Assert.AreEqual("123456789", found.WithdrawalAccounts[0].RoutingCode);
            Assert.AreEqual("01234567890", found.WithdrawalAccounts[0].AccountNumber);
            Assert.AreEqual("USD", found.WithdrawalAccounts[0].Currency);
            Assert.AreEqual("72970a7c-7921-431c-b95f-3438724ba16f", found.WithdrawalAccounts[0].AccountId);


            Assert.AreEqual("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de3", found.WithdrawalAccounts[1].Id);
            Assert.AreEqual("currencycloud2", found.WithdrawalAccounts[1].AccountName);
            Assert.AreEqual("The Currency Cloud 2", found.WithdrawalAccounts[1].AccountHolderName);
            Assert.AreEqual(DateTime.Parse("1990-07-20"), found.WithdrawalAccounts[1].AccountHolderDob);
            Assert.AreEqual("223456789", found.WithdrawalAccounts[1].RoutingCode);
            Assert.AreEqual("01234567892", found.WithdrawalAccounts[1].AccountNumber);
            Assert.AreEqual("GBP", found.WithdrawalAccounts[1].Currency);
            Assert.AreEqual("72970a7c-7921-431c-b95f-3438724ba16g", found.WithdrawalAccounts[1].AccountId);
        }

        /// <summary>
        /// Successfully pull funds from WithdrawalAccount.
        /// </summary>
        [Test]
        public async Task PullFunds()
        {
            player.Play("PullFunds");
            WithdrawalAccountFunds funds = await client.WithdrawalAccountsPullFundsAsync("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de2",
                100.0m, "PullFunds1");
            
            Assert.AreEqual("e2e6b7aa-c9e8-4625-96a6-b97d4baab758", funds.Id);
            Assert.AreEqual("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de2", funds.WithdrawalAccountId);
            Assert.AreEqual("PullFunds1", funds.Reference);
            Assert.AreEqual(100, funds.Amount);
            Assert.AreEqual(DateTime.Parse("2020-06-29T08:02:31+00:00"), funds.CreatedAt);
        }
    }
}
