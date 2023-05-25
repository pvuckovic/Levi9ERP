using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IDocumentRepository
    {
        Task<DocumentDTO> AddDocument(DocumentDTO document);
        Task<DocumentDTO> GetDocumentById(int id);
        Task<IEnumerable<DocumentDTO>> GetDocumentsByParameters(string name, int page, string orderBy, string direction);
        Task<bool> DoesDocumentByGlobalIdExists(Guid globalId);
        Task<Document> UpdateDocument(DocumentDTO document);
        Task<bool> SaveChanges();
    }
}
