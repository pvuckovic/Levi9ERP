using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.Domain.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(DataBaseContext context, IMapper mapper, ILogger<ClientRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ClientDTO> AddClient(ClientDTO clientModel)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientRepository. Timestamp: {Timestamp}.", nameof(AddClient), DateTime.UtcNow);
            Client clientMap = _mapper.Map<Client>(clientModel);
            var createdEntity = _context.Clients.Add(clientMap);
            await SaveChanges();
            _logger.LogInformation("Adding new client in {FunctionName} of ClientRepository. Timestamp: {Timestamp}.", nameof(AddClient), DateTime.UtcNow);
            return _mapper.Map<ClientDTO>(createdEntity.Entity);
        }

        public async Task<ClientDTO> GetClientByEmail(string email)
        {
            _logger.LogInformation("Retrieving client in {FunctionName} of ClientRepository. Timestamp: {Timestamp}.", nameof(GetClientByEmail), DateTime.UtcNow);
            return _mapper.Map<ClientDTO>(clientByEmail);
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientRepository. Timestamp: {Timestamp}.", nameof(GetClientById), DateTime.UtcNow);
            var clientById = await _context.Clients.FirstOrDefaultAsync(e => e.Id == id);
            _logger.LogInformation("Retrieving client in {FunctionName} of ClientRepository. Timestamp: {Timestamp}.", nameof(GetClientById), DateTime.UtcNow);
            return _mapper.Map<ClientDTO>(clientById);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
