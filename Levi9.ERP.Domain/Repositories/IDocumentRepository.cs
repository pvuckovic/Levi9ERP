using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IDocumentRepository
    {
        public Task<DocumentDTO> AddDocument(DocumentDTO document);
        public Task<DocumentDTO> GetDocumentById(int id);
        Task<IEnumerable<DocumentDTO>> GetDocumentsByParameters(string name, int page, string orderBy, string direction);
        public Task<bool> SaveChanges();
    }
}
