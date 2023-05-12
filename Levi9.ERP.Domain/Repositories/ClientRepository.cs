using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ClientDTO> AddClient(ClientDTO clientModel)
        {
            Client clientMap = _mapper.Map<Client>(clientModel);
            var createdEntity = _context.Clients.Add(clientMap);
            await SaveChanges();
            return _mapper.Map<ClientDTO>(createdEntity.Entity);
        }

        public async Task<ClientDTO> GetClientByEmail(string email)
        {
            var clientByEmail = await _context.Clients.FirstOrDefaultAsync(e => e.Email == email);
            return _mapper.Map<ClientDTO>(clientByEmail);
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            var clientById = await _context.Clients.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<ClientDTO>(clientById);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
