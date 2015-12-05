using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Settlement
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "short_reference")]
        public string ShortReference { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        [DataMember(Name = "conversion_ids")]
        public List<string> ConversionIds { get; internal set; }

        [DataMember(Name = "entries")]
        public Dictionary<string, SettlementEntry> Entries { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        [DataMember(Name = "released_at")]
        public DateTime ReleasedAt { get; internal set; }

        internal Settlement() { }
    }
}
