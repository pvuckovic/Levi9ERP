using AutoMapper;
using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{

    [Route("v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest productRequest)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(CreateProductAsync), DateTime.UtcNow);
            var existingProduct = await _productService.GetProductByName(productRequest.Name);
            if (existingProduct != null)
            {
                _logger.LogInformation("Product with that name already exists in {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(CreateProductAsync), DateTime.UtcNow);
                return BadRequest("A product with the same name already exists.");
            }

            var productDto = _mapper.Map<ProductDTO>(productRequest);
            var createdProduct = await _productService.CreateProductAsync(productDto);
            if (createdProduct == null)
            {
                _logger.LogInformation("Failed to create product in {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(CreateProductAsync), DateTime.UtcNow);
                return StatusCode(500, "Failed to create product");
            }
            _logger.LogInformation("Product created successfully in {FunctionName} of ProductController. Timestamp: {Timestamp}.", nameof(CreateProductAsync), DateTime.UtcNow);
            var productResponse = _mapper.Map<ProductResponse>(createdProduct);
            _logger.LogInformation("Product created successfully in {FunctionName} of ClientController. Timestamp: {Timestamp}.", nameof(CreateProductAsync), DateTime.UtcNow);
            return Ok(productResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
            if (id <= 0)
            {
                _logger.LogInformation("Id {id} is null or negative number {FunctionName} in ProductController. Timestamp: {Timestamp}.", id, nameof(GetById), DateTime.UtcNow);
                return BadRequest("Id is null or negative number");
            }

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                _logger.LogInformation("A product with the same id doesn't exists {FunctionName} in ProductController. Timestamp: {Timestamp}.", id, nameof(GetById), DateTime.UtcNow);
                return NotFound("A product with the same id doesn't exists.");
            }


            var productResponse = _mapper.Map<ProductResponse>(product);
            _logger.LogInformation("Product retrieved successfully in {FunctionName} of ClientController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
            return Ok(productResponse);
        }

        [HttpGet("/Global/{id}")]
        public async Task<IActionResult> GetByGlobalId(Guid id)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
            var product = await _productService.GetProductByGlobalId(id);
            if (product == null)
            {
                _logger.LogInformation("A product with that id doesn't exists {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
                return NotFound("A product with that id doesn't exists.");
            }

            var productResponse = _mapper.Map<ProductResponse>(product);
            _logger.LogInformation("Product retrieved successfully in {FunctionName} of ClientController. Timestamp: {Timestamp}.", nameof(GetByGlobalId), DateTime.UtcNow);
            return Ok(productResponse);
        }

        [HttpGet("/Product")]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchProductRequest searchParams)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
            if (searchParams.Page <= 0)
            {
                _logger.LogInformation("Page must be greater than 0 {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
                return BadRequest("Page must be greater than 0.");
            }
            var mappedParams = _mapper.Map<SearchProductDTO>(searchParams);

            var products = await _productService.GetProductsByParameters(mappedParams);
            if (products == null || !products.Any())
            {
                _logger.LogInformation("No products were found that match the search parameters {FunctionName} in ProductController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
                return NotFound("No products were found that match the search parameters.");
            }

            var responseProducts = new SearchProductResponse
            {
                Items = _mapper.Map<IEnumerable<ProductResponse>>(products),
                Page = searchParams.Page
            };
            _logger.LogInformation("Successful search in {FunctionName} of ClientController. Timestamp: {Timestamp}.", nameof(SearchProducts), DateTime.UtcNow);
            return Ok(responseProducts);
        }
    }
}
