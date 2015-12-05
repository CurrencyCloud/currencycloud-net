using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.List
{
    [DataContract]
    public class PaymentDatesList
    {
        [DataMember(Name = "invalid_payment_dates")]
        public Dictionary<DateTime, string> InvalidPaymentDates { get; internal set; }

        [DataMember(Name = "first_payment_date")]
        public DateTime FirstPaymentDate { get; internal set; }

        internal PaymentDatesList() { }
    }
}
