using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IClientRepository
    {
        ClientDTO AddClient(ClientDTO clientModel);
        bool SaveChanges();
    }
}
