using Mango.web.Helpers;
using Mango.web.Models;
using Mango.web.Models.Factories;
using Mango.web.services.Iservices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Mango.web.services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpRequestMessageFactory _requestMessageFactory;

        public ResponseDto ResponseModel { get; set; }
        public IHttpClientFactory HttpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient, IHttpRequestMessageFactory requestMessageFactory)
        {
            HttpClient = httpClient;
            _requestMessageFactory = requestMessageFactory;
            ResponseModel = new ResponseDto();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MangoApi");
                HttpRequestMessage message = _requestMessageFactory.Create(apiRequest.ApiType, apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if(apiRequest.Data != null)
                    message.Content = new StringContent(JsonSerializer.Serialize(apiRequest.Data), Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);

                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                
                var res = JsonHelper.DeserializeIgnoringCase<T>(apiContent);
                return res;
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
