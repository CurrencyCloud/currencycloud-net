using System;
using System.Globalization;
using Newtonsoft.Json;

namespace CurrencyCloud
{
    /// <summary>
    /// Converts API date values to and from <see cref="DateOnly"/>.
    /// <para>
    /// The API sends some dates as plain dates (e.g. "2015-10-29") and others as timestamps
    /// (e.g. "2018-01-01T00:00:00+00:00"). Timestamps carrying an offset are read as their UTC date, so a
    /// midnight-UTC value never slips to the previous day on machines west of UTC.
    /// </para>
    /// </summary>
    internal class DateOnlyConverter : JsonConverter
    {
        internal const string Format = "yyyy-MM-dd";

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateOnly) || objectType == typeof(DateOnly?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    if (objectType == typeof(DateOnly))
                        throw new JsonSerializationException("Cannot convert null value to DateOnly.");
                    return null;

                case JsonToken.Date:
                    if (reader.Value is DateTimeOffset dto)
                        return DateOnly.FromDateTime(dto.UtcDateTime);

                    var dt = (DateTime)reader.Value;
                    return DateOnly.FromDateTime(dt.Kind == DateTimeKind.Local ? dt.ToUniversalTime() : dt);

                case JsonToken.String:
                    var s = (string)reader.Value;
                    if (string.IsNullOrEmpty(s) && objectType == typeof(DateOnly?))
                        return null;
                    if (DateOnly.TryParseExact(s, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                        return date;
                    return DateOnly.FromDateTime(DateTimeOffset.Parse(s, CultureInfo.InvariantCulture).UtcDateTime);

                default:
                    throw new JsonSerializationException(
                        string.Format("Unexpected token {0} when parsing DateOnly.", reader.TokenType));
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateOnly)value).ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
