using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.Domain.Repositories
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PriceListRepository> _logger;
        public PriceListRepository(DataBaseContext dataBaseContext, IMapper mapper, ILogger<PriceListRepository> logger)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<PriceList>> GetAllPricesLists()
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(GetAllPricesLists), DateTime.UtcNow);
            return await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).ToListAsync();
        }
        public async Task<PriceList> GetByGlobalIdAsync(Guid globalId)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(GetByGlobalIdAsync), DateTime.UtcNow);
            return await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).FirstOrDefaultAsync(p => p.GlobalId == globalId);
        }
        public async Task<PriceList> GetByIdAsync(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(GetByIdAsync), DateTime.UtcNow);
            return await _dataBaseContext.PriceLists.Include(p => p.Prices).ThenInclude(p => p.Product).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Price> UpdatePrice(Price price)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(UpdatePrice), DateTime.UtcNow);
            if (PriceListExists(price.PriceListId) && ProductExists(price.ProductId))
            {
                if (PriceExists(price.PriceListId, price.ProductId))
                {
                    _dataBaseContext.Attach(price);
                    _dataBaseContext.Entry(price).Property(x => x.PriceValue).IsModified = true;
                    _dataBaseContext.Entry(price).Property(x => x.Currency).IsModified = true;
                    _dataBaseContext.Entry(price).Property(x => x.LastUpdate).IsModified = true;
                    await _dataBaseContext.SaveChangesAsync();
                    _logger.LogInformation("Updating document in {FunctionName} of PriceListRepository. Timestamp: {Timestamp}.", nameof(UpdatePrice), DateTime.UtcNow);
                    return price;
                }
            }
            return null;
        }
        public async Task<Price> AddPrice(Price price)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(AddPrice), DateTime.UtcNow);
            if (PriceListExists(price.PriceListId) && ProductExists(price.ProductId))
            {
                if (!PriceExists(price.PriceListId, price.ProductId))
                {
                    await _dataBaseContext.Prices.AddAsync(price);
                    await _dataBaseContext.SaveChangesAsync();
                    _logger.LogInformation("Adding new price in {FunctionName} of DocumentRepository. Timestamp: {Timestamp}.", nameof(AddPrice), DateTime.UtcNow);
                    return price;
                }
            }
            return null;
        }
        public async Task<IEnumerable<PriceListArticleDTO>> SearchArticle(int page, string name, OrderByArticleType? orderBy, DirectionType? direction)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(SearchArticle), DateTime.UtcNow);
            var pageSize = 1;
            var skip = (page - 1) * pageSize;

            var list = await GetAllPricesLists();

            if (!string.IsNullOrEmpty(name))
                list = list.Where(p => p.Name.ToLower().Contains(name)).ToList();

            var priceListArticleDTOs = list.Select(p => _mapper.Map<PriceListArticleDTO>(p)).ToList();

            if (orderBy == null)
            {
                priceListArticleDTOs.ForEach(p => p.Articles = (direction == DirectionType.ASC) ? p.Articles.OrderBy(a => a.Name).ToList() : p.Articles.OrderByDescending(a => a.Name).ToList());
                _logger.LogInformation("Retrieving articles in {FunctionName} of PriceListRepositort. Timestamp: {Timestamp}.", nameof(SearchArticle), DateTime.UtcNow);
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
            _logger.LogInformation("Retrieving articles in {FunctionName} of PriceListRepositort. Timestamp: {Timestamp}.", nameof(SearchArticle), DateTime.UtcNow);
            return priceListArticleDTOs.Skip(skip).Take(pageSize);
        }
        private bool ProductExists(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(ProductExists), DateTime.UtcNow);
            return _dataBaseContext.Products.Any(p => p.Id == id);
        }
        private bool PriceListExists(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(PriceListExists), DateTime.UtcNow);
            return _dataBaseContext.PriceLists.Any(p => p.Id == id);
        }
        private bool PriceExists(int priceListId, int productId)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListRepository. Timestamp: {Timestamp}.", nameof(PriceExists), DateTime.UtcNow);
            return _dataBaseContext.Prices.Any(p => p.PriceListId == priceListId && p.ProductId == productId);
        }
    }
}