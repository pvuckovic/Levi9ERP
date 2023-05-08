using Levi9.ERP.Domain.Models;

namespace Levi9.ERP.Domain.Contracts
{
    public interface IPriceListRepository
    {
        Task<PriceList> GetByIdAsync(int id);
    }
}
