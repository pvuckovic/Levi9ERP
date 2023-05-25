using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models.DTO.DocumentDto;

namespace Levi9.ERP.Mappers
{

    public class DocumentProfiles : Profile
    {
        public DocumentProfiles()
        {
            CreateMap<ProductDocument, DocumentItemDTO>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<DocumentItemDTO, ProductDocument>()
                .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(src => src.PriceValue));
            CreateMap<DocumentRequest, DocumentDTO>();
            CreateMap<DocumentDTO, DocumentResponse>()
               .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.Id));
            CreateMap<DocumentDTO, Document>()
               .ForMember(dest => dest.ProductDocuments, opt => opt.MapFrom(src => src.Items))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ReverseMap();
            CreateMap<SearchDocumentRequest, SearchDocumentDTO>();
            //Sync documents
            CreateMap<DocumentSyncRequest, DocumentSyncDTO>();
            CreateMap<DocumentItemSyncRequest, DocumentItemSyncDTO>()
                            .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(src => src.Price));
            CreateMap<DocumentSyncDTO, DocumentDTO>()
                            .ForMember(dest => dest.ClientId, opt => opt.Ignore());
            CreateMap<DocumentItemSyncDTO, DocumentItemDTO>()
                            .ForMember(dest => dest.ProductId, opt => opt.Ignore());
                            
        }
    }
}

