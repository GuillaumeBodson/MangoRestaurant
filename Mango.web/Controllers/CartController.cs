using Mango.web.Models;
using Mango.web.services.Iservices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDto());
        }
        [Authorize]
        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _cartService.RemoveFromCart<ResponseDto>(cartDetailsId, accessToken);

            if(res?.IsSucces == true)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View(); 
        }

        private async Task<CartDto> LoadCartDto()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var res = await _cartService.GetCartByUserId<ResponseDto>(userId, accessToken);

            var card = res.GetResult<CartDto>();

            if(card.CartHeader != null)
            {
                card.CartHeader.OrderTotal = card.CartDetails.Sum(x => x.Product.Price * x.Count);
            }

            return card;
        }
    }
}
