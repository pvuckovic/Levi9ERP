using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Repository
{
    public interface IProductRepository
    {
        Task<ProductDTO> AddProductAsync(Product product);
        Task<Product> GetProductByName(string name);
    }
}
