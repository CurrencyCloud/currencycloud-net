using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class FundingTransactionsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/FundingTransactions.json");

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
        /// Successfully gets a funding transaction
        /// </summary>
        [Test]
        public async Task GetFundingTransaction()
        {
            player.Play("GetFundingTransaction");

            var fundingTransaction = FundingTransactions.FundingTransaction1;

            FundingTransaction gotten = await client.GetFundingTransactionAsync(fundingTransaction.Id);

            Assert.IsNotNull(gotten);
            Assert.AreEqual(fundingTransaction.Id, gotten.Id);
            Assert.AreEqual(fundingTransaction.Amount, gotten.Amount);
            Assert.AreEqual(fundingTransaction.Currency, gotten.Currency);
            Assert.AreEqual(fundingTransaction.Rail, gotten.Rail);
            Assert.AreEqual(fundingTransaction.AdditionalInformation, gotten.AdditionalInformation);
            Assert.AreEqual(fundingTransaction.ValueDate, gotten.ValueDate);
            Assert.AreEqual(fundingTransaction.ReceivingAccountNumber, gotten.ReceivingAccountNumber);
            Assert.AreEqual(fundingTransaction.CreatedAt, gotten.CreatedAt);
            Assert.AreEqual(fundingTransaction.UpdatedAt, gotten.UpdatedAt);
            Assert.AreEqual(fundingTransaction.Sender.SenderId, gotten.Sender.SenderId);
            Assert.AreEqual(fundingTransaction.Sender.SenderAddress, gotten.Sender.SenderAddress);
            Assert.AreEqual(fundingTransaction.Sender.SenderCountry, gotten.Sender.SenderCountry);
            Assert.AreEqual(fundingTransaction.Sender.SenderName, gotten.Sender.SenderName);
        }
    }
}
