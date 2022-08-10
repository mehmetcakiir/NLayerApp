using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IService<Product> service, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;
        }

        [HttpGet("GetProductWithCategorys")]
        public async Task<IActionResult> GetProductWithCategorys()
        {
            return CreateActionResault(await _service.GetProductWithCatagory());
        }


        // Select all
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsycn();
            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Succes(200,productsDto));
            return CreateActionResault<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Succes(200, productsDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        // Select
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(products);
            //return Ok(CustomResponseDto<ProductDto>.Succes(200, productsDto));
            return CreateActionResault<ProductDto>(CustomResponseDto<ProductDto>.Succes(200, productsDto));
        }

        // Veri kaydetme
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var products = await _service.AddAsycn(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(products);
            //return Ok(CustomResponseDto<ProductDto>.Succes(201, productsDto));
            return CreateActionResault<ProductDto>(CustomResponseDto<ProductDto>.Succes(201, productsDto));
        }

        // Veri güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _service.Update(_mapper.Map<Product>(productDto));
            //return Ok(CustomResponseDto<ProductDto>.Succes(204));
            return CreateActionResault<ProductDto>(CustomResponseDto<ProductDto>.Succes(204));
        }

        // Veri silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.Remove(product);
            //return Ok(CustomResponseDto<ProductDto>.Succes(204));
            return CreateActionResault<ProductDto>(CustomResponseDto<ProductDto>.Succes(204));
        }

    }
}
