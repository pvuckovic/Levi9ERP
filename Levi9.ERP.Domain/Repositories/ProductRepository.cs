using Levi9.ERP.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Levi9.ERP.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(DataBaseContext dataBaseContext, ILogger<ProductRepository> logger)
        {
            _dataBaseContext = dataBaseContext;
            _logger = logger;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductRepository. Timestamp: {Timestamp}.", nameof(AddProductAsync), DateTime.UtcNow);
            _dataBaseContext.Products.Add(product);
            await _dataBaseContext.SaveChangesAsync();
            _logger.LogInformation("Adding new product in {FunctionName} of ProductRepository. Timestamp: {Timestamp}.", nameof(AddProductAsync), DateTime.UtcNow);
            return product;
        }

        public async Task<Product> GetProductByName(string name)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductRepository. Timestamp: {Timestamp}.", nameof(GetProductByName), DateTime.UtcNow);
            return await _dataBaseContext.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Product> GetProductById(int productId)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductRepository. Timestamp: {Timestamp}.", nameof(GetProductById), DateTime.UtcNow);
            var product = await _dataBaseContext.Products
                .Include(p => p.Prices)
                    .ThenInclude(p => p.PriceList)
                .FirstOrDefaultAsync(p => p.Id == productId);
            _logger.LogInformation("Retrieving product in {FunctionName} of ProductRepository. Timestamp: {Timestamp}.", nameof(GetProductById), DateTime.UtcNow);
            return product;
        }

        public async Task<Product> GetProductByGlobalId(Guid productId)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductRepository. Timestamp: {Timestamp}.", nameof(GetProductByGlobalId), DateTime.UtcNow);
            var product = await _dataBaseContext.Products
                .Include(p => p.Prices)
                    .ThenInclude(p => p.PriceList)
                .FirstOrDefaultAsync(p => p.GlobalId == productId);
            _logger.LogInformation("Retrieving product in {FunctionName} of ProductRepository. Timestamp: {Timestamp}.", nameof(GetProductByGlobalId), DateTime.UtcNow);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByParameters(string name, int page, string orderBy, string direction)
        {
            _logger.LogInformation("Entering {FunctionName} in ProductRepository. Timestamp: {Timestamp}.", nameof(GetProductsByParameters), DateTime.UtcNow);
            var query = _dataBaseContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(p => p.Name.Contains(name));

            var orderByMap = new Dictionary<string, Expression<Func<Product, object>>>
            {
                { "name", p => p.Name },
                { "id", p => p.Prices.First().ProductId },
                { "globalId", p => p.Prices.First().GlobalId },
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
            _logger.LogInformation("Retrieving products in {FunctionName} of ProductRepository. Timestamp: {Timestamp}.", nameof(GetProductsByParameters), DateTime.UtcNow);
            return products;
        }
    }
}
