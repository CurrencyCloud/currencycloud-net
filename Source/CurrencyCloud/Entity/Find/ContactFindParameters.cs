namespace CurrencyCloud.Entity
{
    public class ContactFindParameters : FindParameters
    {
        ///<summary>
        /// Account name
        ///</summary>
        [Param]
        public string AccountName { get; set; }

        ///<summary>
        /// ID of account
        ///</summary>
        [Param]
        public string AccountId { get; set; }

        ///<summary>
        /// Forename
        ///</summary>
        [Param]
        public string FirstName { get; set; }

        ///<summary>
        /// Surname
        ///</summary>
        [Param]
        public string LastName { get; set; }

        ///<summary>
        /// Email address
        ///</summary>
        [Param]
        public string EmailAddress { get; set; }

        ///<summary>
        /// Your unique ID
        ///</summary>
        [Param]
        public string YourReference { get; set; }

        ///<summary>
        /// Phone number
        ///</summary>
        [Param]
        public string PhoneNumber { get; set; }

        ///<summary>
        /// Login ID of the contact which must be unique
        ///</summary>
        [Param]
        public string LoginId { get; set; }

        ///<summary>
        /// Status (only updateable by a broker)
        ///</summary>
        [Param]
        public string Status { get; set; }

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
    }
}
