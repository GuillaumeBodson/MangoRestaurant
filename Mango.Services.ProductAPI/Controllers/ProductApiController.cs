using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductApiController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _prodctRepository;

        public ProductApiController(IProductRepository prodctRepository)
        {
            _prodctRepository = prodctRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                _response.Result = await _prodctRepository.GetProducts();
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                _response.Result = await _prodctRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
