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
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, ProductResponse>();

        }
    }
}
