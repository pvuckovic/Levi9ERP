using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Services
{
    public interface IClientService
    {
        ClientDTO CreateClient(ClientDTO clientModel);
    }
}
