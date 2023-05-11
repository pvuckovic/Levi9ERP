using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Services
{
    public interface IDocumentService { 
    
        Task<DocumentDTO> GetDocumentById(int id);
    }
}
