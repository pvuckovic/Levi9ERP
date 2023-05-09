using Levi9.ERP.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Levi9.ERP.Domain.Repositories
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        public PriceListRepository(DataBaseContext dataBaseContext) 
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<IEnumerable<PriceList>> GetAllPricesLists()
        {
            return await _dataBaseContext.PriceLists.ToListAsync();
        }
        public async Task<PriceList> GetByGlobalIdAsync(Guid globalId)
        {
            return await _dataBaseContext.PriceLists.FirstOrDefaultAsync(p => p.GlobalId == globalId);
        }

        public async Task<PriceList> GetByIdAsync(int id)
        {
            return await _dataBaseContext.PriceLists.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}