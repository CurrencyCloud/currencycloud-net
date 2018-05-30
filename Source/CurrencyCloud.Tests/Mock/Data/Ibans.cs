namespace CurrencyCloud.Tests.Mock.Data
{
    static class Ibans
    {
        public static readonly Entity.Iban Iban1 = new Entity.Iban
        {
            Id = "e12e108b-52e2-4f02-9477-048638af2bce",
            IbanCode = "GB87TCCL00997918152118",
            AccountId = "dead2c60-ab3c-48be-96d6-cf87b8ebddf2",
            Currency = "EUR",
            AccountHolderName = "Charlie Brown",
            BankInstitutionName = "The Currency Cloud",
            BankInstitutionAddress = "12 Steward Street, The Steward Building, London, E1 6FQ, GB",
            BankInstitutionCountry = "United Kingdom",
            BicSwift = "TCCLGB31"
        };
    }
}