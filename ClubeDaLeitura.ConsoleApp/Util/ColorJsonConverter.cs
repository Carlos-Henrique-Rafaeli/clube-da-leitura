using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClubeDaLeitura.ConsoleApp.Util;

public class ColorJsonConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;
            int a = root.GetProperty("A").GetInt32();
            int r = root.GetProperty("R").GetInt32();
            int g = root.GetProperty("G").GetInt32();
            int b = root.GetProperty("B").GetInt32();
            return Color.FromArgb(a, r, g, b);
        }
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("A", value.A);
        writer.WriteNumber("R", value.R);
        writer.WriteNumber("G", value.G);
        writer.WriteNumber("B", value.B);
        writer.WriteEndObject();
    }
}
