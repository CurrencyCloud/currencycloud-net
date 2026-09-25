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
        Player player = new Player("Mock/Http/Recordings/Funding.json");

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
            Assert.That(found.FundingAccounts.Count, Is.EqualTo(1));
            FundingAccount account = found.FundingAccounts[0];
            Assert.That(account.Id, Is.EqualTo("b7981972-8e29-485b-8a4a-9643fc6ae3sa"));
            Assert.That(account.AccountId, Is.EqualTo("8d98bdc8-e8e3-47dc-bd08-3dd0f4f7ea7b"));
            Assert.That(account.AccountNumber, Is.EqualTo("012345678"));
            Assert.That(account.AccountNumberType, Is.EqualTo("account_number"));
            Assert.That(account.AccountHolderName, Is.EqualTo("Jon Doe"));
            Assert.That(account.BankName, Is.EqualTo("Starling"));
            Assert.That(account.BankAddress, Is.EqualTo("3rd floor, 2 Finsbury Avenue, London, EC2M 2PP, GB"));
            Assert.That(account.BankCountry, Is.EqualTo("UK"));
            Assert.That(account.Currency, Is.EqualTo("GBP"));
            Assert.That(account.PaymentType, Is.EqualTo("regular"));
            Assert.That(account.RoutingCode, Is.EqualTo("010203"));
            Assert.That(account.RoutingCodeType, Is.EqualTo("sort_code"));
            Assert.That(account.CreatedAt, Is.EqualTo(DateTime.Parse("2018-05-14T14:18:30+00:00")));
            Assert.That(account.UpdatedAt, Is.EqualTo(DateTime.Parse("2018-05-14T14:19:30+00:00")));
        }

        /// <summary>
        /// Successfully gets a funding transaction.
        /// </summary>
        [Test]
        public async Task GetFundingTransaction()
        {
            player.Play("GetFundingTransaction");

            string fundingTransactionId = "4924919a-6c28-11ee-a3e3-63774946bad2";

            FundingTransaction transaction = await client.GetFundingTransactionAsync(fundingTransactionId);

            Assert.That(transaction.Id, Is.EqualTo(fundingTransactionId));
            Assert.That(transaction.Amount, Is.EqualTo(1.11m));
            Assert.That(transaction.Currency, Is.EqualTo("USD"));
            Assert.That(transaction.Rail, Is.EqualTo("SEPA"));
            Assert.That(transaction.AdditionalInformation, Is.EqualTo("ABCD20231016143117"));
            Assert.That(transaction.ReceivingAccountRoutingCode, Is.EqualTo("123456789"));
            Assert.That(transaction.ReceivingAccountNumber, Is.EqualTo("32847346"));
            Assert.That(transaction.ReceivingAccountIban, Is.Null);
            Assert.That(transaction.CreatedAt, Is.EqualTo(DateTime.Parse("2022-12-03T10:15:30+00:00")));
            Assert.That(transaction.UpdatedAt, Is.EqualTo(DateTime.Parse("2022-12-03T10:15:30+00:00")));
            Assert.That(transaction.CompletedAt, Is.EqualTo(DateTime.Parse("2022-12-03T10:15:30+00:00")));
            Assert.That(transaction.ValueDate, Is.EqualTo(DateTime.Parse("2022-12-03T10:00:00+00:00")));

            Assert.That(transaction.Sender, Is.Not.Null);
            Assert.That(transaction.Sender.AccountNumber, Is.EqualTo("8119645406"));
            Assert.That(transaction.Sender.Address, Is.EqualTo("Some street"));
            Assert.That(transaction.Sender.Bic, Is.Null);
            Assert.That(transaction.Sender.Country, Is.EqualTo("GB"));
            Assert.That(transaction.Sender.Iban, Is.Null);
            Assert.That(transaction.Sender.Id, Is.EqualTo("5c675fa4-fdf0-4ee6-b5bb-156b36765433"));
            Assert.That(transaction.Sender.Name, Is.EqualTo("Test sender"));
            Assert.That(transaction.Sender.RoutingCode, Is.Null);
        }
    }
}