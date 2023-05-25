using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Levi9.ERP.Domain.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentRepository> _logger;

        public DocumentRepository(DataBaseContext context, IMapper mapper, ILogger<DocumentRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<DocumentDTO> AddDocument(DocumentDTO documentModel)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentRepository. Timestamp: {Timestamp}.", nameof(AddDocument), DateTime.UtcNow);
            Document documentMap = _mapper.Map<Document>(documentModel);
            var createdDocumentEntity = _context.Documents.Add(documentMap);
            await SaveChanges();
            _logger.LogInformation("Adding new document in {FunctionName} of DocumentRepository. Timestamp: {Timestamp}.", nameof(AddDocument), DateTime.UtcNow);
            return _mapper.Map<DocumentDTO>(createdDocumentEntity.Entity);
        }

        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentRepository. Timestamp: {Timestamp}.", nameof(GetDocumentById), DateTime.UtcNow);
            var document = await _context.Documents
                .Include(x => x.ProductDocuments)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(d => d.Id == id);
            _logger.LogInformation("Retrieving document in {FunctionName} of DocumentRepository. Timestamp: {Timestamp}.", nameof(GetDocumentById), DateTime.UtcNow);
            return _mapper.Map<DocumentDTO>(document);
        }
        public async Task<IEnumerable<DocumentDTO>> GetDocumentsByParameters(string name, int page, string orderBy, string direction)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentRepository. Timestamp: {Timestamp}.", nameof(GetDocumentsByParameters), DateTime.UtcNow);
            var query = _context.Documents
                   .Include(d => d.Client)
                   .Include(d => d.ProductDocuments).ThenInclude(pd => pd.Product).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.Client.Name.Contains(name) ||
                         d.ProductDocuments.Any(pd => pd.Product.Name.Contains(name)) ||
                         d.DocumentType.Contains(name));
            }
            var orderByMap = new Dictionary<string, Expression<Func<Document, object>>>
            {
                { "id", p => p.Id },
                { "globalId", p => p.GlobalId },
                { "documentType", p => p.DocumentType }
            };

            var orderByExpression = (orderBy == null) ? orderByMap.GetValueOrDefault("globalId", p => p.GlobalId) :
            orderByMap.GetValueOrDefault(orderBy, p => p.GlobalId);
            var sortedQuery = (direction == "ASC") ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);
            var pageSize = 5;
            var skip = (page - 1) * pageSize;
            var documents = await sortedQuery
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            var mappedDocuments = documents.Select(p => _mapper.Map<DocumentDTO>(p));
            _logger.LogInformation("Retrieving documents in {FunctionName} of DocumentRepository. Timestamp: {Timestamp}.", nameof(GetDocumentsByParameters), DateTime.UtcNow);
            return mappedDocuments;
        }


        public async Task<Document> UpdateDocument(DocumentDTO document)
        {
            _logger.LogInformation("Entering {FunctionName} in DocumentRepository. Timestamp: {Timestamp}.", nameof(UpdateDocument), DateTime.UtcNow);
            Document documentMap = _mapper.Map<Document>(document);
            var updatedDocument = await _context.Documents.FirstOrDefaultAsync(d => d.GlobalId == document.GlobalId);
            updatedDocument.LastUpdate = document.LastUpdate;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Retrieving confirmation of updated document in {FunctionName} of DocumentRepository. Timestamp: {Timestamp}.", nameof(UpdateDocument), DateTime.UtcNow);
            return documentMap;
        }

        public async Task<bool> DoesDocumentByGlobalIdExists(Guid globalId)
        {
            _logger.LogInformation("Entering { FunctionName} in DocumentRepository.Timestamp { Timestamp}.", nameof(DoesDocumentByGlobalIdExists), DateTime.UtcNow);
            var result = await _context.Documents.AnyAsync(p => p.GlobalId == globalId);
            _logger.LogInformation("Retrieving confirmation of document with GlobalId { Id} in { FunctionName}of DocumentRepository. Timestamp { Timestamp}.", globalId, nameof(DoesDocumentByGlobalIdExists), DateTime.UtcNow);
            return result;
        }

        public async Task<IEnumerable<DocumentDTO>> GetAllDocuments()
        {
            var query = _context.Documents
                   .Include(d => d.Client)
                   .Include(d => d.ProductDocuments).ThenInclude(pd => pd.Product).AsQueryable();

            _logger.LogInformation("Entering {FunctionName} in DocumentRepository. Timestamp: {Timestamp}.", nameof(GetAllDocuments), DateTime.UtcNow);

            var documents = await query
                .ToListAsync();

            var mappedDocuments = documents.Select(p => _mapper.Map<DocumentDTO>(p));

            _logger.LogInformation("Retrieving all documents in {FunctionName} of DocumentRepository. Timestamp: {Timestamp}.", nameof(GetAllDocuments), DateTime.UtcNow);

            mappedDocuments = documents.Select(p => _mapper.Map<DocumentDTO>(p));
            return mappedDocuments;
        }
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
