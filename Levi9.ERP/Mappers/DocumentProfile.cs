using AutoMapper;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Datas.Responses;

namespace Levi9.ERP.Mappers
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile() 
        {
            CreateMap<Document, DocumentDTO>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ProductDocuments));
            CreateMap<ProductDocument, DocumentItemDTO>()
                .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(src => src.PriceValue))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.Product.LastUpdate));
            CreateMap<Product, DocumentItemDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<DocumentDTO, DocumentResponse>();

        }
    }
}
