using System;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Contacts
    {
        public static readonly dynamic Contact1 = new
        {
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "john.smith@company.com",
            PhoneNumber = "06554 87845",

            Optional = new
            {
                YourReference = "ACME12345",
                MobilePhoneNumber = "07564 534 54",
                LoginId = "john",
                Status = "enabled",
                Locale = "en-US",
                Timezone = "Europe/London",
                DateOfBirth = new DateTime(1980, 1, 22)
            }
        };

        public static readonly dynamic Contact2 = new
        {
            FirstName = "Emmet",
            LastName = "Brown",
            EmailAddress = "dr.emmet.brown@company.com",
            PhoneNumber = "073 789 1661",
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