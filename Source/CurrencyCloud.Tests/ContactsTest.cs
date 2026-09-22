using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ContactsTest
    {
        Client client = new Client();
        Player player = new Player("Mock/Http/Recordings/Contacts.json");

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

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Successfully creates a contact.
        /// </summary>
        [Test]
        public async Task Create()
        {
            player.Play("Create");

            var contact1 = Contacts.Contact1;

            Account account = await client.GetCurrentAccountAsync();
            contact1.AccountId = account.Id;
            if (!Authentication.ApiServer.Url.Contains("localhost"))
                contact1.LoginId = RandomString(10);
            Contact created = await client.CreateContactAsync(contact1);

            Assert.That(created.FirstName, Is.EqualTo(contact1.FirstName));
            Assert.That(created.LastName, Is.EqualTo(contact1.LastName));
            Assert.That(created.EmailAddress, Is.EqualTo(contact1.EmailAddress));
            Assert.That(created.PhoneNumber, Is.EqualTo(contact1.PhoneNumber));
            Assert.That(created.YourReference, Is.EqualTo(contact1.YourReference));
            Assert.That(created.MobilePhoneNumber, Is.EqualTo(contact1.MobilePhoneNumber));
            Assert.That(created.LoginId, Is.EqualTo(contact1.LoginId));
            Assert.That(created.Status, Is.EqualTo(contact1.Status));
            Assert.That(created.Locale, Is.EqualTo(contact1.Locale));
            Assert.That(created.Timezone, Is.EqualTo(contact1.Timezone));
            Assert.That(created.DateOfBirth, Is.EqualTo(contact1.DateOfBirth));
        }

        /// <summary>
        /// Successfully gets a contact.
        /// </summary>
        [Test]
        public async Task Get()
        {
            player.Play("Get");

            var contact1 = Contacts.Contact1;

            Account account = await client.GetCurrentAccountAsync();
            contact1.AccountId = account.Id;
            if (!Authentication.ApiServer.Url.Contains("localhost"))
                contact1.LoginId = RandomString(10);
            Contact created = await client.CreateContactAsync(contact1);
            Contact gotten = await client.GetContactAsync(created.Id);

            Assert.That(created, Is.EqualTo(gotten));
        }

        /// <summary>
        /// Successfully updates a contact.
        /// </summary>
        [Test]
        public async Task Update()
        {
            player.Play("Update");

            var contact1 = Contacts.Contact1;
            var contact2 = Contacts.Contact2;

            Account account = await client.GetCurrentAccountAsync();
            contact1.AccountId = account.Id;
            if (!Authentication.ApiServer.Url.Contains("localhost"))
            {
                contact1.LoginId = RandomString(10);
                contact2.LoginId = contact1.LoginId;
            }
            Contact created = await client.CreateContactAsync(contact1);
            contact2.Id = created.Id;
            contact2.AccountId = account.Id;
            Contact updated = await client.UpdateContactAsync(contact2);
            Contact gotten = await client.GetContactAsync(created.Id);

            Assert.That(updated, Is.EqualTo(gotten));
        }

        /// <summary>
        /// Successfully finds a contact with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            Contact current = await client.GetCurrentContactAsync();
            PaginatedContacts found = await client.FindContactsAsync(new ContactFindParameters
            {
                LoginId = current.LoginId,
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.That(found.Contacts, Does.Contain(current));
        }

        /// <summary>
        /// Successfully finds a contact without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            Contact current = await client.GetCurrentContactAsync();
            PaginatedContacts found = await client.FindContactsAsync();

            Assert.That(found.Contacts, Does.Contain(current));
        }

        /// <summary>
        /// Successfully gets current contact.
        /// </summary>
        [Test]
        public void GetCurrent()
        {
            player.Play("GetCurrent");

            Assert.DoesNotThrowAsync(async () => {
                Contact current = await client.GetCurrentContactAsync();
            });
        }
    }
}
