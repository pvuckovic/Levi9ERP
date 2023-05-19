using Levi9.ERP.Domain.Helpers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<ClientDTO> CreateClient(ClientDTO clientModel)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientService. Timestamp: {Timestamp}.", nameof(CreateClient), DateTime.UtcNow);
            clientModel.GlobalId = Guid.NewGuid();
            string salt = AuthenticationHelper.GenerateRandomSalt();
            clientModel.Password = AuthenticationHelper.HashPassword(clientModel.Password, salt);
            clientModel.Salt = salt;
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
    }
}
