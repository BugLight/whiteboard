using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;

namespace Whiteboard
{
    public class PointJsonConverter : JsonConverter<Point>
    {
        public override Point ReadJson(JsonReader reader, Type objectType, Point existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            if (!obj.ContainsKey("x"))
                throw new JsonReaderException();
            if (!obj.ContainsKey("y"))
                throw new JsonReaderException();
            return new Point(obj.Value<int>("x"), obj.Value<int>("y"));
        }

        public override void WriteJson(JsonWriter writer, Point value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.X);
            writer.WritePropertyName("y");
            writer.WriteValue(value.Y);
            writer.WriteEndObject();
        }
    }
}
