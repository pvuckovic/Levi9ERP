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
            return await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).ToListAsync();
        }
        public async Task<PriceList> GetByGlobalIdAsync(Guid globalId)
        {
            return await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).FirstOrDefaultAsync(p => p.GlobalId == globalId);
        }
        public async Task<PriceList> GetByIdAsync(int id)
        {
            return await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).FirstOrDefaultAsync(p => p.Id == id);
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
        public async Task<IEnumerable<PriceListArticleDTO>> SearchArticle(int page,string name, OrderByArticleType? orderBy, DirectionType? direction)
        {
            var pageSize = 1;
            var skip = (page - 1) * pageSize;

            var list = await GetAllPricesLists();

            if (!string.IsNullOrEmpty(name))
                list = list.Where(p => p.Name.ToLower().Contains(name)).ToList();
            
            var priceListArticleDTOs = list.Select(p => _mapper.Map<PriceListArticleDTO>(p)).ToList();

            if (orderBy == null)
            {
                priceListArticleDTOs.ForEach(p => p.Articles = (direction == DirectionType.ASC) ? p.Articles.OrderBy(a => a.Name).ToList() : p.Articles.OrderByDescending(a => a.Name).ToList());
                return priceListArticleDTOs.Skip(skip).Take(pageSize);
            } 

            var propertySelectors = 
                new Dictionary<OrderByArticleType?, Func<IEnumerable<ArticleDTO>, IOrderedEnumerable<ArticleDTO>>>
                {
                    { OrderByArticleType.ProductId, articles => direction == DirectionType.ASC ? articles.OrderBy(a => a.Id) : articles.OrderByDescending(a => a.Id) },
                    { OrderByArticleType.ProductGloballId, articles => direction == DirectionType.ASC ? articles.OrderBy(a => a.GlobalId) : articles.OrderByDescending(a => a.GlobalId) },
                    { OrderByArticleType.ProductPrice, articles => direction == DirectionType.ASC ? articles.OrderBy(a => a.Price) : articles.OrderByDescending(a => a.Price) }
                };

            priceListArticleDTOs.ForEach(p => p.Articles = propertySelectors[orderBy](p.Articles).ToList());
            
            return priceListArticleDTOs.Skip(skip).Take(pageSize);
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