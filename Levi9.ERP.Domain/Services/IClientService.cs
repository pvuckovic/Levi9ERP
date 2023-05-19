using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Services
{
    public interface IClientService
    {
        Task<ClientDTO> CreateClient(ClientDTO clientModel);
        Task<ClientDTO> GetClientByEmail(string email);
        Task<ClientDTO> GetClientById(int id);
        Task<IEnumerable<ClientDTO>> GetClientsByLastUpdate(string lastUpdate);
    }
}
