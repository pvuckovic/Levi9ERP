using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataBaseContext context;
        private readonly IMapper _mapper;

        public ClientRepository(DataBaseContext context, IMapper _mapper)
        {
            this.context = context;
            this._mapper = _mapper;
        }

        public ClientDTO AddClient(ClientDTO clientModel)
        {
            Client clientMap = _mapper.Map<Client>(clientModel);
            var createdEntity = context.Clients.Add(clientMap);
            SaveChanges();
            return _mapper.Map<ClientDTO>(createdEntity.Entity);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
