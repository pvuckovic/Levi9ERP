using AutoMapper;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Levi9.ERP.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IMapper mapper, IDocumentRepository documentRepository)
        {
            _mapper = mapper;
            _documentRepository = documentRepository;
        }

        public async Task<DocumentDTO> GetDocumentById(int id) {
            var document = await _documentRepository.GetDocumentById(id);
            var documentDto = _mapper.Map<DocumentDTO>(document);
            return documentDto;
        }
    }
}
