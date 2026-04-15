using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class CollectionsScreeningResult : Entity
    {
        [JsonConstructor]
        public CollectionsScreeningResult() { }

        /// <summary>
        /// The transaction UUID
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Account UUID
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// House Account UUID
        /// </summary>
        public string HouseAccountId { get; set; }

        /// <summary>
        /// The screening result
        /// </summary>
        public ScreeningResult Result { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    TransactionId,
                    AccountId,
                    HouseAccountId,
                    Result
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

            var result = obj as CollectionsScreeningResult;

            return TransactionId == result.TransactionId &&
                   AccountId == result.AccountId &&
                   HouseAccountId == result.HouseAccountId &&
                   Equals(Result, result.Result);
        }

        public override int GetHashCode()
        {
            return TransactionId?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// The screening result details
        /// </summary>
        public class ScreeningResult
        {
            [JsonConstructor]
            public ScreeningResult() { }

            /// <summary>
            /// The reason why the transaction was accepted or rejected
            /// </summary>
            public string Reason { get; set; }

            /// <summary>
            /// Was the transaction accepted? True or false
            /// </summary>
            public bool Accepted { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is ScreeningResult))
                {
                    return false;
                }

                var result = obj as ScreeningResult;

                return Reason == result.Reason &&
                       Accepted == result.Accepted;
            }

            public override int GetHashCode()
            {
                return Reason?.GetHashCode() ?? 0;
            }
        }
    }
}
