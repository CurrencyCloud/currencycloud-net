using Newtonsoft.Json;

namespace CurrencyCloud
{
    /// <summary>
    /// Global settings controlling how API responses are deserialized.
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// Controls how <see cref="System.DateTime"/> values received from the API are treated during
        /// deserialization.
        /// <para>
        /// Defaults to <see cref="Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind"/>, preserving the SDK's
        /// historical behaviour: values carrying an explicit UTC offset (e.g. "2020-11-10T23:19:00+00:00") are
        /// converted to the local time zone of the machine (Kind = Local), and offset-less values (e.g.
        /// "2020-11-10") come back as Kind = Unspecified.
        /// </para>
        /// <para>
        /// Set to <see cref="Newtonsoft.Json.DateTimeZoneHandling.Utc"/> to have every DateTime returned as UTC
        /// (Kind = Utc), matching the values the API actually sends. Recommended when comparing returned values
        /// against <see cref="System.DateTime.UtcNow"/>, since <see cref="System.DateTime"/> comparison ignores
        /// Kind and would otherwise be skewed by the machine's UTC offset.
        /// </para>
        /// </summary>
        public static DateTimeZoneHandling DateTimeZoneHandling { get; set; }
            = Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;
    }
}
