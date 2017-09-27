namespace PokerFace.Core.Extensions
{
    using Newtonsoft.Json.Linq;

    public static class ObjectExtensions
    {
        public static JObject ToJObject<T>(this T source)
        {
            return JObject.FromObject(source);
        }
    }
}