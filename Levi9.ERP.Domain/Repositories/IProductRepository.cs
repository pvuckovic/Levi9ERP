using Levi9.ERP.Domain.Models;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductByName(string name);
    }
}
