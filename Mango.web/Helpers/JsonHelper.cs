using Mango.web.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mango.web.Helpers
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

        public static StringContent ToEncodedJsonString(object data)
        {
            if(data == null)
                return null;
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
