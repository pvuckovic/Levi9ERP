using AutoMapper;
using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Models.DTO;

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
        public async Task<PriceListDTO> GetByIdAsync(int id)
        {
            var priceList = await _priceListRepository.GetByIdAsync(id);
            var priceListDto = _mapper.Map<PriceListDTO>(priceList);

            return priceListDto;
        }
    }
}
