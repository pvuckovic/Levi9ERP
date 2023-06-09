﻿using AutoMapper;
using Levi9.ERP.Domain.Helpers;
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
            var clientByEmail = await _context.Clients.FirstOrDefaultAsync(e => e.Email == email);
            return _mapper.Map<ClientDTO>(clientByEmail);
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientRepository. Timestamp: {Timestamp}.", nameof(GetClientById), DateTime.UtcNow);
            var clientById = await _context.Clients.FirstOrDefaultAsync(e => e.Id == id);
            _logger.LogInformation("Retrieving client in {FunctionName} of ClientRepository. Timestamp: {Timestamp}.", nameof(GetClientById), DateTime.UtcNow);
            return _mapper.Map<ClientDTO>(clientById);
        }

        public async Task<IEnumerable<ClientDTO>> GetProductsByLastUpdate(string lastUpdate)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientRepository. Timestamp: {Timestamp}.", nameof(GetProductsByLastUpdate), DateTime.UtcNow);
            var clients = await _context.Clients
                                .Where(c => string.Compare(c.LastUpdate, lastUpdate) > 0)
                                .Include(c => c.PriceList)
                                .ToListAsync();
            _logger.LogInformation("Retrieving clients in {FunctionName} of ClientRepository. Timestamp: {Timestamp}.", nameof(GetProductsByLastUpdate), DateTime.UtcNow);
            return clients.Select(c => _mapper.Map<ClientDTO>(c));
        }

        public async Task<Client> UpdateClient(ClientSyncRequestDTO client)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientRepository. Timestamp: {Timestamp}.", nameof(UpdateClient), DateTime.UtcNow);
            Client clientMap = _mapper.Map<Client>(client);
            var updatedClient = await _context.Clients.FirstOrDefaultAsync(c => c.GlobalId == clientMap.GlobalId);
            updatedClient.Email = client.Email;
            updatedClient.Address = client.Address;
            updatedClient.Phone = client.Phone;
            updatedClient.LastUpdate = client.LastUpdate;
            updatedClient.Name = client.Name;
            updatedClient.Password = client.Password;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Retrieving confirmation of updated client in {FunctionName} of ClientRepository. Timestamp: {Timestamp}.", nameof(UpdateClient), DateTime.UtcNow);
            return clientMap;
        }

        public async Task<Client> UpdateClientByEmail(ClientSyncRequestDTO client)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientRepository. Timestamp: {Timestamp}.", nameof(UpdateClientByEmail), DateTime.UtcNow);
            Client clientMap = _mapper.Map<Client>(client);
            var updatedClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == clientMap.Email);
            updatedClient.GlobalId = client.GlobalId;
            updatedClient.Address = client.Address;
            updatedClient.Phone = client.Phone;
            updatedClient.LastUpdate = client.LastUpdate;
            updatedClient.Name = client.Name;
            updatedClient.Password = client.Password;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Retrieving confirmation of updated client in {FunctionName} of ClientRepository. Timestamp: {Timestamp}.", nameof(UpdateClientByEmail), DateTime.UtcNow);
            return clientMap;
        }

        public async Task<bool> DoesClientEmailAlreadyExists(Guid globalId, string email)
        {
            _logger.LogInformation("Entering { FunctionName} in ClientRepository.Timestamp { Timestamp}.", nameof(DoesClientEmailAlreadyExists), DateTime.UtcNow);
            var result = await _context.Clients.AnyAsync(p => p.GlobalId != globalId && p.Email == email);
            _logger.LogInformation("Retrieving confirmation if Email: {Email} already exists in { FunctionName} of ClientRepository. Timestamp { Timestamp}.", email, nameof(DoesClientEmailAlreadyExists), DateTime.UtcNow);
            return result;
        }

        public async Task<bool> DoesClientByGlobalIdExists(Guid globalId)
        {
            _logger.LogInformation("Entering { FunctionName} in ClientRepository.Timestamp { Timestamp}.", nameof(DoesClientByGlobalIdExists), DateTime.UtcNow);
            var result = await _context.Clients.AnyAsync(p => p.GlobalId == globalId);
            _logger.LogInformation("Retrieving confirmation of client with GlobalId { Id} in { FunctionName}of ClientRepository. Timestamp { Timestamp}.", globalId, nameof(DoesClientByGlobalIdExists), DateTime.UtcNow);
            return result;
        }

        public async Task<int> GetClientIdFromClientGlobalId(Guid globalId)
        {
            _logger.LogInformation("Entering { FunctionName} in ClientRepository.Timestamp { Timestamp}.", nameof(DoesClientByGlobalIdExists), DateTime.UtcNow);
            var result = await _context.Clients.FirstOrDefaultAsync(p => p.GlobalId == globalId);
            _logger.LogInformation("Retrieving confirmation of client with GlobalId { Id} in { FunctionName}of ClientRepository. Timestamp { Timestamp}.", globalId, nameof(DoesClientByGlobalIdExists), DateTime.UtcNow);
            return result.Id;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
