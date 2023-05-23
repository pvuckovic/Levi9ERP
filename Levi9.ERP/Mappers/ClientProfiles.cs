using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Requests;
using Levi9.ERP.Responses;

namespace Levi9.ERP.Domain.Mappers
{
    public class ClientProfiles : Profile
    {
        public ClientProfiles()
        {
            CreateMap<ClientRequest, ClientDTO>();
            CreateMap<ClientDTO, ClientResponse>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ClientDTO, ClientResponseSync>();
            CreateMap<ClientDTO, Client>();
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientSyncRequest, ClientSyncRequestDTO>();
            CreateMap<ClientSyncRequestDTO, Client>();
            CreateMap<ClientSyncRequestDTO, ClientDTO>();
        }

    }
}
