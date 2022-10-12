using Mango.web.Models;
using Mango.web.Models.Factories;
using Mango.web.services.Iservices;

namespace Mango.web.services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductService _productService;

        public CartService(IHttpClientFactory httpClient, IHttpRequestMessageFactory requestMessageFactory, IHttpContextAccessor contextAccessor, IProductService productService) : base(httpClient, requestMessageFactory)
        {
            _contextAccessor = contextAccessor;
            _productService = productService;
        }

        public async Task<T> GetCartByUserId<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "/api/cart/GetCart/" + userId
            });
        }
        public async Task<T> AddToCart<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "/api/cart/AddCart/",
                Data = cartDto
            });
        }
        public async Task<T> UpdateCart<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "/api/cart/UpdateCart/",
                Data = cartDto
            });
        }
        public async Task<T> RemoveFromCart<T>(int cartId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "/api/cart/RemoveCart/",
                Data = cartId
            });
        }
        public async Task<CartDto> BuildCartDto(ProductDto product)
        {
            var resp = await _productService.GetProductByIdAsync<ResponseDto>(product.ProductId, "");

            return new CartDto()
            {
                CartHeader = new()
                {
                    UserId = _contextAccessor.HttpContext.User.FindFirst("sub").Value
                },
                CartDetails = new List<CartDetailsDto>
                {
                    new()
                    {
                        Count = product.Count,
                        ProductId = product.ProductId,
                        Product = resp.GetResult<ProductDto>(),
                    }
                }
            };
        }

        public async Task<T> ApplyCoupon<T>(CartDto cart, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "/api/cart/ApplyCoupon",
                Data = cart
            });
        }

        public async Task<T> RemoveCoupon<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Post,
                AccessToken = token,
                Url = SD.ShoppingCartAPI + "/api/cart/RemoveCoupon",
                Data = userId
            });
        }
    }
}
