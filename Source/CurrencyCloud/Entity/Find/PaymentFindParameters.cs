using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class PaymentFindParameters : FindParameters
    {
        [Param]
        public string ShortReference { get; set; }

        [Param]
        public string Currency { get; set; }

        [Param]
        public decimal? Amount { get; set; }

        [Param]
        public decimal? AmountFrom { get; set; }

        [Param]
        public decimal? AmountTo { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string Reason { get; set; }

        [Param]
        public DateTime? PaymentDateFrom { get; set; }

        [Param]
        public DateTime? PaymentDateTo { get; set; }

        [Param]
        public DateTime? TransferredAtFrom { get; set; }

        [Param]
        public DateTime? TransferredAtTo { get; set; }

        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        [Param]
        public DateTime? CreatedAtTo { get; set; }

        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        [Param]
        public DateTime? UpdatedAtTo { get; set; }

        [Param]
        public string BeneficiaryId { get; set; }

        [Param]
        public string ConversionId { get; set; }

    }
}
