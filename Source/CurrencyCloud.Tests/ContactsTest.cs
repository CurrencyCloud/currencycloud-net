using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ContactsTest
    {
        Client client = new Client();
        Player player = new Player("../../Mock/Http/Recordings/Contacts.json");

        [TestFixtureSetUp]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;

            client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey).Wait();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            player.Play("TearDown");

            client.CloseAsync().Wait();

            player.Close();
        }

        /// <summary>
        /// Successfully creates reset token.
        /// </summary>
        [Test]
        public async void CreateResetToken()
        {
            player.Play("CreateResetToken");

            var contact1 = Contacts.Contact1;

            Account account = await client.GetCurrentAccountAsync();
            contact1.AccountId = account.Id;
            Contact created = await client.CreateContactAsync(contact1);

            Assert.DoesNotThrow(async () => await client.CreateResetTokenAsync(created.LoginId));
        }

        /// <summary>
        /// Successfully creates a contact.
        /// </summary>
        [Test]
        public async void Create()
        {
            player.Play("Create");

            var contact1 = Contacts.Contact1;

            Account account = await client.GetCurrentAccountAsync();
            contact1.AccountId = account.Id;
            Contact created = await client.CreateContactAsync(contact1);

            Assert.AreEqual(contact1.FirstName, created.FirstName);
            Assert.AreEqual(contact1.LastName, created.LastName);
            Assert.AreEqual(contact1.EmailAddress, created.EmailAddress);
            Assert.AreEqual(contact1.PhoneNumber, created.PhoneNumber);
            Assert.AreEqual(contact1.YourReference, created.YourReference);
            Assert.AreEqual(contact1.MobilePhoneNumber, created.MobilePhoneNumber);
            Assert.AreEqual(contact1.LoginId, created.LoginId);
            Assert.AreEqual(contact1.Status, created.Status);
            Assert.AreEqual(contact1.Locale, created.Locale);
            Assert.AreEqual(contact1.Timezone, created.Timezone);
            Assert.AreEqual(contact1.DateOfBirth, created.DateOfBirth);
        }

        /// <summary>
        /// Successfully gets a contact.
        /// </summary>
        [Test]
        public async void Get()
        {
            player.Play("Get");

            var contact1 = Contacts.Contact1;

            Account account = await client.GetCurrentAccountAsync();
            contact1.AccountId = account.Id;
            Contact created = await client.CreateContactAsync(contact1);
            Contact gotten = await client.GetContactAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully updates a contact.
        /// </summary>
        [Test]
        public async void Update()
        {
            player.Play("Update");

            var contact1 = Contacts.Contact1;
            var contact2 = Contacts.Contact2;

            Account account = await client.GetCurrentAccountAsync();
            contact1.AccountId = account.Id;
            Contact created = await client.CreateContactAsync(contact1);
            contact2.Id = created.Id;
            contact2.AccountId = account.Id;
            Contact updated = await client.UpdateContactAsync(contact2);
            Contact gotten = await client.GetContactAsync(created.Id);

            Assert.AreEqual(gotten, updated);
        }

        /// <summary>
        /// Successfully finds a contact.
        /// </summary>
        [Test]
        public async void Find()
        {
            player.Play("Find");

            Contact current = await client.GetCurrentContactAsync();
            PaginatedContacts found = await client.FindContactsAsync(new ContactFindParameters
            {
                LoginId = current.LoginId,
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.Contains(current, found.Contacts);
        }

        /// <summary>
        /// Successfully gets current contact.
        /// </summary>
        [Test]
        public void GetCurrent()
        {
            player.Play("GetCurrent");

            Assert.DoesNotThrow(async () => {
                Contact current = await client.GetCurrentContactAsync();
            });
        }
    }
}
