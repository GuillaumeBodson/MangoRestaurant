using Mango.web.Models;
using Mango.web.Models.Factories;
using Mango.web.services.Iservices;

namespace Mango.web.services
{
    public class ProductService : BaseService,IProductService
    {
        public ResponseDto ResponseModel { get; set; }
        public ProductService(IHttpClientFactory httpClientFactory, IHttpRequestMessageFactory requestMessageFactory) : base(httpClientFactory, requestMessageFactory)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = product,
                Url = SD.ProductApiBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Delete,
                
                Url = SD.ProductApiBase + "/api/products/" + id,
                AccessToken = token
            });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductApiBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,                
                Url = SD.ProductApiBase + "/api/products/" + id,
                AccessToken = token
            }); 
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto product, string token)  
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Put,
                Data = product,
                Url = SD.ProductApiBase + "/api/products",
                AccessToken = token
            });
        }
    }
}
