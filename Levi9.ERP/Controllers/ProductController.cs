using AutoMapper;
using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest productRequest)
        {
            var existingProduct = await _productService.GetProductByName(productRequest.Name);
            if (existingProduct != null) return BadRequest("A product with the same name already exists.");

            var productDto = _mapper.Map<ProductDTO>(productRequest);
            var createdProduct = await _productService.CreateProductAsync(productDto);
            if (createdProduct == null) return StatusCode(500, "Failed to create product");

            var productResponse = _mapper.Map<ProductResponse>(createdProduct);
            return Ok(productResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Id is null or negative number");

            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound("A product with the same id doesn't exists.");

            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }

        [HttpGet("/Global/{id}")]
        public async Task<IActionResult> GetByGlobalId(Guid id)
        {
            var product = await _productService.GetProductByGlobalId(id);
            if (product == null) return NotFound("A product with that id doesn't exists.");

            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }
    }
}
