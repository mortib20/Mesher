using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mesher.Mesh.Models.Converter;

public class LowercaseEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? throw new JsonException();

        // JSON uses lowercase → convert to PascalCase
        var pascal = char.ToUpper(value[0]) + value[1..];

        return Enum.TryParse(pascal, out T result) ? result : throw new JsonException($"Invalid enum: {value}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString().ToLowerInvariant());
}