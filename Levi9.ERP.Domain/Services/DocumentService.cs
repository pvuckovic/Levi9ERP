using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(IDocumentRepository documentRepository, ILogger<DocumentService> logger)
        {
            _documentRepository = documentRepository;
            _logger = logger;
        }

        public async Task<DocumentDTO> CreateDocument(DocumentDTO documentModel)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentService. Timestamp: {Timestamp}.", nameof(CreateDocument), DateTime.UtcNow);
            documentModel.GlobalId = Guid.NewGuid();
            documentModel.LastUpdate = DateTime.Now.ToFileTimeUtc().ToString();
            var documentEntity = await _documentRepository.AddDocument(documentModel);
            _logger.LogInformation("Adding new document in {FunctionName} of DocumentService. Timestamp: {Timestamp}.", nameof(CreateDocument), DateTime.UtcNow);
            return documentEntity;
        }
        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentService. Timestamp: {Timestamp}.", nameof(GetDocumentById), DateTime.UtcNow);
            var document = await _documentRepository.GetDocumentById(id);
            _logger.LogInformation("Retrieving document in {FunctionName} of DocumentService. Timestamp: {Timestamp}.", nameof(GetDocumentById), DateTime.UtcNow);
            return document;
        }
        public async Task<IEnumerable<DocumentDTO>> GetDocumentsByParameters(SearchDocumentDTO searchParams)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientService. Timestamp: {Timestamp}.", nameof(GetDocumentsByParameters), DateTime.UtcNow);
            var products = await _documentRepository.GetDocumentsByParameters(searchParams.Name, searchParams.Page, searchParams.OrderBy.ToString(), searchParams.Direction.ToString());
            _logger.LogInformation("Search document in {FunctionName} of DocumentService. Timestamp: {Timestamp}.", nameof(GetDocumentsByParameters), DateTime.UtcNow);
            return products;
        }
    }
}
