using System;

namespace CurrencyCloud.Entity
{
    public class TransferFindParameters : FindParameters
    {
        [Param]
        public string ShortReference { get; set; }

        [Param]
        public string SourceAccountId { get; set; }

        [Param]
        public string DestinationAccountId { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string Currency { get; set; }

        [Param]
        public decimal? AmountFrom { get; set; }

        [Param]
        public decimal? AmountTo { get; set; }

        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        [Param]
        public DateTime? CreatedAtTo { get; set; }

        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        [Param]
        public DateTime? UpdatedAtTo { get; set; }

        [Param]
        public DateTime? CompletedAtFrom { get; set; }

        [Param]
        public DateTime? CompletedAtTo { get; set; }

        [Param]
        public string CreatorAccountId { get; set; }

        [Param]
        public string CreatorContactId { get; set; }
    }
}