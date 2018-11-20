namespace CurrencyCloud.Entity
{
    public class IbanFindParameters : FindParameters
    {
        /// <summary>
        /// Currency in which the balance is held. Three-digit currency code.
        /// </summary>
        [Param]
        public string Currency { get; set; }

        /// <summary>
        /// Scope allows the logged in account to search all IBANs at all account levels. Defaults to all.
        /// own: search for IBANs on the main account
        /// clients: search for IBANs of account sub accounts
        /// all: search for IBANs of account and sub-accounts
        /// </summary>
        [Param]
        public string Scope { get; set; }

        /// <summary>
        /// UUID of a specific account you want to see the IBAN for
        /// </summary>
        [Param]
        public string AccountId { get; set; }
    }
}