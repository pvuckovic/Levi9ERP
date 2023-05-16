using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
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

        public DocumentController(IDocumentService documentService, IClientService clientService, IProductService productService, IMapper mapper, IUrlHelper urlHelper)
        {
            _documentService = documentService;
            _clientService = clientService;
            _productService = productService;
            _mapper = mapper;
            _urlHelper = urlHelper;

        }
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentRequest document)
        {
            if (await _clientService.GetClientById(document.ClientId) == null)
            {
                return NotFound("Client doesn't exists");
            }

            foreach (var product in document.Items)
            {
                var productbyId = await _productService.GetProductById(product.ProductId);
                if (productbyId == null)
                {
                    return NotFound($"Product: {product.Name} doesn't exists");
                }
            }

            var documentMap = _mapper.Map<DocumentDTO>(document);
            var documentDTO = await _documentService.CreateDocument(documentMap);
            string location = _urlHelper.Action("CreateDocument", "Document", new { documentId = documentDTO.Id }, Request.Scheme);
            return Created(location, _mapper.Map<DocumentResponse>(documentDTO));
        }
        [HttpGet("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetById(int id)
        {
            var document = await _documentService.GetDocumentById(id);
            if (document == null)
            {
                return NotFound("A document with that id doesn't exists");
            }
            var documentResponse = _mapper.Map<DocumentResponse>(document);
            return Ok(documentResponse);
        }
        [HttpGet]
        public async Task<IActionResult> SearchDocuments([FromQuery] SearchDocumentRequest searchParams)
        {
            var mappedParams = _mapper.Map<SearchDocumentDTO>(searchParams);
            var documents = await _documentService.GetDocumentsByParameters(mappedParams);
            if (!documents.Any())
            {
                return NotFound("No documents were found that match the search parameters");
            }

            var responseProducts = new SearchDocumentResponse
            {
                Items = _mapper.Map<IEnumerable<DocumentResponse>>(documents),
                Page = searchParams.Page
            };
            return Ok(responseProducts);
        }


    }
}
