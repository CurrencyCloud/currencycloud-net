using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.List
{
    [DataContract]
    public class ConversionDatesList
    {
        [DataMember(Name = "invalid_conversion_dates")]
        public Dictionary<DateTime, string> InvalidConversionDates { get; internal set; }

        [DataMember(Name = "first_conversion_date")]
        public DateTime FirstConversionDate { get; internal set; }

        [DataMember(Name = "default_conversion_date")]
        public DateTime DefaultConversionDate { get; internal set; }

        internal ConversionDatesList() { }
    }
}
