using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.Domain.Services
{
    public class PriceListService : IPriceListService
    {
        private readonly IPriceListRepository _priceListRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PriceListService> _logger;
        public PriceListService(IPriceListRepository priceListRepository,IMapper mapper, ILogger<PriceListService> logger) 
        {
            _priceListRepository = priceListRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PriceProductDTO> AddPrice(PriceProductDTO priceProductDTO)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(AddPrice), DateTime.UtcNow);
            var price = _mapper.Map<Price>(priceProductDTO);
            
            price.GlobalId = Guid.NewGuid();
            price.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();

            price = await _priceListRepository.AddPrice(price);
            var newPriceProductDTO = _mapper.Map<PriceProductDTO>(price);
            _logger.LogInformation("Adding new price in {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(AddPrice), DateTime.UtcNow);

            return newPriceProductDTO;
        }

        public async Task<IEnumerable<PriceListDTO>> GetAllPricesLists()
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(GetAllPricesLists), DateTime.UtcNow);
            var list = await _priceListRepository.GetAllPricesLists();

            if(!list.Any()) 
                return new List<PriceListDTO>();
            _logger.LogInformation("Retrieving prices in {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(GetAllPricesLists), DateTime.UtcNow);
            return list.Select(p => _mapper.Map<PriceListDTO>(p));
        }
        public async Task<PriceListDTO> GetByGlobalIdAsync(Guid globalId)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(GetByGlobalIdAsync), DateTime.UtcNow);
            var priceList = await _priceListRepository.GetByGlobalIdAsync(globalId);
            var priceListDto = _mapper.Map<PriceListDTO>(priceList);
            _logger.LogInformation("Retrieving price in {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(GetByGlobalIdAsync), DateTime.UtcNow);
            return priceListDto;
        }

        public async Task<PriceListDTO> GetByIdAsync(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(GetByIdAsync), DateTime.UtcNow);
            var priceList = await _priceListRepository.GetByIdAsync(id);
            var priceListDto = _mapper.Map<PriceListDTO>(priceList);
            _logger.LogInformation("Retrieving price in {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(GetByIdAsync), DateTime.UtcNow);
            return priceListDto;
        }
        public async Task<PriceProductDTO> UpdatePrice(PriceProductDTO priceProductDTO)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(UpdatePrice), DateTime.UtcNow);
            var price = _mapper.Map<Price>(priceProductDTO);

            price.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();

            price = await _priceListRepository.UpdatePrice(price);
            var newPriceProductDTO = _mapper.Map<PriceProductDTO>(price);
            _logger.LogInformation("Updating price in {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(UpdatePrice), DateTime.UtcNow);

            return newPriceProductDTO;
        }
        public async Task<IEnumerable<PriceListArticleDTO>> SearchArticle(SearchArticleDTO searchArticleDTO)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListService. Timestamp: {Timestamp}.", nameof(SearchArticle), DateTime.UtcNow);
            return await _priceListRepository.SearchArticle(searchArticleDTO.PageId, searchArticleDTO.SearchString, searchArticleDTO.OrderBy, searchArticleDTO.Direction);
        }
    }
}