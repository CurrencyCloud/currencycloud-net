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

        /// <summary>
        /// ID of the contact 
        /// </summary>
        public string Id { get; set; }

        ///<summary>
        /// Login ID of the contact which must be unique. (If none is specified the contact's email address is used)
        ///</summary>
        [Param]
        public string LoginId { get; set; }

        ///<summary>
        /// Your unique ID
        ///</summary>
        [Param]
        public string YourReference { get; set; }

        ///<summary>
        /// First name
        ///</summary>
        [Param]
        public string FirstName { get; set; }

        ///<summary>
        /// Last name
        ///</summary>
        [Param]
        public string LastName { get; set; }

        ///<summary>
        /// ID of the account
        ///</summary>
        [Param]
        public string AccountId { get; set; }

        /// <summary>
        /// Account name  
        /// </summary>
        public string AccountName { get; set; }

        ///<summary>
        /// Status (only updateable by a broker)
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// Phone number
        ///</summary>
        [Param]
        public string PhoneNumber { get; set; }

        ///<summary>
        /// Mobile phone number
        ///</summary>
        [Param]
        public string MobilePhoneNumber { get; set; }

        ///<summary>
        /// The locale of the user as combination of an ISO 639-1 language code and ISO_3166-1 regional code e.g. en, en-US, fr-FR. This determines the language of TCC Direct and the notification emails if applicable
        ///</summary>
        [Param]
        public string Locale { get; set; }

        ///<summary>
        /// The timezone of the user as it exists in the IANA tz database e.g. Europe/London, America/New_York (reference http://en.wikipedia.org/wiki/List_of_tz_database_time_zones#List)
        ///</summary>
        [Param]
        public string Timezone { get; set; }

        ///<summary>
        /// Email address
        ///</summary>
        [Param]
        public string EmailAddress { get; set; }

        ///<summary>
        /// Date of birth
        ///</summary>
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
