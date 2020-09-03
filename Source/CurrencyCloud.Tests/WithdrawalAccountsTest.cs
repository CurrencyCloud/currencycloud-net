using System;
using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;

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


            PaginatedWithdrawalAccounts found = await client.FindWithdrawalAccountsAsync("72970a7c-7921-431c-b95f-3438724ba16f");
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

    }
}
