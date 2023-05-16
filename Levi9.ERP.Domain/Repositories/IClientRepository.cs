using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<ClientDTO> AddClient(ClientDTO clientModel);
        Task<ClientDTO> GetClientByEmail(string email);
        Task<ClientDTO> GetClientById(int id);
        Task<bool> SaveChanges();
    }
}
