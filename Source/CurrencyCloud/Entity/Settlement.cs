using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Settlement : Entity
    {
        internal Settlement() { }

        public string Id { get; set; }

        public string ShortReference { get; set; }

        public string Status { get; set; }

        public List<string> ConversionIds { get; set; }

        public List<Dictionary<string, SettlementEntry>> Entries { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime ReleasedAt { get; set; }

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
    }
}
