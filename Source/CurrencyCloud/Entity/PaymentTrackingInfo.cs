using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentTrackingInfo : Entity
    {
        [JsonConstructor]
        public PaymentTrackingInfo() { }

        public string Uetr { get; set; }

        public DateTime? InitiationTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public TransactionStatusDef TransactionStatus { get; set; }

        public List<PaymentEvent> PaymentEvents { get; set; }
        
        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Uetr,
                    InitiationTime,
                    CompletionTime,
                    LastUpdateTime,
                    TransactionStatus,
                    PaymentEvents
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentTrackingInfo))
            {
                return false;
            }

            var paymentTrackingInfo = (PaymentTrackingInfo) obj;

            return Uetr == paymentTrackingInfo.Uetr &&
                   InitiationTime == paymentTrackingInfo.InitiationTime &&
                   CompletionTime == paymentTrackingInfo.CompletionTime &&
                   LastUpdateTime == paymentTrackingInfo.LastUpdateTime &&
                   Equals(TransactionStatus, paymentTrackingInfo.TransactionStatus) &&
                   (PaymentEvents == paymentTrackingInfo.PaymentEvents ||
                    (PaymentEvents != null && paymentTrackingInfo.PaymentEvents != null &&
                     PaymentEvents.SequenceEqual(paymentTrackingInfo.PaymentEvents)));
        }

        public override int GetHashCode()
        {
            return Uetr.GetHashCode();
        }

        public class TransactionStatusDef
        {
            [JsonConstructor]
            public TransactionStatusDef()
            {
            }

            public string Status { get; set; }
            public string Reason { get; set; }
            
            public string ToJSON()
            {
                var obj = new[]
                {
                    new
                    {
                        Status,
                        Reason
                    }
                };
                return JsonConvert.SerializeObject(obj);
            }
            
            public override bool Equals(object obj)
            {
                if (!(obj is TransactionStatusDef))
                {
                    return false;
                }

                var transactionStatus2 = (TransactionStatusDef) obj;

                return Status == transactionStatus2.Status &&
                       Reason == transactionStatus2.Reason ;
            }
        }

        public class PaymentEvent
        {
            [JsonConstructor]
            public PaymentEvent()
            {
            }
        
            public string TrackerEventType { get; set; }
            public bool Valid { get; set; }
            public TransactionStatusDef TransactionStatus { get; set; }
            public DateTime? FundsAvailable { get; set; }
            public string ForwardedToAgent { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Originator { get; set; }
            public SerialPartiesDef SerialParties { get; set; }
            public DateTime? SenderAcknowledgementReceipt { get; set; }
            public AmountDef InstructedAmount { get; set; }
            public AmountDef ConfirmedAmount { get; set; }
            public AmountDef InterbankSettlementAmount { get; set; }
            public DateTime? InterbankSettlementDate { get; set; }
            public AmountDef ChargeAmount { get; set; }
            public string ChargeType { get; set; }
            public ForeignExchangeDetailsDef ForeignExchangeDetails { get; set; }
            public DateTime? LastUpdateTime { get; set; }
            
            public string ToJSON()
            {
                var obj = new[]
                {
                    new
                    {
                        TrackerEventType,
                        Valid,
                        TransactionStatus,
                        FundsAvailable,
                        ForwardedToAgent,
                        From,
                        To,
                        Originator,
                        SerialParties,
                        SenderAcknowledgementReceipt,
                        InstructedAmount,
                        ConfirmedAmount,
                        InterbankSettlementAmount,
                        InterbankSettlementDate,
                        ChargeAmount,
                        ChargeType,
                        ForeignExchangeDetails,
                        LastUpdateTime
                    }
                };
                return JsonConvert.SerializeObject(obj);
            }
            
            public override bool Equals(object obj)
            {
                if (!(obj is PaymentEvent))
                {
                    return false;
                }

                var paymentEvent = (PaymentEvent) obj;

                return TrackerEventType == paymentEvent.TrackerEventType &&
                       Valid == paymentEvent.Valid &&
                       Equals(TransactionStatus, paymentEvent.TransactionStatus) &&
                       FundsAvailable == paymentEvent.FundsAvailable &&
                       ForwardedToAgent == paymentEvent.ForwardedToAgent &&
                       From == paymentEvent.From &&
                       To == paymentEvent.To &&
                       Originator == paymentEvent.Originator &&
                       Equals(SerialParties, paymentEvent.SerialParties) &&
                       SenderAcknowledgementReceipt == paymentEvent.SenderAcknowledgementReceipt &&
                       Equals(InstructedAmount, paymentEvent.InstructedAmount) &&
                       Equals(ConfirmedAmount, paymentEvent.ConfirmedAmount) &&
                       Equals(InterbankSettlementAmount, paymentEvent.InterbankSettlementAmount) &&
                       InterbankSettlementDate == paymentEvent.InterbankSettlementDate &&
                       Equals(ChargeAmount, paymentEvent.ChargeAmount) &&
                       ChargeType == paymentEvent.ChargeType &&
                       Equals(ForeignExchangeDetails, paymentEvent.ForeignExchangeDetails) &&
                       LastUpdateTime == paymentEvent.LastUpdateTime;
            }
        }

        public class SerialPartiesDef
        {
            [JsonConstructor]
            public SerialPartiesDef()
            {
            }
            
            public string Debtor { get; set; }
            public string DebtorAgent { get; set; }
            public string IntermediaryAgent1 { get; set; }
            public string InstructingReimbursementAgent { get; set; }
            public string CreditorAgent { get; set; }
            public string Creditor { get; set; }
            
            public string ToJSON()
            {
                var obj = new[]
                {
                    new
                    {
                        Debtor,
                        DebtorAgent,
                        IntermediaryAgent1,
                        InstructingReimbursementAgent,
                        CreditorAgent,
                        Creditor
                    }
                };
                return JsonConvert.SerializeObject(obj);
            }
            
            public override bool Equals(object obj)
            {
                if (!(obj is SerialPartiesDef))
                {
                    return false;
                }

                var serialParties = (SerialPartiesDef) obj;

                return Debtor == serialParties.Debtor &&
                       DebtorAgent == serialParties.DebtorAgent &&
                       IntermediaryAgent1 == serialParties.IntermediaryAgent1 &&
                       InstructingReimbursementAgent == serialParties.InstructingReimbursementAgent &&
                       CreditorAgent == serialParties.CreditorAgent &&
                       Creditor == serialParties.Creditor;
            }
        }
        
        public class AmountDef
        {
            [JsonConstructor]
            public AmountDef()
            {
            }
            
            public string Currency { get; set; }
            public decimal? Amount { get; set; }
            
            public string ToJSON()
            {
                var obj = new[]
                {
                    new
                    {
                        Currency,
                        Amount
                    }
                };
                return JsonConvert.SerializeObject(obj);
            }
            
            public override bool Equals(object obj)
            {
                if (!(obj is AmountDef))
                {
                    return false;
                }

                var amount = (AmountDef) obj;

                return Currency == amount.Currency &&
                       Amount == amount.Amount;
            }
        }
        
        public class ForeignExchangeDetailsDef
        {
            [JsonConstructor]
            public ForeignExchangeDetailsDef()
            {
            }
            
            public string SourceCurrency { get; set; }
            public string TargetCurrency { get; set; }
            public decimal? Rate { get; set; }
            
            public string ToJSON()
            {
                var obj = new[]
                {
                    new
                    {
                        SourceCurrency,
                        TargetCurrency,
                        Rate
                    }
                };
                return JsonConvert.SerializeObject(obj);
            }
            
            public override bool Equals(object obj)
            {
                if (!(obj is ForeignExchangeDetailsDef))
                {
                    return false;
                }

                var amount = (ForeignExchangeDetailsDef) obj;

                return SourceCurrency == amount.SourceCurrency &&
                       TargetCurrency == amount.TargetCurrency &&
                       Rate == amount.Rate;
            }
        }
    }
}