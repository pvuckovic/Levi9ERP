using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
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
            CreateMap<PriceRequest, PriceProductDTO>();
            CreateMap<PriceProductDTO, Price>()
                .ForMember(dest => dest.PriceValue, 
                            from => from.MapFrom(src => src.Price));
            CreateMap<Price, PriceProductDTO>()
                .ForMember(dest => dest.Price,
                            from => from.MapFrom(src => src.PriceValue));
            CreateMap<PriceProductDTO, PriceResponse>();
        }
    }
}
