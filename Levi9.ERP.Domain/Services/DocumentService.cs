using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models.DTO.DocumentDto;
using Levi9.ERP.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DocumentService> _logger;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository documentRepository, IClientRepository clientRepository, IProductRepository productRepository, ILogger<DocumentService> logger, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
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

        public async Task<string> SyncDocuments(List<DocumentSyncDTO> documents)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentService. Timestamp: {Timestamp}.", nameof(SyncDocuments), DateTime.UtcNow);
            string lastUpdate = null;
            foreach (var document in documents)
            {

                DocumentDTO doc = _mapper.Map<DocumentDTO>(document);
                doc.ClientId = await _clientRepository.GetClientIdFromClientGlobalId(document.ClientId);
                int i = 0;
                foreach (var product in doc.Items)
                {
                    product.ProductId = await _productRepository.GetProductIdFromProductGlobalId(document.Items[i].ProductId);
                    i++;
                }
                if (await _documentRepository.DoesDocumentByGlobalIdExists(document.GlobalId))
                {
                    await _documentRepository.UpdateDocument(doc);
                    _logger.LogInformation("Document updated successfully in {FunctionName} of DocumentService. Timestamp: {Timestamp}.", nameof(SyncDocuments), DateTime.UtcNow);
                }
                else
                {
                    doc.GlobalId = Guid.NewGuid();
                    await _documentRepository.AddDocument(doc);
                    _logger.LogInformation("Document inserted successfully in {FunctionName} of DocumentService. Timestamp: {Timestamp}.", nameof(SyncDocuments), DateTime.UtcNow);
                }
                lastUpdate = document.LastUpdate;
            }
            return lastUpdate;
        }

    }
}
