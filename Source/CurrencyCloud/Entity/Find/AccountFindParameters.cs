namespace CurrencyCloud.Entity
{
    public class AccountFindParameters : FindParameters
    {

        ///<summary>
        /// Name of the account
        ///</summary>        
        [Param]
        public string AccountName { get; set; }

        ///<summary>
        /// The brand to associate with this account
        ///</summary>
        [Param]
        public string Brand { get; set; }

        ///<summary>
        /// Your unique customer ID
        ///</summary>
        [Param]
        public string YourReference { get; set; }

        ///<summary>
        /// Status of the account
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// First line of the address
        ///</summary>
        [Param]
        public string Street { get; set; }

        ///<summary>
        /// City
        ///</summary>
        [Param]
        public string City { get; set; }

        ///<summary>
        /// State/Province
        ///</summary>
        [Param]
        public string StateOrProvince { get; set; }

        ///<summary>
        /// Post Code or Zip Code
        ///</summary>
        [Param]
        public string PostalCode { get; set; }

        ///<summary>
        /// A two-letter country codes as defined in ISO 3166-1
        ///</summary>
        [Param]
        public string Country { get; set; }

        ///<summary>
        /// Name of spread table
        ///</summary>
        [Param]
        public string SpreadTable { get; set; }
    }
}
