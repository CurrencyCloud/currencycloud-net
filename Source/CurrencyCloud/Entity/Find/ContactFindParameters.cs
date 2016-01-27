using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class ContactFindParameters : FindParameters
    {
        [Param]
        public string AccountName { get; set; }

        [Param]
        public string AccountId { get; set; }

        [Param]
        public string FirstName { get; set; }

        [Param]
        public string LastName { get; set; }

        [Param]
        public string EmailAddress { get; set; }

        [Param]
        public string YourReference { get; set; }

        [Param]
        public string PhoneNumber { get; set; }

        [Param]
        public string LoginId { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string Locale { get; set; }

        [Param]
        public string Timezone { get; set; }
    }
}
