using AutoMapper;
using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequest, ProductDTO>();
            CreateMap<ProductDTO, ProductResponse>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.PriceList, opt => opt.MapFrom(src => src.Prices));
            CreateMap<Price, PriceDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PriceListId));
        }
    }
}
