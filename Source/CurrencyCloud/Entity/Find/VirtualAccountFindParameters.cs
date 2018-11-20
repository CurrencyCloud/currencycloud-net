namespace CurrencyCloud.Entity
{
    public class VirtualAccountFindParameters : FindParameters
    {
        /// <summary>
        /// Scope allows the logged in account to search all VANs at all account levels. Defaults to all.
        /// own: search for VANs on the main account
        /// clients: search for VANs of account sub accounts
        /// all: search for VANs of account and sub-accounts
        /// </summary>
        [Param]
        public string Scope { get; set; }

        /// <summary>
        /// UUID of a specific account you want to see the VAN for
        /// </summary>
        [Param]
        public string AccountId { get; set; }
    }
}