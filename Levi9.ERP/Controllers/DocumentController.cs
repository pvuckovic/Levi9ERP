using AutoMapper;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Id is null or negative number");

            var document = await _documentService.GetDocumentById(id);
            if (document == null) return NotFound("A document with the same id doesn't exists.");

            var documentResponse = _mapper.Map<DocumentResponse>(document);
            return Ok(documentResponse);
        }
    }
}
