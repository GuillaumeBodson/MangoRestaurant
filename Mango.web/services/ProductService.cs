using Mango.web.Models;
using Mango.web.Models.Factories;
using Mango.web.services.Iservices;
using Mango.web.Models;

namespace Mango.web.services
{
    public class ProductService : BaseService,IProductService
    {
        public ResponseDto ResponseModel { get; set; }
        public ProductService(IHttpClientFactory httpClientFactory, IHttpRequestMessageFactory requestMessageFactory) : base(httpClientFactory, requestMessageFactory)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = product,
                Url = SD.ProductApiBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Delete,
                
                Url = SD.ProductApiBase + "/api/products/" + id,
                AccessToken = ""
            });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductApiBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,                
                Url = SD.ProductApiBase + "/api/products/" + id,
                AccessToken = ""
            }); 
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto product)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Put,
                Data = product,
                Url = SD.ProductApiBase + "/api/products",
                AccessToken = ""
            });
        }
    }
}
