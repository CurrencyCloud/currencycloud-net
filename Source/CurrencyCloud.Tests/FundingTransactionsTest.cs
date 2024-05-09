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

            Assert.AreEqual(fundingTransaction, gotten);
        }
    }
}
