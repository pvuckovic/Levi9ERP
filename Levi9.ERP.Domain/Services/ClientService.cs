using Levi9.ERP.Domain.Helpers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;

namespace Levi9.ERP.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientDTO> CreateClient(ClientDTO clientModel)
        {
            clientModel.GlobalId = Guid.NewGuid();
            string salt = AuthenticationHelper.GenerateRandomSalt();
            clientModel.Password = AuthenticationHelper.HashPassword(clientModel.Password, salt);
            clientModel.Salt = salt;
            clientModel.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();
            var clientEntity = await _clientRepository.AddClient(clientModel);
            return clientEntity;

        }
        public async Task<ClientDTO> GetClientByEmail(string email)
        {
            var clientEntity = await _clientRepository.GetClientByEmail(email);
            return clientEntity;
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            var clientEntity = await _clientRepository.GetClientById(id);
            return clientEntity;
        }
    }
}
