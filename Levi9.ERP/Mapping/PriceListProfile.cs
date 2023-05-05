using AutoMapper;
using Levi9.ERP.Data.Response;
using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP
{
    public class PriceListProfile : Profile
    {
        public PriceListProfile() 
        {
            CreateMap<PriceList, PriceListDTO>()
                .ForMember(
                    dest => dest.Id,
                    from => from.MapFrom(x => $"{x.Id}"))
                .ForMember(
                    dest => dest.GlobalId,
                    from => from.MapFrom(x => $"{x.GlobalId}"))
                .ForMember(
                    dest => dest.Name,
                    from => from.MapFrom(x => $"{x.Name}"))
                .ForMember(
                    dest => dest.LastUpdate,
                    from => from.MapFrom(x => $"{x.LastUpdate}"));

            CreateMap<PriceListDTO, PriceListResponse>()
                .ForMember(
                    dest => dest.Id,
                    from => from.MapFrom(x => $"{x.Id}"))
                .ForMember(
                    dest => dest.GlobalId,
                    from => from.MapFrom(x => $"{x.GlobalId}"))
                .ForMember(
                    dest => dest.Name,
                    from => from.MapFrom(x => $"{x.Name}"))
                .ForMember(
                    dest => dest.LastUpdate,
                    from => from.MapFrom(x => $"{x.LastUpdate}"));
        }
    }
}
