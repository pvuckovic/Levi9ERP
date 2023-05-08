using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public ClientDTO CreateClient(ClientDTO clientModel)
        {
            clientModel.GlobalId = Guid.NewGuid();
            var clientEntity = clientRepository.AddClient(clientModel);
            return clientEntity;

        }

    }
}
