using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;

namespace Levi9.ERP.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAuthenticatationService _authenticationService;

        public ClientService(IClientRepository _clientRepository, IAuthenticatationService _authenticationService)
        {
            this._clientRepository = _clientRepository;
            this._authenticationService = _authenticationService;
        }

        public ClientDTO CreateClient(ClientDTO clientModel)
        {
            clientModel.GlobalId = Guid.NewGuid();
            string salt = _authenticationService.GenerateRandomSalt();
            clientModel.Password = _authenticationService.HashPassword(clientModel.Password, salt);
            clientModel.Salt = salt;
            clientModel.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();
            var clientEntity = _clientRepository.AddClient(clientModel);
            return clientEntity;

        }
    }
}
