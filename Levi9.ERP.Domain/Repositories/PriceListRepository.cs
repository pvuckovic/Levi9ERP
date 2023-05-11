using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Levi9.ERP.Domain.Repositories
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;
        public PriceListRepository(DataBaseContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
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
            return await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task<Price> UpdatePrice(Price price)
        {
            if (PriceListExists(price.PriceListId) && ProductExists(price.ProductId))
            {
                if (PriceExists(price.PriceListId, price.ProductId))
                {
                    _dataBaseContext.Attach(price);
                    _dataBaseContext.Entry(price).Property(x => x.PriceValue).IsModified = true;
                    _dataBaseContext.Entry(price).Property(x => x.Currency).IsModified = true;
                    _dataBaseContext.Entry(price).Property(x => x.LastUpdate).IsModified = true;
                    await _dataBaseContext.SaveChangesAsync();
                    return price;
                }
            }
            return null;
        }
        public async Task<Price> AddPrice(Price price)
        {
            if (PriceListExists(price.PriceListId) && ProductExists(price.ProductId))
            {
                if (!PriceExists(price.PriceListId, price.ProductId))
                {
                    await _dataBaseContext.Prices.AddAsync(price);
                    await _dataBaseContext.SaveChangesAsync();
                    return price;
                }
            }
            return null;
        }

        public async Task<List<KeyValuePair<string, IEnumerable<ArticleDTO>>>> SearchArticle(string name, OrderByArticleType orderBy, string direction)
        {
            var list = await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).ToListAsync();

            if (!string.IsNullOrEmpty(name))
                list = list.Where(p => p.Name.Contains(name)).ToList();

            List<KeyValuePair<string, IEnumerable<ArticleDTO>>> listArticle = new List<KeyValuePair<string, IEnumerable<ArticleDTO>>>();

            foreach (var priceList in list)
            {
                var tempListArticle = new List<ArticleDTO>();

                foreach (var article in priceList.Prices)
                {
                    tempListArticle.Add(_mapper.Map<ArticleDTO>(article));
                }
                listArticle.Add(new KeyValuePair<string, IEnumerable<ArticleDTO>>(priceList.Name, tempListArticle));
            }

            //TO DO: logic for OrderBy and Direction
            return listArticle;
        }
    }
}