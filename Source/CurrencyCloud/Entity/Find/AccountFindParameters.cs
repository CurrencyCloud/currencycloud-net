using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class AccountFindParameters : FindParameters
    {
        [Param]
        public string AccountName { get; set; }

        [Param]
        public string Brand { get; set; }

        [Param]
        public string YourReference { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string Street { get; set; }

        [Param]
        public string City { get; set; }

        [Param]
        public string StateOrProvince { get; set; }

        [Param]
        public string PostalCode { get; set; }

        [Param]
        public string Country { get; set; }

        [Param]
        public string SpreadTable { get; set; }
    }
}
