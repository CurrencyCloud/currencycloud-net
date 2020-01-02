using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class MarginBalanceTopUp : Entity
    {
        [JsonConstructor]
        public MarginBalanceTopUp() { }
            
        /// <summary>
        /// Account identifier
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Amount Transferred
        /// </summary>
        public decimal TransferredAmount { get; set; }

        /// <summary>
        /// DateTime which the transfer was completed
        /// </summary>
        public DateTime TransferCompletedAt { get; set; }
        
        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    AccountId,
                    Currency,
                    TransferredAmount,
                    TransferCompletedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MarginBalanceTopUp))
            {
                return false;
            }

            var topUp = obj as MarginBalanceTopUp;

            return AccountId == topUp.AccountId &&
                   Currency == topUp.Currency &&
                   TransferredAmount == topUp.TransferredAmount &&
                   TransferCompletedAt == topUp.TransferCompletedAt;
        }

        public override int GetHashCode()
        {
            return AccountId.GetHashCode();
        }
    }
}