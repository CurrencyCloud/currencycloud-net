using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public sealed class ReportParameters
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string Description { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string Currency { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? AmountFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? AmountTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string Status { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? PaymentDateFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? PaymentDateTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? TransferredAtFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? TransferredAtTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? CreatedAtFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? CreatedAtTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? UpdatedAtFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? UpdatedAtTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string BeneficiaryId { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string ConversionId { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public bool? WithDeleted { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string PaymentGroupId { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string UniqueRequestId { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string Scope { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string BuyCurrency { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string SellCurrency { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? ClientBuyAmountFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? ClientBuyAmountTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? ClientSellAmountFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? ClientSellAmountTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? PartnerBuyAmountFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? PartnerBuyAmountTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? PartnerSellAmountFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public decimal? PartnerSellAmountTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public string ClientStatus { get; set; }
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? ConversionDateFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? ConversionDateTo { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? SettlementDateFrom { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [Param]
            public DateTime? SettlementDateTo { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is ReportParameters))
                {
                    return false;
                }
                var reportParameters = obj as ReportParameters;
                return Description == reportParameters.Description &&
                       Currency == reportParameters.Currency &&
                       AmountFrom == reportParameters.AmountFrom &&
                       AmountTo == reportParameters.AmountTo &&
                       Status == reportParameters.Status &&
                       PaymentDateFrom == reportParameters.PaymentDateFrom &&
                       PaymentDateTo == reportParameters.PaymentDateTo &&
                       TransferredAtFrom == reportParameters.TransferredAtFrom &&
                       TransferredAtTo == reportParameters.TransferredAtTo &&
                       CreatedAtFrom == reportParameters.CreatedAtFrom &&
                       CreatedAtTo == reportParameters.CreatedAtTo &&
                       UpdatedAtFrom == reportParameters.UpdatedAtFrom &&
                       UpdatedAtTo == reportParameters.UpdatedAtTo &&
                       BeneficiaryId == reportParameters.BeneficiaryId &&
                       ConversionId == reportParameters.ConversionId &&
                       WithDeleted == reportParameters.WithDeleted &&
                       PaymentGroupId == reportParameters.PaymentGroupId &&
                       UniqueRequestId == reportParameters.UniqueRequestId &&
                       Scope == reportParameters.Scope &&
                       BuyCurrency == reportParameters.BuyCurrency &&
                       SellCurrency == reportParameters.SellCurrency &&
                       ClientBuyAmountFrom == reportParameters.ClientBuyAmountFrom &&
                       ClientBuyAmountTo == reportParameters.ClientBuyAmountTo &&
                       ClientSellAmountFrom == reportParameters.ClientSellAmountFrom &&
                       ClientSellAmountTo == reportParameters.ClientSellAmountTo &&
                       PartnerBuyAmountFrom == reportParameters.PartnerBuyAmountFrom &&
                       PartnerBuyAmountTo == reportParameters.PartnerBuyAmountTo &&
                       PartnerSellAmountFrom == reportParameters.PartnerSellAmountFrom &&
                       PartnerSellAmountTo == reportParameters.PartnerSellAmountTo &&
                       ClientStatus == reportParameters.ClientStatus &&
                       ConversionDateFrom == reportParameters.ConversionDateFrom &&
                       ConversionDateTo == reportParameters.ConversionDateTo &&
                       SettlementDateFrom == reportParameters.SettlementDateFrom &&
                       SettlementDateTo == reportParameters.SettlementDateTo;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (Description != null ? Description.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (Currency != null ? Currency.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ AmountFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ AmountTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ (Status != null ? Status.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ PaymentDateFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ PaymentDateTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ TransferredAtFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ TransferredAtTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ CreatedAtFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ CreatedAtTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ UpdatedAtFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ UpdatedAtTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ (BeneficiaryId != null ? BeneficiaryId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (ConversionId != null ? ConversionId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ WithDeleted.GetHashCode();
                    hashCode = (hashCode * 397) ^ (PaymentGroupId != null ? PaymentGroupId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (UniqueRequestId != null ? UniqueRequestId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (Scope != null ? Scope.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (BuyCurrency != null ? BuyCurrency.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (SellCurrency != null ? SellCurrency.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ ClientBuyAmountFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ ClientBuyAmountTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ ClientSellAmountFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ ClientSellAmountTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ PartnerBuyAmountFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ PartnerBuyAmountTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ PartnerSellAmountFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ PartnerSellAmountTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ (ClientStatus != null ? ClientStatus.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ ConversionDateFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ ConversionDateTo.GetHashCode();
                    hashCode = (hashCode * 397) ^ SettlementDateFrom.GetHashCode();
                    hashCode = (hashCode * 397) ^ SettlementDateTo.GetHashCode();
                    return hashCode;
                }
            }
        }
}