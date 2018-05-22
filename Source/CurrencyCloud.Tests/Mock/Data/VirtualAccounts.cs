namespace CurrencyCloud.Tests.Mock.Data
{
    static class VirtualAccounts
    {
        public static readonly Entity.VirtualAccount Van1 = new Entity.VirtualAccount
        {
            Id = "00d272ee-fae5-4f97-b425-993a2d6e3a46",
            VirtualAccountNumber = "8303723297",
            AccountId = "2090939e-b2f7-3f2b-1363-4d235b3f58af",
            AccountHolderName = "Lucy van Pelt",
            BankInstitutionName = "Community Federal Savings Bank",
            BankInstitutionAddress = "Seventh Avenue, New York, NY 10019, US",
            BankInstitutionCountry = "United States",
            RoutingCode = "026073150"
        };
    }
}