using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class TransactionFindParameters : FindParameters
    {

        [Param]
        public string Currency { get; set; }

        [Param]
        public decimal? Amount { get; set; }

        [Param]
        public decimal? AmountFrom { get; set; }

        [Param]
        public decimal? AmountTo { get; set; }

        [Param]
        public string Action { get; set; }

        [Param]
        public string RelacedEntityType { get; set; }

        [Param]
        public string RelatedEntityId { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string Type { get; set; }

        [Param]
        public string Reason { get; set; }

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
        public string BeneficiaryId { get; set; }

        [Param]
        public string CurrencyPair { get; set; }
    }
}
