using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Contact
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "login_id")]
        public string LoginId { get; internal set; }

        [DataMember(Name = "your_reference")]
        public string YourReference { get; internal set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; internal set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; internal set; }

        [DataMember(Name = "account_id")]
        public string AccountId { get; internal set; }

        [DataMember(Name = "account_name")]
        public string AccountName { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; internal set; }

        [DataMember(Name = "mobile_phone_number")]
        public string MobilePhoneNumber { get; internal set; }

        [DataMember(Name = "locale")]
        public string Locale { get; internal set; }

        [DataMember(Name = "timezone")]
        public string Timezone { get; internal set; }

        [DataMember(Name = "email_address")]
        public string EmailAddress { get; internal set; }

        [DataMember(Name = "date_of_birth")]
        public DateTime DateOfBirth { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal Contact() { }
    }
}
