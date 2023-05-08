using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Services
{
    public interface IProductService
    {
        Task<ProductDTO> CreateProductAsync(ProductDTO product);
        Task<ProductDTO> GetProductByName(string name);
    }
}
