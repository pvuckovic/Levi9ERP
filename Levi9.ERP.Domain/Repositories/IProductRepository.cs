﻿using Levi9.ERP.Domain.Models;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductById(int productId);
        Task<Product> GetProductByGlobalId(Guid productId);
        Task<IEnumerable<Product>> GetProductsByParameters(string name, int page, string orderBy, string direction);
    }
}
