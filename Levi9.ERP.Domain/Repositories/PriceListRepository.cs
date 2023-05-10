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

        public async Task<Price> AddPrice(Price price)
        {
            if(PriceListExists(price.PriceListId) && ProductExists(price.ProductId))
            {
                if (!PriceExists(price.PriceListId,price.ProductId))
                {
                    await _dataBaseContext.Prices.AddAsync(price);
                    await _dataBaseContext.SaveChangesAsync();
                    return price;
                }
            }
            return null;
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

        private bool ProductExists(int id)
        {
            return _dataBaseContext.Products.Any(p => p.Id == id);
        }
        private bool PriceListExists(int id)
        {
            return _dataBaseContext.PriceLists.Any(p => p.Id == id);
        }
        private bool PriceExists(int priceListId, int productId)
        {
            return _dataBaseContext.Prices.Any(p => p.PriceListId == priceListId && p.ProductId == productId);
        }
    }
}