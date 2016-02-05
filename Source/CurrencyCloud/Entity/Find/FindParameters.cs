using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class FindParameters
    {
        [Param]
        public int? Page { get; set; }
        [Param]
        public int? PerPage { get; set; }
        [Param]
        public string Order { get; set; }
        [Param]
        public enum OrderDirection { Asc, Desc };
        [Param]
        public OrderDirection? OrderAscDesc { get; set; }
    }
}
