using AutoMapper;
using Levi9.ERP.Domain.Helpers;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ClientDTO> CreateClient(ClientDTO clientModel)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientService. Timestamp: {Timestamp}.", nameof(CreateClient), DateTime.UtcNow);
            if(clientModel.GlobalId == null)
                clientModel.GlobalId = Guid.NewGuid();
            string salt = AuthenticationHelper.GenerateRandomSalt();
            clientModel.Password = AuthenticationHelper.HashPassword(clientModel.Password, salt);
            clientModel.Salt = salt;
            if (clientModel.LastUpdate == null)
                clientModel.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();
            var clientEntity = await _clientRepository.AddClient(clientModel);
            _logger.LogInformation("Adding new client in {FunctionName} of ClientService. Timestamp: {Timestamp}.", nameof(CreateClient), DateTime.UtcNow);
            return clientEntity;

        }
        public async Task<ClientDTO> GetClientByEmail(string email)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientService. Timestamp: {Timestamp}.", nameof(GetClientByEmail), DateTime.UtcNow);
            var clientEntity = await _clientRepository.GetClientByEmail(email);
            _logger.LogInformation("Retrieving client in {FunctionName} of ClientService. Timestamp: {Timestamp}.", nameof(GetClientByEmail), DateTime.UtcNow);
            return clientEntity;
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientService. Timestamp: {Timestamp}.", nameof(GetClientById), DateTime.UtcNow);
            var clientEntity = await _clientRepository.GetClientById(id);
            _logger.LogInformation("Retrieving client in {FunctionName} of ClientService. Timestamp: {Timestamp}.", nameof(GetClientById), DateTime.UtcNow);
            return clientEntity;
        }

        public async Task<IEnumerable<ClientDTO>> GetClientsByLastUpdate(string lastUpdate)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientService. Timestamp: {Timestamp}.", nameof(GetClientsByLastUpdate), DateTime.UtcNow);
            var products = await _clientRepository.GetProductsByLastUpdate(lastUpdate);
            if (!products.Any())
                return new List<ClientDTO>();
            _logger.LogInformation("Retrieving products in {FunctionName} of ClientService. Timestamp: {Timestamp}.", nameof(GetClientsByLastUpdate), DateTime.UtcNow);
            return products;
        }

        public async Task<string> SyncClients(List<ClientSyncRequestDTO> clients)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientService. Timestamp: {Timestamp}.", nameof(SyncClients), DateTime.UtcNow);
            string lastUpdate = null;
            foreach (var client in clients)
            {
                //lastUpdate = DateTime.Now.ToFileTimeUtc().ToString();
                //client.LastUpdate = lastUpdate;
                if (await _clientRepository.DoesClientByGlobalIdExists(client.GlobalId))
                {
                    await _clientRepository.UpdateClient(client);
                    _logger.LogInformation("Client updated successfully in {FunctionName} of ClientService. Timestamp: {Timestamp}.", nameof(SyncClients), DateTime.UtcNow);
                }
                else if (await _clientRepository.DoesClientEmailAlreadyExists(client.GlobalId, client.Email))
                {
                    await _clientRepository.UpdateClientByEmail(client);
                    _logger.LogInformation("Client updated successfully in {FunctionName} of ClientService. Timestamp: {Timestamp}.", nameof(SyncClients), DateTime.UtcNow);
                }
                else
                {
                    var newClient = _mapper.Map<ClientDTO>(client);
                    await CreateClient(newClient);
                    _logger.LogInformation("Client inserted successfully in {FunctionName} of ClientService. Timestamp: {Timestamp}.", nameof(SyncClients), DateTime.UtcNow);
                }
                lastUpdate = client.LastUpdate;
            }
            return lastUpdate;
        }
    }
}
