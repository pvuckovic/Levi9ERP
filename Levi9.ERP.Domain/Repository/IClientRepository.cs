using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Repository
{
    public interface IClientRepository
    {
        ClientDTO AddClient(ClientDTO clientModel);
        bool SaveChanges();
    }
}
