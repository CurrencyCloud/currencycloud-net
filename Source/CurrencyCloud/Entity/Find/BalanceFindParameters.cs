using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
