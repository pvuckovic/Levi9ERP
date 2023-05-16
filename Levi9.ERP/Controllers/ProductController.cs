using AutoMapper;
using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{

    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("Global/{id}")]
        public async Task<IActionResult> GetByGlobalId(Guid id)
        {
            var product = await _productService.GetProductByGlobalId(id);
            if (product == null) return NotFound("A product with that id doesn't exists.");

            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }

        [HttpGet("/Search")]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchProductRequest searchParams)
        {
            if (searchParams.Page <= 0) return BadRequest("Page must be greater than 0.");
            var mappedParams = _mapper.Map<SearchProductDTO>(searchParams);

            var products = await _productService.GetProductsByParameters(mappedParams);
            if (products == null || !products.Any()) return NotFound("No products were found that match the search parameters.");

            var responseProducts = new SearchProductResponse
            {
                Items = _mapper.Map<IEnumerable<ProductResponse>>(products),
                Page = searchParams.Page
            };
            return Ok(responseProducts);
        }
    }
}
