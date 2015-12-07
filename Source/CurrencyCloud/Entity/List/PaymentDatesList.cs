using System;
using System.Collections.Generic;

namespace CurrencyCloud.Entity.List
{
    public class PaymentDatesList
    {
        internal PaymentDatesList() { }

        public Dictionary<DateTime, string> InvalidPaymentDates { get; set; }

        public DateTime FirstPaymentDate { get; set; }
    }
}
