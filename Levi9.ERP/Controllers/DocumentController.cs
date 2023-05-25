using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models.DTO.DocumentDto;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IClientService _clientService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService, IClientService clientService, IProductService productService, IMapper mapper, IUrlHelper urlHelper, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _clientService = clientService;
            _productService = productService;
            _mapper = mapper;
            _urlHelper = urlHelper;
            _logger = logger;   
        }
        [Authorize]
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentRequest document)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentController. Timestamp: {Timestamp}.", nameof(CreateDocument), DateTime.UtcNow);
            if (await _clientService.GetClientById(document.ClientId) == null)
            {
                _logger.LogError("Invalid client ID: {ClientId} in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", document.ClientId, nameof(CreateDocument), DateTime.UtcNow);
                return NotFound("Client doesn't exists");
            }

            foreach (var product in document.Items)
            {
                var productbyId = await _productService.GetProductById(product.ProductId);
                if (productbyId == null)
                {
                    _logger.LogError("Product doesn't exists in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", nameof(CreateDocument), DateTime.UtcNow);
                    return NotFound($"Product: {product.Name} doesn't exists");
                }
            }

            var documentMap = _mapper.Map<DocumentDTO>(document);
            var documentDTO = await _documentService.CreateDocument(documentMap);
            string location = _urlHelper.Action("CreateDocument", "Document", new { documentId = documentDTO.Id }, Request.Scheme);
            _logger.LogInformation("Document created successfully in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", nameof(CreateDocument), DateTime.UtcNow);
            return Created(location, _mapper.Map<DocumentResponse>(documentDTO));
        }
        [Authorize]
        [HttpGet("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentController. Timestamp: {Timestamp}.", nameof(GetById), DateTime.UtcNow);
            var document = await _documentService.GetDocumentById(id);
            if (document == null)
            {
                _logger.LogError("Document ID: {DocumentId} not found in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", id, nameof(GetById), DateTime.UtcNow);
                return NotFound("A document with that id doesn't exists");
            }
            var documentResponse = _mapper.Map<DocumentResponse>(document);
            _logger.LogInformation("Document retrieved successfully with ID: {DocumentId} in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", id, nameof(GetById), DateTime.UtcNow);
            return Ok(documentResponse);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchDocuments([FromQuery] SearchDocumentRequest searchParams)
        {
            var mappedParams = _mapper.Map<SearchDocumentDTO>(searchParams);
            var documents = await _documentService.GetDocumentsByParameters(mappedParams);
            if (!documents.Any())
            {
                _logger.LogInformation("No documents were found in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", nameof(SearchDocuments), DateTime.UtcNow);
                return NotFound("No documents were found that match the search parameters");
            }

            var responseProducts = new SearchDocumentResponse
            {
                Items = _mapper.Map<IEnumerable<DocumentResponse>>(documents),
                Page = searchParams.Page
            };
            _logger.LogInformation("Successful search in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", nameof(SearchDocuments), DateTime.UtcNow);
            return Ok(responseProducts);
        }

        [HttpPost("sync")]
        [AllowAnonymous]
        public async Task<IActionResult> SyncDocuments(List<DocumentSyncRequest> documents)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentController. Timestamp: {Timestamp}.", nameof(SyncDocuments), DateTime.UtcNow);
            var newDocuments = _mapper.Map<List<DocumentSyncDTO>>(documents);
            string result = await _documentService.SyncDocuments(newDocuments);
            if (result == null)
            {
                _logger.LogError("Filed to update documents in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", nameof(SyncDocuments), DateTime.UtcNow);
                string error = "Update failed!";
                return BadRequest(error);
            }
            _logger.LogInformation("Documents updated successfully in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", nameof(SyncDocuments), DateTime.UtcNow);
            return Ok(result);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllDocuments()
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentController. Timestamp: {Timestamp}.", nameof(GetAllDocuments), DateTime.UtcNow);
            var documents = await _documentService.GetAllDocuments();
            if (documents == null || !documents.Any())
            {
                _logger.LogInformation("No documents found in {FunctionName} in DocumentController. Timestamp: {Timestamp}.", nameof(GetAllDocuments), DateTime.UtcNow);
                return NotFound("No documents found.");
            }

            var documentsResponses = _mapper.Map<IEnumerable<DocumentResponse>>(documents);
            _logger.LogInformation("Retrieving all documents in {FunctionName} of DocumentController. Timestamp: {Timestamp}.", nameof(GetAllDocuments), DateTime.UtcNow);
            return Ok(documentsResponses);
        }
    }
}
