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
        Player player = new Player("Mock/Http/Recordings/WithdrawalAccounts.json");

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
            Assert.That(found.WithdrawalAccounts[0].Id, Is.EqualTo("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de2"));
            Assert.That(found.WithdrawalAccounts[0].AccountName, Is.EqualTo("currencycloud"));
            Assert.That(found.WithdrawalAccounts[0].AccountHolderName, Is.EqualTo("The Currency Cloud"));
            Assert.That(found.WithdrawalAccounts[0].AccountHolderDob, Is.Null);
            Assert.That(found.WithdrawalAccounts[0].RoutingCode, Is.EqualTo("123456789"));
            Assert.That(found.WithdrawalAccounts[0].AccountNumber, Is.EqualTo("01234567890"));
            Assert.That(found.WithdrawalAccounts[0].Currency, Is.EqualTo("USD"));
            Assert.That(found.WithdrawalAccounts[0].AccountId, Is.EqualTo("72970a7c-7921-431c-b95f-3438724ba16f"));

        }

        /// <summary>
        /// Successfully finds WithdrawalAccounts.
        /// </summary>
        [Test]
        public async Task Find2()
        {
            player.Play("Find2");


            PaginatedWithdrawalAccounts found = await client.FindWithdrawalAccountsAsync();

            Assert.That(found.WithdrawalAccounts[0].Id, Is.EqualTo("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de2"));
            Assert.That(found.WithdrawalAccounts[0].AccountName, Is.EqualTo("currencycloud"));
            Assert.That(found.WithdrawalAccounts[0].AccountHolderName, Is.EqualTo("The Currency Cloud"));
            Assert.That(found.WithdrawalAccounts[0].AccountHolderDob, Is.Null);
            Assert.That(found.WithdrawalAccounts[0].RoutingCode, Is.EqualTo("123456789"));
            Assert.That(found.WithdrawalAccounts[0].AccountNumber, Is.EqualTo("01234567890"));
            Assert.That(found.WithdrawalAccounts[0].Currency, Is.EqualTo("USD"));
            Assert.That(found.WithdrawalAccounts[0].AccountId, Is.EqualTo("72970a7c-7921-431c-b95f-3438724ba16f"));


            Assert.That(found.WithdrawalAccounts[1].Id, Is.EqualTo("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de3"));
            Assert.That(found.WithdrawalAccounts[1].AccountName, Is.EqualTo("currencycloud2"));
            Assert.That(found.WithdrawalAccounts[1].AccountHolderName, Is.EqualTo("The Currency Cloud 2"));
            Assert.That(found.WithdrawalAccounts[1].AccountHolderDob, Is.EqualTo(DateTime.Parse("1990-07-20")));
            Assert.That(found.WithdrawalAccounts[1].RoutingCode, Is.EqualTo("223456789"));
            Assert.That(found.WithdrawalAccounts[1].AccountNumber, Is.EqualTo("01234567892"));
            Assert.That(found.WithdrawalAccounts[1].Currency, Is.EqualTo("GBP"));
            Assert.That(found.WithdrawalAccounts[1].AccountId, Is.EqualTo("72970a7c-7921-431c-b95f-3438724ba16g"));
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
            
            Assert.That(funds.Id, Is.EqualTo("e2e6b7aa-c9e8-4625-96a6-b97d4baab758"));
            Assert.That(funds.WithdrawalAccountId, Is.EqualTo("0886ac00-6ab6-41a6-b0e1-8d3faf2e0de2"));
            Assert.That(funds.Reference, Is.EqualTo("PullFunds1"));
            Assert.That(funds.Amount, Is.EqualTo(100));
            Assert.That(funds.CreatedAt, Is.EqualTo(DateTime.Parse("2020-06-29T08:02:31+00:00")));
        }
    }
}
