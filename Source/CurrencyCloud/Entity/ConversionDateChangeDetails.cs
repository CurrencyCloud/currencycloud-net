using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionDateChangeDetails : Entity
    {
        [JsonConstructor]
        public ConversionDateChangeDetails() { }

        public DateTime? InitialValueDate { get; set; }

        public DateTime? CurrentValueDate { get; set; }

        public DateTime? InitialDeliveryDate { get; set; }

        public DateTime? CurrentDeliveryDate { get; set; }

        public decimal? TotalProfitAndLoss { get; set; }

        public string FloatingCurrency { get; set; }

        public List<DateChange> Changes { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    InitialValueDate,
                    CurrentValueDate,
                    InitialDeliveryDate,
                    CurrentDeliveryDate,
                    TotalProfitAndLoss,
                    FloatingCurrency,
                    Changes
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConversionDateChangeDetails))
            {
                return false;
            }

            var conversionDateChangeDetails = obj as ConversionDateChangeDetails;

            return InitialValueDate == conversionDateChangeDetails.InitialValueDate &&
                   CurrentValueDate == conversionDateChangeDetails.CurrentValueDate &&
                   InitialDeliveryDate == conversionDateChangeDetails.InitialDeliveryDate &&
                   CurrentDeliveryDate == conversionDateChangeDetails.CurrentDeliveryDate &&
                   TotalProfitAndLoss == conversionDateChangeDetails.TotalProfitAndLoss &&
                   FloatingCurrency == conversionDateChangeDetails.FloatingCurrency &&
                   Changes == conversionDateChangeDetails.Changes;
        }

        public override int GetHashCode()
        {
            return InitialValueDate.GetHashCode() ^
                CurrentValueDate.GetHashCode() ^
                InitialDeliveryDate.GetHashCode() ^
                CurrentDeliveryDate.GetHashCode() ^
                TotalProfitAndLoss.GetHashCode() ^
                Changes.GetHashCode();
        }

        public class DateChange
        {
            [JsonConstructor]
            public DateChange() {}

            public DateTime? RequestedValueDate { get; set; }

            public DateTime? NewValueDate { get; set; }

            public DateTime? NewDeliveryDate { get; set; }

            public decimal? ProfitAndLoss { get; set; }

            public decimal? AdminFee { get; set; }

            public string Type { get; set; }

            public string Status { get; set; }
        }

    }
}