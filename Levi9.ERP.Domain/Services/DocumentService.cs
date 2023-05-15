using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;

namespace Levi9.ERP.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentDTO> CreateDocument(DocumentDTO documentModel)
        {
            documentModel.GlobalId = Guid.NewGuid();
            documentModel.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();
            var documentEntity = await _documentRepository.AddDocument(documentModel);
            return documentEntity;
        }
        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            var document = await _documentRepository.GetDocumentById(id);
            return document;
        }
        public async Task<IEnumerable<DocumentDTO>> GetDocumentsByParameters(SearchDocumentDTO searchParams)
        {
            var products = await _documentRepository.GetDocumentsByParameters(searchParams.Name, searchParams.Page, searchParams.OrderBy, searchParams.Direction);
            return products;
        }
    }
}
