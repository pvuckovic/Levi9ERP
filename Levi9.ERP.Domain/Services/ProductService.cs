using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Levi9.ERP.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO newProduct)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductService. Timestamp: {Timestamp}.", nameof(CreateProductAsync), DateTime.UtcNow);
            var product = new Product
            {
                Name = newProduct.Name,
                GlobalId = Guid.NewGuid(),
                ImageUrl = newProduct.ImageUrl,
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString(),
            };
            Product addedProduct = await _productRepository.AddProductAsync(product);
            var productDto = _mapper.Map<ProductDTO>(addedProduct);
            _logger.LogInformation("Adding new product in {FunctionName} of ProductService. Timestamp: {Timestamp}.", nameof(CreateProductAsync), DateTime.UtcNow);
            return productDto;
        }

        public async Task<ProductDTO> GetProductByName(string name)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductService. Timestamp: {Timestamp}.", nameof(GetProductByName), DateTime.UtcNow);
            var product = await _productRepository.GetProductByName(name);
            var productDto = _mapper.Map<ProductDTO>(product);
            _logger.LogInformation("Retrieving product in {FunctionName} of ProductService. Timestamp: {Timestamp}.", nameof(GetProductByName), DateTime.UtcNow);

            return productDto;
        }

        public async Task<ProductDTO> GetProductById(int productId)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductService. Timestamp: {Timestamp}.", nameof(GetProductById), DateTime.UtcNow);

            var product = await _productRepository.GetProductById(productId);
            var productDto = _mapper.Map<ProductDTO>(product);
            _logger.LogInformation("Retrieving product in {FunctionName} of ProductService. Timestamp: {Timestamp}.", nameof(GetProductById), DateTime.UtcNow);

            return productDto;
        }

        public async Task<ProductDTO> GetProductByGlobalId(Guid productId)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductService. Timestamp: {Timestamp}.", nameof(GetProductByGlobalId), DateTime.UtcNow);

            var product = await _productRepository.GetProductByGlobalId(productId);
            var productDto = _mapper.Map<ProductDTO>(product);
            _logger.LogInformation("Retrieving product in {FunctionName} of ProductService. Timestamp: {Timestamp}.", nameof(GetProductByGlobalId), DateTime.UtcNow);

            return productDto;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByParameters(SearchProductDTO searchParams)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductService. Timestamp: {Timestamp}.", nameof(GetProductsByParameters), DateTime.UtcNow);
            var products = await _productRepository.GetProductsByParameters(searchParams.Name, searchParams.Page, searchParams.OrderBy, searchParams.Direction);
            var mappedProducts = products.Select(p => _mapper.Map<ProductDTO>(p));
            _logger.LogInformation("Retrieving products in {FunctionName} of ProductService. Timestamp: {Timestamp}.", nameof(GetProductsByParameters), DateTime.UtcNow);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByLastUpdate(string lastUpdate)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductService. Timestamp: {Timestamp}.", nameof(GetProductsByLastUpdate), DateTime.UtcNow);
            var products = await _productRepository.GetProductsByLastUpdate(lastUpdate);
            if (!products.Any())
                return new List<ProductDTO>();
            _logger.LogInformation("Retrieving products in {FunctionName} of ProductService. Timestamp: {Timestamp}.", nameof(GetProductsByLastUpdate), DateTime.UtcNow);
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            _logger.LogInformation("Entering {FunctionName} in ProductService. Timestamp: {Timestamp}.", nameof(GetAllProducts), DateTime.UtcNow);

            var products = await _productRepository.GetAllProducts();

            _logger.LogInformation("Retrieving all products in {FunctionName} of ProductService. Timestamp: {Timestamp}.", nameof(GetAllProducts), DateTime.UtcNow);

            var productDTOs = _mapper.Map<List<ProductDTO>>(products);

            return productDTOs;
        }
    }
}
