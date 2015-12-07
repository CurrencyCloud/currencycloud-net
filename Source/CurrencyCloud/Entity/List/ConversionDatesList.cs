using System;
using System.Collections.Generic;

namespace CurrencyCloud.Entity.List
{
    public class ConversionDatesList
    {
        internal ConversionDatesList() { }

        public Dictionary<DateTime, string> InvalidConversionDates { get; set; }

        public DateTime FirstConversionDate { get; set; }

        public DateTime DefaultConversionDate { get; set; }
    }
}
