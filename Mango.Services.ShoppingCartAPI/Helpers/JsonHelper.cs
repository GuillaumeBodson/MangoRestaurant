using System.Text.Json;

namespace Mango.Services.ShoppingCartAPI.Helpers
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
        public async static Task<T> GetDeserializeHttpResponseContent<T>(this HttpClient client, string requestUri)
        {
            var response = await client.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();

            return DeserializeIgnoringCase<T>(content);
        }
    }
}
