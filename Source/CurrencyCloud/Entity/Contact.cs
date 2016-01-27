using System;

namespace CurrencyCloud.Entity
{
    public class Contact : Entity
    {
        public Contact(
            string accountId,
            string firstName,
            string lastName,
            string emailAddress,
            string phoneNumber
            )
        {
            this.AccountId = accountId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.PhoneNumber = phoneNumber;
        }

        [Newtonsoft.Json.JsonConstructor]
        internal Contact() { }

        public string Id { get; set; }

        [Param]
        public string LoginId { get; set; }

        [Param]
        public string YourReference { get; set; }

        [Param]
        public string FirstName { get; set; }

        [Param]
        public string LastName { get; set; }

        [Param]
        public string AccountId { get; set; }

        public string AccountName { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string PhoneNumber { get; set; }

        [Param]
        public string MobilePhoneNumber { get; set; }

        [Param]
        public string Locale { get; set; }

        [Param]
        public string Timezone { get; set; }

        [Param]
        public string EmailAddress { get; set; }

        [Param]
        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Contact))
            {
                return false;
            }

            var contact = obj as Contact;

            return Id == contact.Id &&
                   LoginId == contact.LoginId &&
                   YourReference == contact.YourReference &&
                   FirstName == contact.FirstName &&
                   LastName == contact.LastName &&
                   AccountId == contact.AccountId &&
                   AccountName == contact.AccountName &&
                   Status == contact.Status &&
                   PhoneNumber == contact.PhoneNumber &&
                   MobilePhoneNumber == contact.MobilePhoneNumber &&
                   Locale == contact.Locale &&
                   Timezone == contact.Timezone &&
                   EmailAddress == contact.EmailAddress &&
                   DateOfBirth == contact.DateOfBirth &&
                   CreatedAt == contact.CreatedAt &&
                   UpdatedAt == contact.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
