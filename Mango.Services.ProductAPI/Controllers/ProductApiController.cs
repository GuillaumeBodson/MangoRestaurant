﻿using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
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
        [Authorize]
        [HttpPost]
        public async Task<object> UpdateCreate([FromBody] ProductDto productdto)
        {
            try
            {
                _response.Result = await _prodctRepository.CreateUpdateProduct(productdto);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [Authorize]
        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productdto)
        {
            try
            {
                _response.Result = await _prodctRepository.CreateUpdateProduct(productdto);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                _response.Result = await _prodctRepository.DeleteProduct(id);
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
