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
