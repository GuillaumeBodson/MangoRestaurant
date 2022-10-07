using Mango.web.Models;
using Mango.web.Models.Factories;
using Mango.web.services.Iservices;

namespace Mango.web.services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IHttpClientFactory httpClient, IHttpRequestMessageFactory requestMessageFactory) : base(httpClient, requestMessageFactory)
        {
        }

        public async Task<T> GetCartByUserId<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "api/cart/GetCart/" + userId
            });
        }
        public async Task<T> AddToCart<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "api/cart/AddCart/",
                Data = cartDto
            });
        }
        public async Task<T> UpdateCart<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "api/cart/UpdateCart/",
                Data = cartDto
            });
        }
        public async Task<T> RemoveFromCart<T>(int cartId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "api/cart/RemoveCart/",
                Data = cartId
            });
        }
    }
}
