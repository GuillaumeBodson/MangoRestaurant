using Mango.web.Models;
using Mango.web.services.Iservices;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Mango.web.services
{
    public class BaseService : IBaseService
    {
        public ResponseDto ResponseModel { get; set; }
        public IHttpClientFactory HttpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            HttpClient = httpClient;
            ResponseModel = new ResponseDto();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MangoApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if(apiRequest.Data != null)
                    message.Content = new StringContent(JsonSerializer.Serialize(apiRequest.Data), Encoding.UTF8, "application/json");

                HttpResponseMessage apiResponse = null;

                message.Method = apiRequest.ApiType switch
                {
                    SD.ApiType.Post => HttpMethod.Post,
                    SD.ApiType.Put => HttpMethod.Put,
                    SD.ApiType.Delete => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(apiContent);
            }
            catch (Exception ex)
            {
                var response = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string>() { Convert.ToString(ex.Message) },
                    IsSucces = false
                };

                var res = JsonSerializer.Serialize(response);
                return JsonSerializer.Deserialize<T>(res);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
