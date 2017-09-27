namespace PokerFace.Core.Types
{
    using System;
    using Newtonsoft.Json;

    public class EnumJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType.IsEnum && reader.Value is string)
            {
                return Enum.Parse(objectType, (string) reader.Value, true);
            }

            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(Enum.GetName(value.GetType(), value));
            }
        }
    }
}