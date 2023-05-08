using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Levi9.ERP.Domain.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataBaseContext context;
        private readonly IMapper mapper;

        public ClientRepository(DataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ClientDTO AddClient(ClientDTO clientModel)
        {
            Client clientMap = mapper.Map<Client>(clientModel);
            var createdEntity = context.Clients.Add(clientMap);
            SaveChanges();
            return mapper.Map<ClientDTO>(createdEntity.Entity);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
