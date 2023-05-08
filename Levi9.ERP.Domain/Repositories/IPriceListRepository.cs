using Levi9.ERP.Domain.Models;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IPriceListRepository
    {
        Task<PriceList> GetByIdAsync(int id);
        Task<IEnumerable<PriceList>> GetAllPricesLists();
    }
}
