using System.Text.Json;
using System.Text;

namespace Mango.Services.OrderAPI.Helpers
{
    public static class JsonHelper
    {
        public static TValue DeserializeIgnoringCase<TValue>(string json)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<TValue>(json, jsonOptions);
        }

        public static StringContent ToUTF8EncodedJsonStringContent(this object data)
        {
            if (data == null)
                return null;
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
