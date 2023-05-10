using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public ClientRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ClientDTO AddClient(ClientDTO clientModel)
        {
            Client clientMap = _mapper.Map<Client>(clientModel);
            var createdEntity = _context.Clients.Add(clientMap);
            SaveChanges();
            return _mapper.Map<ClientDTO>(createdEntity.Entity);
        }

        public ClientDTO GetClientByEmail(string email)
        {
            var clientByEmail = _context.Clients.FirstOrDefault(e => e.Email == email);
            return _mapper.Map<ClientDTO>(clientByEmail);
        }

        public ClientDTO GetClientById(int id)
        {
            var clientById = _context.Clients.FirstOrDefault(e => e.Id == id);
            return _mapper.Map<ClientDTO>(clientById);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
