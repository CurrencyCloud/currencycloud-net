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
        Player player = new Player("/../../Mock/Http/Recordings/ReportRequests.json");

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

            Assert.IsNotNull(gotten);
            Assert.AreEqual(report.Status, gotten.Status);
            Assert.AreEqual(report.Description, gotten.Description);
            Assert.AreEqual(report.Id, gotten.Id);
            Assert.AreEqual(report.AccountId, gotten.AccountId);
            Assert.AreEqual(report.ContactId, gotten.ContactId);
            Assert.AreEqual(report.CreatedAt, gotten.CreatedAt);
            Assert.AreEqual(report.ExpirationDate, gotten.ExpirationDate);
            Assert.AreEqual(report.FailureReason, gotten.FailureReason);
            Assert.AreEqual(report.ReportType, gotten.ReportType);
            Assert.AreEqual(report.ReportUrl, gotten.ReportUrl);
            Assert.AreEqual(report.SearchParams.Description, gotten.SearchParams.Description);
            Assert.AreEqual(report.SearchParams.Scope, gotten.SearchParams.Scope);
            Assert.AreEqual(report.ShortReference, gotten.ShortReference);
            Assert.AreEqual(report.UpdatedAt, gotten.UpdatedAt);
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

            Assert.IsNotNull(gotten);
            Assert.AreEqual(report.Status, gotten.Status);
            Assert.AreEqual(report.Description, gotten.Description);
            Assert.AreEqual(report.Id, gotten.Id);
            Assert.AreEqual(report.AccountId, gotten.AccountId);
            Assert.AreEqual(report.ContactId, gotten.ContactId);
            Assert.AreEqual(report.CreatedAt, gotten.CreatedAt);
            Assert.AreEqual(report.ExpirationDate, gotten.ExpirationDate);
            Assert.AreEqual(report.FailureReason, gotten.FailureReason);
            Assert.AreEqual(report.ReportType, gotten.ReportType);
            Assert.AreEqual(report.ReportUrl, gotten.ReportUrl);
            Assert.AreEqual(report.SearchParams, gotten.SearchParams);
            Assert.AreEqual(report.ShortReference, gotten.ShortReference);
            Assert.AreEqual(report.UpdatedAt, gotten.UpdatedAt);
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

            Assert.IsNotNull(gotten);
            Assert.AreEqual(report.Status, gotten.Status);
            Assert.AreEqual(report.Description, gotten.Description);
            Assert.AreEqual(report.Id, gotten.Id);
            Assert.AreEqual(report.AccountId, gotten.AccountId);
            Assert.AreEqual(report.ContactId, gotten.ContactId);
            Assert.AreEqual(report.CreatedAt, gotten.CreatedAt);
            Assert.AreEqual(report.ExpirationDate, gotten.ExpirationDate);
            Assert.AreEqual(report.FailureReason, gotten.FailureReason);
            Assert.AreEqual(report.ReportType, gotten.ReportType);
            Assert.AreEqual(report.ReportUrl, gotten.ReportUrl);
            Assert.AreEqual(report.SearchParams.Description, gotten.SearchParams.Description);
            Assert.AreEqual(report.SearchParams.Scope, gotten.SearchParams.Scope);
            Assert.AreEqual(report.ShortReference, gotten.ShortReference);
            Assert.AreEqual(report.UpdatedAt, gotten.UpdatedAt);
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

            Assert.IsNotNull(gotten);
            Assert.AreEqual(report.Status, gotten.Status);
            Assert.AreEqual(report.Description, gotten.Description);
            Assert.AreEqual(report.Id, gotten.Id);
            Assert.AreEqual(report.AccountId, gotten.AccountId);
            Assert.AreEqual(report.ContactId, gotten.ContactId);
            Assert.AreEqual(report.CreatedAt, gotten.CreatedAt);
            Assert.AreEqual(report.ExpirationDate, gotten.ExpirationDate);
            Assert.AreEqual(report.FailureReason, gotten.FailureReason);
            Assert.AreEqual(report.ReportType, gotten.ReportType);
            Assert.AreEqual(report.ReportUrl, gotten.ReportUrl);
            Assert.AreEqual(report.SearchParams, gotten.SearchParams);
            Assert.AreEqual(report.ShortReference, gotten.ShortReference);
            Assert.AreEqual(report.UpdatedAt, gotten.UpdatedAt);
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

            Assert.IsNotEmpty(found.ReportRequests);
            Assert.AreEqual(found.ReportRequests.Count, found.Pagination.TotalEntries);
            Assert.AreEqual(report1.Status, found.ReportRequests[0].Status);
            Assert.AreEqual(report1.Description, found.ReportRequests[0].Description);
            Assert.AreEqual(report1.Id, found.ReportRequests[0].Id);
            Assert.AreEqual(report1.AccountId, found.ReportRequests[0].AccountId);
            Assert.AreEqual(report1.ContactId, found.ReportRequests[0].ContactId);
            Assert.AreEqual(report1.CreatedAt, found.ReportRequests[0].CreatedAt);
            Assert.AreEqual(report1.ExpirationDate, found.ReportRequests[0].ExpirationDate);
            Assert.AreEqual(report1.FailureReason, found.ReportRequests[0].FailureReason);
            Assert.AreEqual(report1.ReportType, found.ReportRequests[0].ReportType);
            Assert.AreEqual(report1.ReportUrl, found.ReportRequests[0].ReportUrl);
            Assert.AreEqual(report1.SearchParams, found.ReportRequests[0].SearchParams);
            Assert.AreEqual(report1.ShortReference, found.ReportRequests[0].ShortReference);
            Assert.AreEqual(report1.UpdatedAt, found.ReportRequests[0].UpdatedAt);

            Assert.AreEqual(report2.Status, found.ReportRequests[1].Status);
            Assert.AreEqual(report2.Description, found.ReportRequests[1].Description);
            Assert.AreEqual(report2.Id, found.ReportRequests[1].Id);
            Assert.AreEqual(report2.AccountId, found.ReportRequests[1].AccountId);
            Assert.AreEqual(report2.ContactId, found.ReportRequests[1].ContactId);
            Assert.AreEqual(report2.CreatedAt, found.ReportRequests[1].CreatedAt);
            Assert.AreEqual(report2.ExpirationDate, found.ReportRequests[1].ExpirationDate);
            Assert.AreEqual(report2.FailureReason, found.ReportRequests[1].FailureReason);
            Assert.AreEqual(report2.ReportType, found.ReportRequests[1].ReportType);
            Assert.AreEqual(report2.ReportUrl, found.ReportRequests[1].ReportUrl);
            Assert.AreEqual(report2.SearchParams, found.ReportRequests[1].SearchParams);
            Assert.AreEqual(report2.ShortReference, found.ReportRequests[1].ShortReference);
            Assert.AreEqual(report2.UpdatedAt, found.ReportRequests[1].UpdatedAt);
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

            Assert.IsNotEmpty(found.ReportRequests);
            Assert.AreEqual(found.ReportRequests.Count, found.Pagination.TotalEntries);
            Assert.AreEqual(report1.Status, found.ReportRequests[0].Status);
            Assert.AreEqual(report1.Description, found.ReportRequests[0].Description);
            Assert.AreEqual(report1.Id, found.ReportRequests[0].Id);
            Assert.AreEqual(report1.AccountId, found.ReportRequests[0].AccountId);
            Assert.AreEqual(report1.ContactId, found.ReportRequests[0].ContactId);
            Assert.AreEqual(report1.CreatedAt, found.ReportRequests[0].CreatedAt);
            Assert.AreEqual(report1.ExpirationDate, found.ReportRequests[0].ExpirationDate);
            Assert.AreEqual(report1.FailureReason, found.ReportRequests[0].FailureReason);
            Assert.AreEqual(report1.ReportType, found.ReportRequests[0].ReportType);
            Assert.AreEqual(report1.ReportUrl, found.ReportRequests[0].ReportUrl);
            Assert.AreEqual(report1.SearchParams, found.ReportRequests[0].SearchParams);
            Assert.AreEqual(report1.ShortReference, found.ReportRequests[0].ShortReference);
            Assert.AreEqual(report1.UpdatedAt, found.ReportRequests[0].UpdatedAt);
        }
    }
}