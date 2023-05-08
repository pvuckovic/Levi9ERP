using AutoMapper;
using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Request;
using Levi9.ERP.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
