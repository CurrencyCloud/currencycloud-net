using Newtonsoft.Json;
using System;

namespace CurrencyCloud.Entity
{
    public class CollectionsScreeningResult: Entity
    {
        [JsonConstructor]
        public CollectionsScreeningResult() { }

        /// <summary>
        /// UUID of the Withdrawal Account Funding pull
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// UUID of the Account
        /// </summary>
        public string Reason { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Accepted,
                    Reason
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CollectionsScreeningResult))
            {
                return false;
            }

            var collectionsScreeningResult = obj as CollectionsScreeningResult;

            return Accepted == collectionsScreeningResult.Accepted &&
                   Reason == collectionsScreeningResult.Reason;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

