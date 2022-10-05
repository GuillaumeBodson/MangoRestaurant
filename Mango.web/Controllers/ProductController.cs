using Mango.web.Helpers;
using Mango.web.Models;
using Mango.web.services.Iservices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
            var acessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.GetAllProductsAsync<ResponseDto>(acessToken);
            
            if(response?.IsSucces == true)
            {
                list = JsonHelper.DeserializeIgnoringCase<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var acessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.CreateProductAsync<ResponseDto>(model, acessToken);

                if(res.IsSucces == true)
                {
                    return RedirectToAction("ProductIndex");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(id, token);

            if(response?.IsSucces == true)
            {
                var product = JsonHelper.DeserializeIgnoringCase<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.UpdateProductAsync<ResponseDto>(model, token);

                if (res.IsSucces == true)
                {
                    return RedirectToAction("ProductIndex");
                }
            }

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var res = await _productService.GetProductByIdAsync<ResponseDto>(id, token);

            if (res.IsSucces == true)
            {
                return View(JsonHelper.DeserializeIgnoringCase<ProductDto>(Convert.ToString(res.Result)));
            }
            return NotFound();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var res = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId, token);


            if (res.IsSucces == true && Boolean.TrueString == res.Result.ToString())
            {
                return RedirectToAction("ProductIndex");
            }
            return View(model);
        }
    } 
}
