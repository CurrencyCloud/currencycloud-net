using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class FundingTransactionSender : Entity
    {
        [JsonConstructor]
        public FundingTransactionSender() { }

        public string SenderId { get; set; }

        public string SenderAddress { get; set; }

        public string SenderCountry; { get; set; }

        public string SenderName; { get; set; }

        public string SenderBic; { get; set; }

        public string SenderIban; { get; set; }

        public string SenderAccountNumber; { get; set; }

        public string SenderRoutingCode; { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    SenderId,
                    SenderAddress,
                    SenderCountry,
                    SenderName,
                    SenderBic,
                    SenderIban,
                    SenderAccountNumber,
                    SenderRoutingCode
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FundingTransactionSender))
            {
                return false;
            }

            var fundingTransactionSender = obj as FundingTransactionSender;

            return SenderId == fundingTransactionSender.SenderId &&
                   SenderAddress == fundingTransactionSender.SenderAddress &&
                   SenderCountry == fundingTransactionSender.SenderCountry &&
                   SenderName == fundingTransactionSender.SenderName &&
                   SenderBic == fundingTransactionSender.SenderBic &&
                   SenderIban == fundingTransactionSender.SenderIban &&
                   SenderAccountNumber == fundingTransactionSender.SenderAccountNumber &&
                   SenderRoutingCode == fundingTransactionSender.SenderRoutingCode;
        }

        public override int GetHashCode()
        {
            return SenderId.GetHashCode();
        }
    }
}