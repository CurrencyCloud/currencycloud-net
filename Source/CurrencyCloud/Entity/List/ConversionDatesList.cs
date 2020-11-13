using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class ConversionDatesList
    {
        internal ConversionDatesList() { }

        public Dictionary<DateTime, string> InvalidConversionDates { get; set; }

        public DateTime FirstConversionDate { get; set; }

        public DateTime DefaultConversionDate { get; set; }
        
        public DateTime OptimizeLiquidityConversionDate { get; set; }

        public DateTime FirstConversionCutoffDatetime { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    InvalidConversionDates,
                    FirstConversionDate,
                    DefaultConversionDate,
                    OptimizeLiquidityConversionDate,
                    FirstConversionCutoffDatetime
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
