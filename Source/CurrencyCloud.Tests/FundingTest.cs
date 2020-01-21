using System;
using System.Threading.Tasks;
using CurrencyCloud.Entity;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Environment;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Tests.Mock.Http;
using NUnit.Framework;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class FundingTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Funding.json");

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
        /// Successfully finds an account with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindFundingAccounts");

            var currency = "GBP";
                
            PaginatedFundingAccounts found = await client.FindFundingAccountsAsync(new FundingAccountFindParameters
            {
                Currency = currency,
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });
            Assert.AreEqual(1, found.FundingAccounts.Count);
            FundingAccount account = found.FundingAccounts[0];
            Assert.AreEqual("b7981972-8e29-485b-8a4a-9643fc6ae3sa", account.Id);
            Assert.AreEqual("8d98bdc8-e8e3-47dc-bd08-3dd0f4f7ea7b", account.AccountId);
            Assert.AreEqual("012345678", account.AccountNumber);
            Assert.AreEqual("account_number", account.AccountNumberType);
            Assert.AreEqual("Jon Doe", account.AccountHolderName);
            Assert.AreEqual("Starling", account.BankName);
            Assert.AreEqual("3rd floor, 2 Finsbury Avenue, London, EC2M 2PP, GB", account.BankAddress);
            Assert.AreEqual("UK", account.BankCountry);
            Assert.AreEqual("GBP", account.Currency);
            Assert.AreEqual("regular", account.PaymentType);
            Assert.AreEqual("010203", account.RoutingCode);
            Assert.AreEqual("sort_code", account.RoutingCodeType);
            Assert.AreEqual(DateTime.Parse("2018-05-14T14:18:30+00:00"), account.CreatedAt);
            Assert.AreEqual(DateTime.Parse("2018-05-14T14:19:30+00:00"), account.UpdatedAt);
        }
    }
}