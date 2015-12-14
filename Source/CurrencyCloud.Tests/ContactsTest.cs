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

            client.InitializeAsync(credentials.ApiServer, credentials.LoginId, credentials.APIkey).Wait();
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
            Contact created = await client.CreateContactAsync(account.Id, contact1.FirstName, contact1.LastName, contact1.EmailAddress, contact1.PhoneNumber, contact1.Optional);

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
            Contact created = await client.CreateContactAsync(account.Id, contact1.FirstName, contact1.LastName, contact1.EmailAddress, contact1.PhoneNumber, contact1.Optional);

            Assert.AreEqual(contact1.FirstName, created.FirstName);
            Assert.AreEqual(contact1.LastName, created.LastName);
            Assert.AreEqual(contact1.EmailAddress, created.EmailAddress);
            Assert.AreEqual(contact1.PhoneNumber, created.PhoneNumber);
            Assert.AreEqual(contact1.Optional.YourReference, created.YourReference);
            Assert.AreEqual(contact1.Optional.MobilePhoneNumber, created.MobilePhoneNumber);
            Assert.AreEqual(contact1.Optional.LoginId, created.LoginId);
            Assert.AreEqual(contact1.Optional.Status, created.Status);
            Assert.AreEqual(contact1.Optional.Locale, created.Locale);
            Assert.AreEqual(contact1.Optional.Timezone, created.Timezone);
            Assert.AreEqual(contact1.Optional.DateOfBirth, created.DateOfBirth);
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
            Contact created = await client.CreateContactAsync(account.Id, contact1.FirstName, contact1.LastName, contact1.EmailAddress, contact1.PhoneNumber, contact1.Optional);
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
            Contact created = await client.CreateContactAsync(account.Id, contact1.FirstName, contact1.LastName, contact1.EmailAddress, contact1.PhoneNumber, contact1.Optional);
            Contact updated = await client.UpdateContactAsync(created.Id, contact2);
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
            PaginatedContacts found = await client.FindContactsAsync(new
            {
                LoginId = current.LoginId,
                Order = "created_at",
                OrderAscDesc = "desc",
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
