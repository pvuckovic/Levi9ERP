using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;

namespace Levi9.ERP.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO newProduct)
        {
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
            return productDto;
        }

        public async Task<ProductDTO> GetProductByName(string name)
        {
            var product = await _productRepository.GetProductByName(name);
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }

        public async Task<ProductDTO> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductById(productId);
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }

        public async Task<ProductDTO> GetProductByGlobalId(Guid productId)
        {
            var product = await _productRepository.GetProductByGlobalId(productId);
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }
    }
}
