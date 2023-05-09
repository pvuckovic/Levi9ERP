using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Contracts
{
    public interface IPriceListService
    {
        Task<PriceListDTO> GetByIdAsync(int id);
    }
}
