using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class FundingTransaction : Entity
    {
        [JsonConstructor]
        public FundingTransaction() { }

        /// <summary>
        /// The Related Entity UUID (related_entity_id) for the transaction
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The transaction amount
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The three-letter currency code
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The transaction rail
        /// </summary>
        public string Rail { get; set; }

        /// <summary>
        /// Any additional information
        /// </summary>
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// The routing code
        /// </summary>
        public string ReceivingAccountRoutingCode { get; set; }

        /// <summary>
        /// The value date in ISO-8601 format
        /// </summary>
        public DateTime? ValueDate { get; set; }

        /// <summary>
        /// The transaction receiving account number
        /// </summary>
        public string ReceivingAccountNumber { get; set; }

        /// <summary>
        /// The transaction receiving account IBAN
        /// </summary>
        public string ReceivingAccountIban { get; set; }

        /// <summary>
        /// The created at date in ISO-8601 format
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The last time the transaction was updated in ISO-8601 format
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The completed date in ISO-8601 format
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// The sender information
        /// </summary>
        public SenderInformation Sender { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    Amount,
                    Currency,
                    Rail,
                    AdditionalInformation,
                    ReceivingAccountRoutingCode,
                    ValueDate,
                    ReceivingAccountNumber,
                    ReceivingAccountIban,
                    CreatedAt,
                    UpdatedAt,
                    CompletedAt,
                    Sender
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FundingTransaction))
            {
                return false;
            }

            var transaction = obj as FundingTransaction;

            return Id == transaction.Id &&
                   Amount == transaction.Amount &&
                   Currency == transaction.Currency &&
                   Rail == transaction.Rail &&
                   AdditionalInformation == transaction.AdditionalInformation &&
                   ReceivingAccountRoutingCode == transaction.ReceivingAccountRoutingCode &&
                   ValueDate == transaction.ValueDate &&
                   ReceivingAccountNumber == transaction.ReceivingAccountNumber &&
                   ReceivingAccountIban == transaction.ReceivingAccountIban &&
                   CreatedAt == transaction.CreatedAt &&
                   UpdatedAt == transaction.UpdatedAt &&
                   CompletedAt == transaction.CompletedAt &&
                   Equals(Sender, transaction.Sender);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Sender information for a funding transaction
        /// </summary>
        public class SenderInformation
        {
            [JsonConstructor]
            public SenderInformation() { }

            /// <summary>
            /// The sender account number
            /// </summary>
            [JsonProperty("sender_account_number")]
            public string AccountNumber { get; set; }

            /// <summary>
            /// The unstructured sender address
            /// </summary>
            [JsonProperty("sender_address")]
            public string Address { get; set; }

            /// <summary>
            /// The sender BIC
            /// </summary>
            [JsonProperty("sender_bic")]
            public string Bic { get; set; }

            /// <summary>
            /// The two letter sender country code
            /// </summary>
            [JsonProperty("sender_country")]
            public string Country { get; set; }

            /// <summary>
            /// The sender IBAN
            /// </summary>
            [JsonProperty("sender_iban")]
            public string Iban { get; set; }

            /// <summary>
            /// The sender ID
            /// </summary>
            [JsonProperty("sender_id")]
            public string Id { get; set; }

            /// <summary>
            /// The sender name
            /// </summary>
            [JsonProperty("sender_name")]
            public string Name { get; set; }

            /// <summary>
            /// The sender routing code
            /// </summary>
            [JsonProperty("sender_routing_code")]
            public string RoutingCode { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is SenderInformation))
                {
                    return false;
                }

                var senderInfo = obj as SenderInformation;

                return AccountNumber == senderInfo.AccountNumber &&
                       Address == senderInfo.Address &&
                       Bic == senderInfo.Bic &&
                       Country == senderInfo.Country &&
                       Iban == senderInfo.Iban &&
                       Id == senderInfo.Id &&
                       Name == senderInfo.Name &&
                       RoutingCode == senderInfo.RoutingCode;
            }

            public override int GetHashCode()
            {
                return Id?.GetHashCode() ?? 0;
            }
        }
    }
}
