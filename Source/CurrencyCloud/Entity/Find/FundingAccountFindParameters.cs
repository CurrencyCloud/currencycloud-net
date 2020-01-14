namespace CurrencyCloud.Entity
{
    public class FundingAccountFindParameters : FindParameters
    {
        ///<summary>
        /// ID of the Account this SSI is attached to
        ///</summary>
        [Param]
        public string AccountId { get; set; }

        ///<summary>
        /// The currency that should be sent to these account details
        ///</summary>
        [Param]
        public string Currency { get; set; }

        ///<summary>
        /// The type of payment that can be made into the account details provided
        ///</summary>
        [Param]
        public string PaymentType { get; set; }
        
    }
}