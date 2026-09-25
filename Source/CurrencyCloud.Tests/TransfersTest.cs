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
        Player player = new Player("Mock/Http/Recordings/Transfers.json");

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

            Assert.That(created.SourceAccountId, Is.EqualTo(transfer1.SourceAccountId));
            Assert.That(created.DestinationAccountId, Is.EqualTo(transfer1.DestinationAccountId));
            Assert.That(created.Currency, Is.EqualTo(transfer1.Currency));
            Assert.That(created.Amount, Is.EqualTo(transfer1.Amount));
            Assert.That(created.Status, Is.EqualTo(transfer1.Status));
            Assert.That(created.Reason, Is.EqualTo(transfer1.Reason));
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

            Assert.That(created, Is.EqualTo(gotten));
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

            Assert.That(found.Transfers[0].SourceAccountId, Is.EqualTo(transfer3.SourceAccountId));
            Assert.That(found.Transfers[0].DestinationAccountId, Is.EqualTo(transfer3.DestinationAccountId));
            Assert.That(found.Transfers[0].Currency, Is.EqualTo(transfer3.Currency));
            Assert.That(found.Transfers[0].Amount, Is.EqualTo(transfer3.Amount));
            Assert.That(found.Transfers[0].Status, Is.EqualTo(transfer3.Status));
            Assert.That(found.Transfers[0].Reason, Is.EqualTo(transfer3.Reason));
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

            Assert.That(found.Transfers[0].SourceAccountId, Is.EqualTo(transfer3.SourceAccountId));
            Assert.That(found.Transfers[0].DestinationAccountId, Is.EqualTo(transfer3.DestinationAccountId));
            Assert.That(found.Transfers[0].Currency, Is.EqualTo(transfer3.Currency));
            Assert.That(found.Transfers[0].Amount, Is.EqualTo(transfer3.Amount));
            Assert.That(found.Transfers[0].Status, Is.EqualTo(transfer3.Status));
            Assert.That(found.Transfers[0].Reason, Is.EqualTo(transfer3.Reason));
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

            Assert.That(created, Is.EqualTo(gotten));
        }
    }
}