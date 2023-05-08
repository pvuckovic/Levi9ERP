using AutoMapper;
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
            CreateMap<ClientDTO, ClientResponse>();
            CreateMap<ClientDTO, Client>();
            CreateMap<Client, ClientDTO>();
        }

    }
}
