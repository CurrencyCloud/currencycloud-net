using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using CurrencyCloud.Entity.List;
using System.Threading.Tasks;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class AccountsTest
    {
        Client client = new Client();
        Player player = new Player("Mock/Http/Recordings/Accounts.json");

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
        /// Successfully creates an account.
        /// </summary>
        [Test]
        public async Task Create()
        {
            player.Play("Create");

            var account1 = Accounts.Account1;

            Account created = await client.CreateAccountAsync(account1);

            Assert.That(created.Status, Is.Not.Null.And.Not.Empty);
            Assert.That(created.AccountName, Is.EqualTo(account1.AccountName));
            Assert.That(created.LegalEntityType, Is.EqualTo(account1.LegalEntityType));
            Assert.That(created.YourReference, Is.EqualTo(account1.YourReference));
            Assert.That(created.Street, Is.EqualTo(account1.Street));
            Assert.That(created.City, Is.EqualTo(account1.City));
            Assert.That(created.StateOrProvince, Is.EqualTo(account1.StateOrProvince));
            Assert.That(created.PostalCode, Is.EqualTo(account1.PostalCode));
            Assert.That(created.Country, Is.EqualTo(account1.Country));
            Assert.That(created.SpreadTable, Is.EqualTo(account1.SpreadTable));
            Assert.That(created.IdentificationType, Is.EqualTo(account1.IdentificationType));
            Assert.That(created.Brand, Is.EqualTo(account1.Brand));
            Assert.That(created.ApiTrading, Is.EqualTo(account1.ApiTrading));
            Assert.That(created.OnlineTrading, Is.EqualTo(account1.OnlineTrading));
            Assert.That(created.PhoneTrading, Is.EqualTo(account1.PhoneTrading));
            Assert.That(created.TermsAndConditionsAccepted, Is.EqualTo(account1.TermsAndConditionsAccepted));
        }

        /// <summary>
        /// Successfully creates an account.
        /// </summary>
        [Test]
        public async Task CreateWithAccountCreateRequest()
        {
            player.Play("CreateWithAccountCreateRequest");

            var createRequest = Accounts.AccountCreateRequest;
            Account created = await client.CreateAccountAsync(createRequest);

            Assert.That(created.Status, Is.Not.Null.And.Not.Empty);

            Assert.That(created.AccountName, Is.EqualTo(createRequest.AccountName));
            Assert.That(created.LegalEntityType, Is.EqualTo(createRequest.LegalEntityType));
            Assert.That(created.LegalEntitySubType, Is.EqualTo(createRequest.LegalEntitySubType));
            Assert.That(created.YourReference, Is.EqualTo(createRequest.YourReference));
            Assert.That(created.Street, Is.EqualTo(createRequest.Street));
            Assert.That(created.City, Is.EqualTo(createRequest.City));
            Assert.That(created.StateOrProvince, Is.EqualTo(createRequest.StateOrProvince));
            Assert.That(created.PostalCode, Is.EqualTo(createRequest.PostalCode));
            Assert.That(created.Country, Is.EqualTo(createRequest.Country));
            Assert.That(created.SpreadTable, Is.EqualTo(createRequest.SpreadTable));
            Assert.That(created.IdentificationType, Is.EqualTo(createRequest.IdentificationType));
            Assert.That(created.IdentificationExpiration, Is.EqualTo(createRequest.IdentificationExpiration));
            Assert.That(created.IdentificationIssuer, Is.EqualTo(createRequest.IdentificationIssuer));
            Assert.That(created.Brand, Is.EqualTo(createRequest.Brand));
            Assert.That(created.ApiTrading, Is.EqualTo(createRequest.ApiTrading));
            Assert.That(created.OnlineTrading, Is.EqualTo(createRequest.OnlineTrading));
            Assert.That(created.PhoneTrading, Is.EqualTo(createRequest.PhoneTrading));
            Assert.That(created.TermsAndConditionsAccepted, Is.EqualTo(createRequest.TermsAndConditionsAccepted));
        }

        /// <summary>
        /// Successfully gets an account.
        /// </summary>
        [Test]
        public async Task Get()
        {
            player.Play("Get");

            var account1 = Accounts.Account1;

            Account created = await client.CreateAccountAsync(account1);
            Account gotten = await client.GetAccountAsync(created.Id);

            Assert.That(created, Is.EqualTo(gotten));
        }

        /// <summary>
        /// Successfully updates an account.
        /// </summary>
        [Test]
        public async Task Update()
        {
            player.Play("Update");

            var account1 = Accounts.Account1;
            var account2 = Accounts.Account2;

            Account created = await client.CreateAccountAsync(account1);
            account2.Id = created.Id;
            Account updated = await client.UpdateAccountAsync(account2);
            Account gotten = await client.GetAccountAsync(created.Id);

            Assert.That(updated, Is.EqualTo(gotten));
        }

        /// <summary>
        /// Successfully finds an account with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            Account current = await client.GetCurrentAccountAsync();
            PaginatedAccounts found = await client.FindAccountsAsync(new AccountFindParameters
            {
                AccountName = current.AccountName,
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.That(found.Accounts, Does.Contain(current));
        }

        /// <summary>
        /// Successfully finds an account without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            Account current = await client.GetCurrentAccountAsync();
            PaginatedAccounts found = await client.FindAccountsAsync();

            Assert.That(found.Accounts, Does.Contain(current));
        }

        /// <summary>
        /// Successfully gets current account.
        /// </summary>
        [Test]
        public void GetCurrent()
        {
            player.Play("GetCurrent");

            Assert.DoesNotThrowAsync(async () => {
                Account current = await client.GetCurrentAccountAsync();
            });
        }

        /// <summary>
        /// Successfully gets payment charges settings for given account.
        /// </summary>
        [Test]
        public async Task GetChargesSettings()
        {
            player.Play("GetChargesSettings");

            var settings = Accounts.PaymentCharges;

            PaymentChargesSettingsList charges = await client.GetPaymentChargesSettingsAsync(settings.AccountId);
            Assert.That(settings.AccountId, Is.EqualTo(charges.PaymentChargesSettings[0].AccountId));
            Assert.That(settings.ChargeSettingsId, Is.EqualTo(charges.PaymentChargesSettings[0].ChargeSettingsId));
        }

        /// <summary>
        /// Successfully manages given Account's Payment Charge Settings.
        /// </summary>
        [Test]
        public async Task ManageChargesSettings()
        {
            player.Play("ManageChargesSettings");

            var settings = Accounts.PaymentCharges;

            PaymentChargesSettings charges = await client.ManageAccountPaymentChargesSettingsAsync(settings);
            Assert.That(charges, Is.EqualTo(settings));
        }
        /// <summary>
        /// Successfully gets payment charges settings for given account.
        /// </summary>
        [Test]
        public async Task GetComplianceSettings()
        {
            player.Play("GetComplianceSettings");

            var expected = Accounts.ComplianceSettings;

            AccountComplianceSettings actual = await client.GetComplianceSettingsAsync(expected.AccountId);

            Assert.That(actual.AccountId, Is.EqualTo(expected.AccountId));
            Assert.That(actual.IndustryType, Is.EqualTo(expected.IndustryType));
            Assert.That(actual.CountryOfIncorporation, Is.EqualTo(expected.CountryOfIncorporation));
            Assert.That(actual.DateOfIncorporation, Is.EqualTo(expected.DateOfIncorporation));
            Assert.That(actual.BusinessWebsiteUrl, Is.EqualTo(expected.BusinessWebsiteUrl));
            Assert.That(actual.ExpectedTransactionCountries, Is.EqualTo(expected.ExpectedTransactionCountries));
            Assert.That(actual.ExpectedTransactionCurrencies, Is.EqualTo(expected.ExpectedTransactionCurrencies));
            Assert.That(actual.ExpectedMonthlyActivityVolume, Is.EqualTo(expected.ExpectedMonthlyActivityVolume));
            Assert.That(actual.ExpectedMonthlyActivityValue, Is.EqualTo(expected.ExpectedMonthlyActivityValue));
            Assert.That(actual.TaxIdentification, Is.EqualTo(expected.TaxIdentification));
            Assert.That(actual.NationalIdentification, Is.EqualTo(expected.NationalIdentification));
            Assert.That(actual.CountryOfCitizenship, Is.EqualTo(expected.CountryOfCitizenship));
            Assert.That(actual.TradingAddressStreet, Is.EqualTo(expected.TradingAddressStreet));
            Assert.That(actual.TradingAddressCity, Is.EqualTo(expected.TradingAddressCity));
            Assert.That(actual.TradingAddressState, Is.EqualTo(expected.TradingAddressState));
            Assert.That(actual.TradingAddressPostalcode, Is.EqualTo(expected.TradingAddressPostalcode));
            Assert.That(actual.TradingAddressCountry, Is.EqualTo(expected.TradingAddressCountry));
            Assert.That(actual.CustomerRisk, Is.EqualTo(expected.CustomerRisk));
        }

        /// <summary>
        /// Successfully manages given Account's Compliance Settings.
        /// </summary>
        [Test]
        public async Task ManageComplianceSettings()
        {
            player.Play("ManageComplianceSettings");

            var expected = Accounts.ComplianceSettings;

            AccountComplianceSettings actual = await client.ManageComplianceSettingsAsync(expected);

            Assert.That(actual.AccountId, Is.EqualTo(expected.AccountId));
            Assert.That(actual.IndustryType, Is.EqualTo(expected.IndustryType));
            Assert.That(actual.CountryOfIncorporation, Is.EqualTo(expected.CountryOfIncorporation));
            Assert.That(actual.DateOfIncorporation, Is.EqualTo(expected.DateOfIncorporation));
            Assert.That(actual.BusinessWebsiteUrl, Is.EqualTo(expected.BusinessWebsiteUrl));
            Assert.That(actual.ExpectedTransactionCountries, Is.EqualTo(expected.ExpectedTransactionCountries));
            Assert.That(actual.ExpectedTransactionCurrencies, Is.EqualTo(expected.ExpectedTransactionCurrencies));
            Assert.That(actual.ExpectedMonthlyActivityVolume, Is.EqualTo(expected.ExpectedMonthlyActivityVolume));
            Assert.That(actual.ExpectedMonthlyActivityValue, Is.EqualTo(expected.ExpectedMonthlyActivityValue));
            Assert.That(actual.TaxIdentification, Is.EqualTo(expected.TaxIdentification));
            Assert.That(actual.NationalIdentification, Is.EqualTo(expected.NationalIdentification));
            Assert.That(actual.CountryOfCitizenship, Is.EqualTo(expected.CountryOfCitizenship));
            Assert.That(actual.TradingAddressStreet, Is.EqualTo(expected.TradingAddressStreet));
            Assert.That(actual.TradingAddressCity, Is.EqualTo(expected.TradingAddressCity));
            Assert.That(actual.TradingAddressState, Is.EqualTo(expected.TradingAddressState));
            Assert.That(actual.TradingAddressPostalcode, Is.EqualTo(expected.TradingAddressPostalcode));
            Assert.That(actual.TradingAddressCountry, Is.EqualTo(expected.TradingAddressCountry));
            Assert.That(actual.CustomerRisk, Is.EqualTo(expected.CustomerRisk));
        }
    }
}
