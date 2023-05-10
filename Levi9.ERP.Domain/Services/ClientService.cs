using Levi9.ERP.Domain.Helpers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ClientDTO CreateClient(ClientDTO clientModel)
        {
            clientModel.GlobalId = Guid.NewGuid();
            string salt = AuthenticationHelper.GenerateRandomSalt();
            clientModel.Password = AuthenticationHelper.HashPassword(clientModel.Password, salt);
            clientModel.Salt = salt;
            clientModel.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();
            var clientEntity = _clientRepository.AddClient(clientModel);
            return clientEntity;

        }
        public ClientDTO GetClientByEmail(string email)
        {
            var clientEntity = _clientRepository.GetClientByEmail(email);
            return clientEntity;
        }

        public ClientDTO GetClientById(int id)
        {
            var clientEntity = _clientRepository.GetClientById(id);
            return clientEntity;
        }
    }
}
