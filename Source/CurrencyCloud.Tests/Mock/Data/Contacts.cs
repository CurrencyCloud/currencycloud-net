using System;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Contacts
    {
        public static readonly Entity.Contact Contact1 = new Entity.Contact(
           "To be filled later",
            "John",
            "Smith",
            "john.smith@company.com",
            "06554 87845")
        { 
                YourReference = "ACME12345",
                MobilePhoneNumber = "07564 534 54",
                LoginId = "john",
                Status = "enabled",
                Locale = "en-US",
                Timezone = "Europe/London",
                DateOfBirth = new DateTime(1980, 1, 22)
        };

        public static readonly Entity.Contact Contact2 = new Entity.Contact (
            "To be filled later",
            "Emmet",
            "Brown",
            "dr.emmet.brown@company.com",
            "073 789 1661")
        {
            YourReference = "doc",
            MobilePhoneNumber = "073 789 1661",
            LoginId = "emmet",
            Status = "enabled",
            Locale = "en-US",
            Timezone = "Europe/London",
            DateOfBirth = new DateTime(1960, 1, 29)
        };
    }
}