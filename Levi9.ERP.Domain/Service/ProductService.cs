using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO> CreateProductAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Product name cannot be null or empty.");
            var product = new Product
            {
                Name = name,
                GlobalId = Guid.NewGuid(),
                ImageUrl = "someurl.png",
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString(),
            };
            var addedProduct = await _productRepository.AddProductAsync(product);

            return addedProduct;
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _productRepository.GetProductByName(name);
        }
    }
}
