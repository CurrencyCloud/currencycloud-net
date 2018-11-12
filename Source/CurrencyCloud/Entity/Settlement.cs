using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Settlement : Entity
    {
        public Settlement() { }

        ///<summary>
        /// Settlement ID
        ///</summary>
        public string Id { get; set; }

        public string ShortReference { get; set; }

        public string Status { get; set; }

        public List<string> ConversionIds { get; set; }

        public List<Dictionary<string, SettlementEntry>> Entries { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? ReleasedAt { get; set; }

        [Param]
        public string Type { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    Status,
                    ShortReference,
                    Type,
                    ConversionIds,
                    Entries,
                    CreatedAt,
                    UpdatedAt,
                    ReleasedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Settlement))
            {
                return false;
            }

            var settlement = obj as Settlement;

            return Id == settlement.Id &&
                   ShortReference == settlement.ShortReference &&
                   Status == settlement.Status &&
                   ConversionIds.SequenceEqual(settlement.ConversionIds) &&
                   Entries.SequenceEqual(settlement.Entries) &&
                   CreatedAt == settlement.CreatedAt &&
                   UpdatedAt == settlement.UpdatedAt &&
                   ReleasedAt == settlement.ReleasedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public sealed class SettlementEntry
        {
            internal SettlementEntry() { }

            public decimal SendAmount { get; set; }

            public decimal ReceiveAmount { get; set; }
        }
    }
}
