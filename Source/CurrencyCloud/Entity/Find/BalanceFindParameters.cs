using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class BalanceFindParameters : FindParameters
    {
        [Param]
        public decimal? AmountFrom { get; set; }

        [Param]
        public decimal? AmountTo { get; set; }

        [Param]
        public DateTime? AsAtDate { get; set; }
    }
}
