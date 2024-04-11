using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dwolla.Client
{
    public class DecimalFormatConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return decimal.Parse(reader.GetString() ?? string.Empty);
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value:0.00}");
        }
    }
}