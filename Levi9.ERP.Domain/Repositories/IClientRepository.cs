using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<ClientDTO> AddClient(ClientDTO clientModel);
        Task<ClientDTO> GetClientByEmail(string email);
        Task<ClientDTO> GetClientById(int id);
        Task<bool> SaveChanges();
        Task<IEnumerable<ClientDTO>> GetProductsByLastUpdate(string lastUpdate);
        Task<bool> DoesClientEmailAlreadyExists(Guid globalId, string email);
        Task<bool> DoesClientByGlobalIdExists(Guid globalId);
        Task<Client> UpdateClient(ClientSyncRequestDTO client);  
        Task<Client> UpdateClientByEmail(ClientSyncRequestDTO client);

    }
}
