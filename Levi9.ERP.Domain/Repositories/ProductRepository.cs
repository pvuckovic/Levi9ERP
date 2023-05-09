using Levi9.ERP.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Levi9.ERP.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public ProductRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _dataBaseContext.Products.Add(product);
            await _dataBaseContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _dataBaseContext.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _dataBaseContext.Products
                .Include(p => p.Prices)
                    .ThenInclude(p => p.PriceList)
                .FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<Product> GetProductByGlobalId(Guid productId)
        {
            var product = await _dataBaseContext.Products
                .Include(p => p.Prices)
                    .ThenInclude(p => p.PriceList)
                .FirstOrDefaultAsync(p => p.GlobalId == productId);
            return product;
        }
    }
}
