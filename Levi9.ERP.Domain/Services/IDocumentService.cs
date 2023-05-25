using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models.DTO.DocumentDto;

namespace Levi9.ERP.Domain.Services
{
    public interface IDocumentService
    {
        public Task<DocumentDTO> CreateDocument(DocumentDTO documentDTO);
        public Task<DocumentDTO> GetDocumentById(int id);
        public Task<IEnumerable<DocumentDTO>> GetDocumentsByParameters(SearchDocumentDTO searchParams);
        public Task<string> SyncDocuments(List<DocumentSyncDTO> documents);
    }
}
