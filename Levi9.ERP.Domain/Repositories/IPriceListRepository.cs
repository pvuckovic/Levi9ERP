using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IPriceListRepository
    {
        Task<PriceList> GetByIdAsync(int id);
        Task<IEnumerable<PriceList>> GetAllPricesLists();
        Task<PriceList> GetByGlobalIdAsync(Guid globalId);
        Task<Price> AddPrice(Price price);
        Task<Price> UpdatePrice(Price price);
        Task<IEnumerable<PriceListArticleDTO>> SearchArticle(int page, string name, OrderByArticleType? orderBy, DirectionType? direction);
    }
}
