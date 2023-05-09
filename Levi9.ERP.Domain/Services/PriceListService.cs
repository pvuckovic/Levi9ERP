using AutoMapper;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;

namespace Levi9.ERP.Domain.Services
{
    public class PriceListService : IPriceListService
    {
        private readonly IPriceListRepository _priceListRepository;
        private readonly IMapper _mapper;
        public PriceListService(IPriceListRepository priceListRepository,IMapper mapper) 
        {
            _priceListRepository = priceListRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PriceListDTO>> GetAllPricesLists()
        {
            var list = await _priceListRepository.GetAllPricesLists();

            if(!list.Any()) 
                return new List<PriceListDTO>();

            return list.Select(p => _mapper.Map<PriceListDTO>(p));
        }
        public async Task<PriceListDTO> GetByGlobalIdAsync(Guid globalId)
        {
            var priceList = await _priceListRepository.GetByGlobalIdAsync(globalId);
            var priceListDto = _mapper.Map<PriceListDTO>(priceList);

            return priceListDto;
        }

        public async Task<PriceListDTO> GetByIdAsync(int id)
        {
            var priceList = await _priceListRepository.GetByIdAsync(id);
            var priceListDto = _mapper.Map<PriceListDTO>(priceList);

            return priceListDto;
        }
    }
}