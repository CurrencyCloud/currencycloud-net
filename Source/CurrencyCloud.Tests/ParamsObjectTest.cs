using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyCloud.Entity;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ParamsObjectTest
    {
        private static AccountCreateRequest AccountCreateRequestWithLists()
        {
            return new AccountCreateRequest("Acme Ltd", "company", "12 Steward St", "London", "E1 6FQ", "GB")
            {
                ExpectedTransactionCurrencies = new List<string> { "GBP" },
                ExpectedTransactionCountries = new List<string> { "US", "GB" }
            };
        }

        [Test]
        public async Task AccountCreateRequestListsAreSentAsArrays()
        {
            var body = await ParamsObject.CreateFromStaticObject(AccountCreateRequestWithLists())
                .buildFormUrlBodyFromParams().ReadAsStringAsync();

            Assert.That(body, Does.Contain("expected_transaction_currencies%5B%5D=GBP"));
            Assert.That(body, Does.Contain("expected_transaction_countries%5B%5D=US&expected_transaction_countries%5B%5D=GB"));
            Assert.That(body, Does.Not.Contain("expected_transaction_currencies="));
            Assert.That(body, Does.Not.Contain("expected_transaction_countries="));
        }

        [Test]
        public async Task AccountComplianceSettingsListsAreSentAsArrays()
        {
            var settings = new AccountComplianceSettings("e277c9f9-679f-454f-8367-274b3ff977ff")
            {
                ExpectedTransactionCurrencies = new List<string> { "GBP" },
                ExpectedTransactionCountries = new List<string> { "US", "GB" }
            };

            var body = await ParamsObject.CreateFromStaticObject(settings)
                .buildFormUrlBodyFromParams().ReadAsStringAsync();

            Assert.That(body, Does.Contain("expected_transaction_currencies%5B%5D=GBP"));
            Assert.That(body, Does.Contain("expected_transaction_countries%5B%5D=US&expected_transaction_countries%5B%5D=GB"));
            Assert.That(body, Does.Not.Contain("expected_transaction_countries="));
        }

        [Test]
        public void ListsAreSentAsArraysInQueryString()
        {
            var query = ParamsObject.CreateFromStaticObject(AccountCreateRequestWithLists()).ToQueryString();

            Assert.That(query, Does.Contain("expected_transaction_currencies[]=GBP"));
            Assert.That(query, Does.Contain("expected_transaction_countries[]=US&expected_transaction_countries[]=GB"));
        }

        [Test]
        public async Task BeneficiaryAddressesAreSentAsJoinedStrings()
        {
            var beneficiary = new Beneficiary("John Doe", "DE", "EUR", "Employee Funds")
            {
                BeneficiaryAddress = new List<string> { "23 Acacia Road", "Peasden" },
                BankAddress = new List<string> { "KAISERSTRASSE 16", "60311 Frankfurt" }
            };

            var body = await ParamsObject.CreateFromStaticObject(beneficiary)
                .buildFormUrlBodyFromParams().ReadAsStringAsync();

            Assert.That(body, Does.Contain("beneficiary_address=23+Acacia+Road+%0D%0APeasden"));
            Assert.That(body, Does.Contain("bank_address=KAISERSTRASSE+16+%0D%0A60311+Frankfurt"));
            Assert.That(body, Does.Not.Contain("%5B%5D"));
        }

        [Test]
        public void NullListsAreOmitted()
        {
            var request = new AccountCreateRequest("Acme Ltd", "company", "12 Steward St", "London", "E1 6FQ", "GB");

            var query = ParamsObject.CreateFromStaticObject(request).ToQueryString();

            Assert.That(query, Does.Not.Contain("expected_transaction"));
        }
    }
}
