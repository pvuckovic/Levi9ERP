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

        public async Task<IEnumerable<Product>> GetProductsByParameters(string name, int page, string orderBy, string direction)
        {
            var query = _dataBaseContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(p => p.Name.Contains(name));

            var orderByMap = new Dictionary<string, Expression<Func<Product, object>>>
            {
                { "name", p => p.Name },
                { "id", p => p.Prices.Min(p => p.ProductId) },
                { "globalId", p => p.Prices.Min(p => p.GlobalId) },
                { "availableQuantity", p => p.AvailableQuantity }
            };

            var orderByExpression = orderByMap.GetValueOrDefault(orderBy, p => p.Name);
            var sortedQuery = (direction == "asc")
                ? query.OrderBy(orderByExpression)
                : query.OrderByDescending(orderByExpression);

            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var products = await sortedQuery
                .Include(p => p.Prices)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            return products;
        }
    }
}
