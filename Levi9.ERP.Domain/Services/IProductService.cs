using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Services
{
    public interface IProductService
    {
        Task<ProductDTO> CreateProductAsync(ProductDTO product);
        Task<ProductDTO> GetProductByName(string name);
        Task<ProductDTO> GetProductById(int productId);
        Task<ProductDTO> GetProductByGlobalId(Guid productId);      
        Task<IEnumerable<ProductDTO>> GetProductsByParameters(SearchProductDTO searchParams);
        Task<IEnumerable<ProductDTO>> GetProductsByLastUpdate(string lastUpdateDate);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
    }
}
