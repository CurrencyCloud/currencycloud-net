using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class SettlementFindParameters : FindParameters
    {

        [Param]
        public string ShortReference { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        [Param]
        public DateTime? CreatedAtTo { get; set; }

        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        [Param]
        public DateTime? UpdatedAtTo { get; set; }

        [Param]
        public DateTime? ReleasedAtFrom { get; set; }

        [Param]
        public DateTime? ReleasedAtTo { get; set; }
    }
}
