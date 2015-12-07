using System;
using System.Collections.Generic;

namespace CurrencyCloud.Entity
{
    public class Settlement : Entity
    {
        internal Settlement() { }

        public string Id { get; set; }

        public string ShortReference { get; set; }

        public string Status { get; set; }

        public List<string> ConversionIds { get; set; }

        public Dictionary<string, SettlementEntry> Entries { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime ReleasedAt { get; set; }
    }
}
