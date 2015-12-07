using System;

namespace CurrencyCloud.Entity
{
    public class Contact : Entity
    {
        internal Contact() { }

        public string Id { get; set; }

        public string LoginId { get; set; }

        public string YourReference { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AccountId { get; set; }

        public string AccountName { get; set; }

        public string Status { get; set; }

        public string PhoneNumber { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Locale { get; set; }

        public string Timezone { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
