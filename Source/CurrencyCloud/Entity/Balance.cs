using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Balance : Entity
    {
        public Balance() { }

        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    AccountId,
                    Currency,
                    Amount,
                    CreatedAt,
                    UpdatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Balance))
            {
                return false;
            }

            var balance = obj as Balance;

            return Id == balance.Id &&
                   AccountId == balance.AccountId &&
                   Currency == balance.Currency &&
                   Amount == balance.Amount &&
                   CreatedAt == balance.CreatedAt &&
                   UpdatedAt == balance.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
