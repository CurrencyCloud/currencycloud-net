using System;

namespace CurrencyCloud.Entity
{
    public class BalanceFindParameters : FindParameters
    {
        ///<summary>
        /// Amount of balances to 2dp
        ///</summary>
        [Param]
        public decimal? AmountFrom { get; set; }

        ///<summary>
        /// Amount of balances to 2dp
        ///</summary>
        [Param]
        public decimal? AmountTo { get; set; }

        ///<summary>
        /// ISO 8601 Date Time to view balances at a specific point in time
        ///</summary>
        [Param]
        public DateTime? AsAtDate { get; set; }

        ///<summary>
        /// Controls the search of balances at all account levels. Defaults to own.
        /// own: allows to search balances on the main account
        /// clients: allows to search balances of account sub accounts
        /// all: allows to search balances across account and sub-accounts
        ///</summary>
        [Param]
        public string Scope { get; set; }
    }
}
