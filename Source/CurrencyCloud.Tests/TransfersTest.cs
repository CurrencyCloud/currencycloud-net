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
    class TransfersTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Transfers.json");

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
        /// Successfully creates a transfer.
        /// </summary>
        [Test]
        public async Task Create()
        {
            player.Play("Create");

            var transfer1 = Transfers.Transfer1;

            Transfer created = await client.CreateTransferAsync(transfer1);

            Assert.AreEqual(transfer1.SourceAccountId, created.SourceAccountId);
            Assert.AreEqual(transfer1.DestinationAccountId, created.DestinationAccountId);
            Assert.AreEqual(transfer1.Currency, created.Currency);
            Assert.AreEqual(transfer1.Amount, created.Amount);
            Assert.AreEqual(transfer1.Status, created.Status);
            Assert.AreEqual(transfer1.Reason, created.Reason);
        }

        /// <summary>
        /// Successfully gets a transfer.
        /// </summary>
        [Test]
        public async Task Get()
        {
            player.Play("Get");

            var transfer2 = Transfers.Transfer2;

            Transfer created = await client.CreateTransferAsync(transfer2);
            Transfer gotten = await client.GetTransferAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully finds transfers with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var transfer3 = Transfers.Transfer3;

            //Transfer created = await client.CreateTransferAsync(transfer3);
            PaginatedTransfers found = await client.FindTransfersAsync(new TransferFindParameters
            {
                ShortReference = "BT-20170118-VMSCBS"
            });

            Assert.AreEqual(transfer3.SourceAccountId, found.Transfers[0].SourceAccountId);
            Assert.AreEqual(transfer3.DestinationAccountId, found.Transfers[0].DestinationAccountId);
            Assert.AreEqual(transfer3.Currency, found.Transfers[0].Currency);
            Assert.AreEqual(transfer3.Amount, found.Transfers[0].Amount);
            Assert.AreEqual(transfer3.Status, found.Transfers[0].Status);
            Assert.AreEqual(transfer3.Reason, found.Transfers[0].Reason);
        }

        /// <summary>
        /// Successfully finds transfers without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var transfer3 = Transfers.Transfer3;

            //Transfer created = await client.CreateTransferAsync(transfer3);
            PaginatedTransfers found = await client.FindTransfersAsync();

            Assert.AreEqual(transfer3.SourceAccountId, found.Transfers[0].SourceAccountId);
            Assert.AreEqual(transfer3.DestinationAccountId, found.Transfers[0].DestinationAccountId);
            Assert.AreEqual(transfer3.Currency, found.Transfers[0].Currency);
            Assert.AreEqual(transfer3.Amount, found.Transfers[0].Amount);
            Assert.AreEqual(transfer3.Status, found.Transfers[0].Status);
            Assert.AreEqual(transfer3.Reason, found.Transfers[0].Reason);
        }
        
        /// <summary>
        /// Successfully Cancels a transfer.
        /// </summary>
        [Test]
        public async Task Cancel()
        {
            player.Play("Cancel");

            var transfer2 = Transfers.Transfer2;

            Transfer created = await client.CreateTransferAsync(transfer2);
            Transfer gotten = await client.CancelTransferAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }
    }
}