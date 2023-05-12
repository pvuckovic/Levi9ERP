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
            CreateMap<SearchArticleRequest, SearchArticleDTO>()
                .ForMember(dest => dest.SearchString,
                            from => from.MapFrom(src => src.SearchString.ToLower()));
            CreateMap<PriceList, PriceListArticleDTO>()
                .ForMember(dest => dest.Articles,
                            from => from.MapFrom(src => src.Prices));
            CreateMap<Price, ArticleDTO>()
                .ForMember(dest => dest.Id,
                            from => from.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.GlobalId,
                            from => from.MapFrom(src => src.Product.GlobalId))
                .ForMember(dest => dest.Name,
                            from => from.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price,
                            from => from.MapFrom(src => src.PriceValue))
                .ForMember(dest => dest.Currency,
                            from => from.MapFrom(src => src.Currency))
                .ForMember(dest => dest.LastUpdate,
                            from => from.MapFrom(src => src.Product.LastUpdate));
            CreateMap<PriceListArticleDTO, PriceListArticleResponse>()
                .ForMember(dest => dest.Articles,
                            from => from.MapFrom(src => src.Articles));
        }
    }
}
