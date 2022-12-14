using System.Text.Json;

namespace Mango.Services.CouponAPI.Helpers
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
    }
}
