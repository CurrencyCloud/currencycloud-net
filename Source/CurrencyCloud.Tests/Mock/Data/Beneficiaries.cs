namespace CurrencyCloud.Tests.Mock.Data
{
    static class Beneficiaries
    {
        public static readonly dynamic Beneficiary1 = new
        {
            BankAccountHolderName = "John Doe",
            BankCountry = "DE",
            Currency = "EUR",
            Name = "Employee Funds",

            Optional = new
            {
                Email = "john.doe@acme.com",
                BeneficiaryAddress = "23 Acacia Road",
                BeneficiaryCountry = "GB",
                AccountNumber = "13071472",
                RoutingCodeType1 = "sort_code",
                RoutingCodeValue1 = "200605",
                RoutingCodeType2 = "aba",
                RoutingCodeValue2 = "789",
                BicSwift = "COBADEFF",
                Iban = "DE89370400440532013000",
                DefaultBeneficiary = true,
                BankAddress = "4356 Wisteria Lane",
                BankName = "HSBC Bank",
                BankAccountType = "checking",
                BeneficiaryEntityType = "company",
                BeneficiaryCompanyName = "Some Company LLC",
                BeneficiaryFirstName = "John",
                BeneficiaryLastName = "Doe",
                BeneficiaryCity = "London",
                BeneficiaryPostcode = "W11 2BQ",
                BeneficiaryStateOrProvince = "TX",
                BeneficiaryDateOfBirth = "1990-07-20",
                BeneficiaryIdentificationType = "none"
            }
        };
    }
}