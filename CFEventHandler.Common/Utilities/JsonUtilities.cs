using System.Text.Json.Serialization;
using System.Text.Json;

namespace CFEventHandler.Utilities
{
    /// <summary>
    /// JSON utilities
    /// </summary>
    public class JSONUtilities
    {
        public static JsonSerializerOptions DefaultJsonSerializerOptions
        {
            get
            {
                var jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                jsonSerializerOptions.WriteIndented = true;
                jsonSerializerOptions.PropertyNameCaseInsensitive = true;
                return jsonSerializerOptions;
            }
        }

        public static string SerializeToString<T>(T item, JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(item, options);
        }

        public static T DeserializeFromString<T>(string json, JsonSerializerOptions options)
        {
            return (T)JsonSerializer.Deserialize(json, typeof(T), options)!;
        }     
    }
}
