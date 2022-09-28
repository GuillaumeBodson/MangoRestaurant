using Mango.web.Helpers;
using Mango.web.Models;
using Mango.web.services.Iservices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Mango.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();

            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            
            if(response?.IsSucces == true)
            {
                list = JsonHelper.DeserializeIgnoringCase<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
