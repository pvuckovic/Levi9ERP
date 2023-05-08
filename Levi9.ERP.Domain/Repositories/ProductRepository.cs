using Levi9.ERP.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
