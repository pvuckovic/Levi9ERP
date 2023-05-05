using AutoMapper;
using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Service
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }

            var priceList = await _priceListRepository.GetByIdAsync(id);
            var priceListDto = _mapper.Map<PriceListDTO>(priceList);

            return priceListDto;
        }
    }
}
