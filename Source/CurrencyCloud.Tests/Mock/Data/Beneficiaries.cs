using System;

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
                BeneficiaryAddress = "23 Acacia Road",
                BeneficiaryCountry = "GB",
                BicSwift = "COBADEFF",
                Iban = "DE89370400440532013000",
                DefaultBeneficiary = true,
                BankAddress = "KAISERSTRASSE 16",
                BankName = "COMMERZBANK AG",
                BankAccountType = "checking",
                BeneficiaryEntityType = "company",
                BeneficiaryCompanyName = "Some Company LLC",
                BeneficiaryFirstName = "John",
                BeneficiaryLastName = "Doe",
                BeneficiaryCity = "London",
                BeneficiaryPostcode = "W11 2BQ",
                BeneficiaryStateOrProvince = "TX",
                BeneficiaryDateOfBirth = new DateTime(1990, 7, 20),
                BeneficiaryIdentificationType = "none"
            }
        };

        public static readonly dynamic Beneficiary2 = new
        {
            BankAccountHolderName = "Martin McFly",
            BankCountry = "US",
            Currency = "USD",
            Name = "Employee Funds",
            BeneficiaryAddress = "9303 Roslyndale Ave.",
            BeneficiaryCountry = "US",
            AccountNumber = "13071472",
            RoutingCodeType1 = "sort_code",
            RoutingCodeValue1 = "200606",
            RoutingCodeType2 = "aba",
            RoutingCodeValue2 = "780",
            BicSwift = "USSWIFT",
            Iban = "US89370400440532013000",
            DefaultBeneficiary = true,
            BankAddress = "1 Courthouse Square",
            BankName = "Emmet Bank",
            BankAccountType = "checking",
            BeneficiaryEntityType = "company",
            BeneficiaryCompanyName = "Back to the Future",
            BeneficiaryFirstName = "Martin",
            BeneficiaryLastName = "McFly",
            BeneficiaryCity = "Hill Valley",
            BeneficiaryPostcode = "91331",
            BeneficiaryStateOrProvince = "CA",
            BeneficiaryDateOfBirth = new DateTime(1968, 6, 9),
            BeneficiaryIdentificationType = "none"
        };
    }
}