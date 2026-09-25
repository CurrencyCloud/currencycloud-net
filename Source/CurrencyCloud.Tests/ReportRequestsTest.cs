using NUnit.Framework;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;
using CurrencyCloud.Entity;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    public class ReportRequestsTest
    {
        Client client = new Client();
        Player player = new Player("Mock/Http/Recordings/ReportRequests.json");

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
        /// Successfully creates a conversion report.
        /// </summary>
        [Test]
        public async Task CreateConversionReport()
        {
            player.Play("CreateConversionReport");

            var report = ReportRequests.Report3;

            ReportRequest gotten = await client.CreateConversionReportAsync(new ReportParameters
                {
                    Description = "New Conversion test report",
                    UniqueRequestId = "1b3687dc-c779-4fe7-9515-00a6509632c4"
                }
            );

            Assert.That(gotten, Is.Not.Null);
            Assert.That(gotten.Status, Is.EqualTo(report.Status));
            Assert.That(gotten.Description, Is.EqualTo(report.Description));
            Assert.That(gotten.Id, Is.EqualTo(report.Id));
            Assert.That(gotten.AccountId, Is.EqualTo(report.AccountId));
            Assert.That(gotten.ContactId, Is.EqualTo(report.ContactId));
            Assert.That(gotten.CreatedAt, Is.EqualTo(report.CreatedAt));
            Assert.That(gotten.ExpirationDate, Is.EqualTo(report.ExpirationDate));
            Assert.That(gotten.FailureReason, Is.EqualTo(report.FailureReason));
            Assert.That(gotten.ReportType, Is.EqualTo(report.ReportType));
            Assert.That(gotten.ReportUrl, Is.EqualTo(report.ReportUrl));
            Assert.That(gotten.SearchParams.Description, Is.EqualTo(report.SearchParams.Description));
            Assert.That(gotten.SearchParams.Scope, Is.EqualTo(report.SearchParams.Scope));
            Assert.That(gotten.ShortReference, Is.EqualTo(report.ShortReference));
            Assert.That(gotten.UpdatedAt, Is.EqualTo(report.UpdatedAt));
        }

        /// <summary>
        /// Successfully gets a conversion report.
        /// </summary>
        [Test]
        public async Task GetConversionReport()
        {
            player.Play("GetConversionReport");

            var report = ReportRequests.Report1;

            ReportRequest gotten = await client.GetReportRequestAsync(report.Id);

            Assert.That(gotten, Is.Not.Null);
            Assert.That(gotten.Status, Is.EqualTo(report.Status));
            Assert.That(gotten.Description, Is.EqualTo(report.Description));
            Assert.That(gotten.Id, Is.EqualTo(report.Id));
            Assert.That(gotten.AccountId, Is.EqualTo(report.AccountId));
            Assert.That(gotten.ContactId, Is.EqualTo(report.ContactId));
            Assert.That(gotten.CreatedAt, Is.EqualTo(report.CreatedAt));
            Assert.That(gotten.ExpirationDate, Is.EqualTo(report.ExpirationDate));
            Assert.That(gotten.FailureReason, Is.EqualTo(report.FailureReason));
            Assert.That(gotten.ReportType, Is.EqualTo(report.ReportType));
            Assert.That(gotten.ReportUrl, Is.EqualTo(report.ReportUrl));
            Assert.That(gotten.SearchParams, Is.EqualTo(report.SearchParams));
            Assert.That(gotten.ShortReference, Is.EqualTo(report.ShortReference));
            Assert.That(gotten.UpdatedAt, Is.EqualTo(report.UpdatedAt));
        }

        /// <summary>
        /// Successfully creates a payment report.
        /// </summary>
        [Test]
        public async Task CreatePaymentReport()
        {
            player.Play("CreatePaymentReport");

            var report = ReportRequests.Report4;

            ReportRequest gotten = await client.CreatePaymentReportAsync(new ReportParameters
                {
                    Description = "New Payment test report",
                    UniqueRequestId = "2422a1ee-b376-4358-a4f2-560aa953c461"
                }
            );

            Assert.That(gotten, Is.Not.Null);
            Assert.That(gotten.Status, Is.EqualTo(report.Status));
            Assert.That(gotten.Description, Is.EqualTo(report.Description));
            Assert.That(gotten.Id, Is.EqualTo(report.Id));
            Assert.That(gotten.AccountId, Is.EqualTo(report.AccountId));
            Assert.That(gotten.ContactId, Is.EqualTo(report.ContactId));
            Assert.That(gotten.CreatedAt, Is.EqualTo(report.CreatedAt));
            Assert.That(gotten.ExpirationDate, Is.EqualTo(report.ExpirationDate));
            Assert.That(gotten.FailureReason, Is.EqualTo(report.FailureReason));
            Assert.That(gotten.ReportType, Is.EqualTo(report.ReportType));
            Assert.That(gotten.ReportUrl, Is.EqualTo(report.ReportUrl));
            Assert.That(gotten.SearchParams.Description, Is.EqualTo(report.SearchParams.Description));
            Assert.That(gotten.SearchParams.Scope, Is.EqualTo(report.SearchParams.Scope));
            Assert.That(gotten.ShortReference, Is.EqualTo(report.ShortReference));
            Assert.That(gotten.UpdatedAt, Is.EqualTo(report.UpdatedAt));
        }

        /// <summary>
        /// Successfully gets a payment report.
        /// </summary>
        [Test]
        public async Task GetPaymentReport()
        {
            player.Play("GetPaymentReport");

            var report = ReportRequests.Report2;

            ReportRequest gotten = await client.GetReportRequestAsync(report.Id);

            Assert.That(gotten, Is.Not.Null);
            Assert.That(gotten.Status, Is.EqualTo(report.Status));
            Assert.That(gotten.Description, Is.EqualTo(report.Description));
            Assert.That(gotten.Id, Is.EqualTo(report.Id));
            Assert.That(gotten.AccountId, Is.EqualTo(report.AccountId));
            Assert.That(gotten.ContactId, Is.EqualTo(report.ContactId));
            Assert.That(gotten.CreatedAt, Is.EqualTo(report.CreatedAt));
            Assert.That(gotten.ExpirationDate, Is.EqualTo(report.ExpirationDate));
            Assert.That(gotten.FailureReason, Is.EqualTo(report.FailureReason));
            Assert.That(gotten.ReportType, Is.EqualTo(report.ReportType));
            Assert.That(gotten.ReportUrl, Is.EqualTo(report.ReportUrl));
            Assert.That(gotten.SearchParams, Is.EqualTo(report.SearchParams));
            Assert.That(gotten.ShortReference, Is.EqualTo(report.ShortReference));
            Assert.That(gotten.UpdatedAt, Is.EqualTo(report.UpdatedAt));
        }

        /// <summary>
        /// Successfully finds report requests without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var report1 = ReportRequests.Report1;
            var report2 = ReportRequests.Report2;

            PaginatedReportRequests found = await client.FindReportRequestsAsync();

            Assert.That(found.ReportRequests, Is.Not.Empty);
            Assert.That(found.Pagination.TotalEntries, Is.EqualTo(found.ReportRequests.Count));
            Assert.That(found.ReportRequests[0].Status, Is.EqualTo(report1.Status));
            Assert.That(found.ReportRequests[0].Description, Is.EqualTo(report1.Description));
            Assert.That(found.ReportRequests[0].Id, Is.EqualTo(report1.Id));
            Assert.That(found.ReportRequests[0].AccountId, Is.EqualTo(report1.AccountId));
            Assert.That(found.ReportRequests[0].ContactId, Is.EqualTo(report1.ContactId));
            Assert.That(found.ReportRequests[0].CreatedAt, Is.EqualTo(report1.CreatedAt));
            Assert.That(found.ReportRequests[0].ExpirationDate, Is.EqualTo(report1.ExpirationDate));
            Assert.That(found.ReportRequests[0].FailureReason, Is.EqualTo(report1.FailureReason));
            Assert.That(found.ReportRequests[0].ReportType, Is.EqualTo(report1.ReportType));
            Assert.That(found.ReportRequests[0].ReportUrl, Is.EqualTo(report1.ReportUrl));
            Assert.That(found.ReportRequests[0].SearchParams, Is.EqualTo(report1.SearchParams));
            Assert.That(found.ReportRequests[0].ShortReference, Is.EqualTo(report1.ShortReference));
            Assert.That(found.ReportRequests[0].UpdatedAt, Is.EqualTo(report1.UpdatedAt));

            Assert.That(found.ReportRequests[1].Status, Is.EqualTo(report2.Status));
            Assert.That(found.ReportRequests[1].Description, Is.EqualTo(report2.Description));
            Assert.That(found.ReportRequests[1].Id, Is.EqualTo(report2.Id));
            Assert.That(found.ReportRequests[1].AccountId, Is.EqualTo(report2.AccountId));
            Assert.That(found.ReportRequests[1].ContactId, Is.EqualTo(report2.ContactId));
            Assert.That(found.ReportRequests[1].CreatedAt, Is.EqualTo(report2.CreatedAt));
            Assert.That(found.ReportRequests[1].ExpirationDate, Is.EqualTo(report2.ExpirationDate));
            Assert.That(found.ReportRequests[1].FailureReason, Is.EqualTo(report2.FailureReason));
            Assert.That(found.ReportRequests[1].ReportType, Is.EqualTo(report2.ReportType));
            Assert.That(found.ReportRequests[1].ReportUrl, Is.EqualTo(report2.ReportUrl));
            Assert.That(found.ReportRequests[1].SearchParams, Is.EqualTo(report2.SearchParams));
            Assert.That(found.ReportRequests[1].ShortReference, Is.EqualTo(report2.ShortReference));
            Assert.That(found.ReportRequests[1].UpdatedAt, Is.EqualTo(report2.UpdatedAt));
        }

        /// <summary>
        /// Successfully finds report requests with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var report1 = ReportRequests.Report1;

            PaginatedReportRequests found = await client.FindReportRequestsAsync(
                new ReportRequestFindParameters { ReportType = "conversion"}
                );

            Assert.That(found.ReportRequests, Is.Not.Empty);
            Assert.That(found.Pagination.TotalEntries, Is.EqualTo(found.ReportRequests.Count));
            Assert.That(found.ReportRequests[0].Status, Is.EqualTo(report1.Status));
            Assert.That(found.ReportRequests[0].Description, Is.EqualTo(report1.Description));
            Assert.That(found.ReportRequests[0].Id, Is.EqualTo(report1.Id));
            Assert.That(found.ReportRequests[0].AccountId, Is.EqualTo(report1.AccountId));
            Assert.That(found.ReportRequests[0].ContactId, Is.EqualTo(report1.ContactId));
            Assert.That(found.ReportRequests[0].CreatedAt, Is.EqualTo(report1.CreatedAt));
            Assert.That(found.ReportRequests[0].ExpirationDate, Is.EqualTo(report1.ExpirationDate));
            Assert.That(found.ReportRequests[0].FailureReason, Is.EqualTo(report1.FailureReason));
            Assert.That(found.ReportRequests[0].ReportType, Is.EqualTo(report1.ReportType));
            Assert.That(found.ReportRequests[0].ReportUrl, Is.EqualTo(report1.ReportUrl));
            Assert.That(found.ReportRequests[0].SearchParams, Is.EqualTo(report1.SearchParams));
            Assert.That(found.ReportRequests[0].ShortReference, Is.EqualTo(report1.ShortReference));
            Assert.That(found.ReportRequests[0].UpdatedAt, Is.EqualTo(report1.UpdatedAt));
        }
    }
}