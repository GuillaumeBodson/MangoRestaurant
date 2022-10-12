using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private ResponseDto _response;

        public CartAPIController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _response = new ResponseDto();
        }
        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            await _response.TrySetResult(async () => await _cartRepository.GetCartByUserId(userId));
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            await _response.TrySetResult(async () => await _cartRepository.CreateUpdateCart(cartDto));
            return _response;
        }
        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            await _response.TrySetResult(async () => await _cartRepository.CreateUpdateCart(cartDto));
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemmoveCart([FromBody]int cartId)
        {
            await _response.TrySetResult(async () => await _cartRepository.RemoveFromCart(cartId));
            return _response;
        }
        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto Cart)
        {
            await _response.TrySetResult(async () => await _cartRepository.ApplyCoupon(Cart.CartHeader.UserId, Cart.CartHeader.CouponCode));

            return _response;
        }
        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            await _response.TrySetResult(async () => await _cartRepository.RemoveCoupon(userId));

            return _response;
        }
    }
}
