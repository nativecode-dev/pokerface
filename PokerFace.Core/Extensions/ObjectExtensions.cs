namespace PokerFace.Core.Extensions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class ObjectExtensions
    {
        public static JObject ToJObject<T>(this T source)
        {
            return JObject.FromObject(source);
        }

        public static string ToJson<T>(this T source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}