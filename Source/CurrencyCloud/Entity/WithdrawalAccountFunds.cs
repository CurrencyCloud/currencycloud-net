using Newtonsoft.Json;
using System;

namespace CurrencyCloud.Entity
{
    public class WithdrawalAccountFunds: Entity
    {
        [JsonConstructor]
        public WithdrawalAccountFunds() { }
            
        /// <summary>
        /// Id of the Withdrawal Account Funding pull
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Id of the Withdrawal Account
        /// </summary>
        public string WithdrawalAccountId { get; set; }
        
        /// <summary>
        /// Reference for Withdrawal Account Funding pull
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Amount pulled
        /// </summary>
        public decimal? Amount { get; set; }
        
        /// <summary>
        /// Datetime funds pulled
        /// </summary>
        public DateTime? CreatedAt { get; set; }
        
        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    WithdrawalAccountId,
                    Reference,
                    Amount,
                    CreatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WithdrawalAccountFunds))
            {
                return false;
            }

            var withdrawalAccountFunds = obj as WithdrawalAccountFunds;

            return Id == withdrawalAccountFunds.Id &&
                   WithdrawalAccountId == withdrawalAccountFunds.WithdrawalAccountId &&
                   Reference == withdrawalAccountFunds.Reference &&
                   Amount == withdrawalAccountFunds.Amount &&
                   CreatedAt == withdrawalAccountFunds.CreatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

