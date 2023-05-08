using AutoMapper;
using Levi9.ERP.Data.Response;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Mappers
{
    public class PriceListProfile : Profile
    {
        public PriceListProfile()
        {
            CreateMap<PriceList, PriceListDTO>();
            CreateMap<PriceListDTO, PriceListResponse>();
        }
    }
}
