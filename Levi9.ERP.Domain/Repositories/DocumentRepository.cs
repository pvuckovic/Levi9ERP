using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Levi9.ERP.Domain.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public DocumentRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DocumentDTO> AddDocument(DocumentDTO documentModel)
        {
            Document documentMap = _mapper.Map<Document>(documentModel);
            var createdDocumentEntity = _context.Documents.Add(documentMap);

            await SaveChanges();
            return _mapper.Map<DocumentDTO>(createdDocumentEntity.Entity);
        }
        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            var document = await _context.Documents
                .Include(x => x.ProductDocuments)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(d => d.Id == id);
            return _mapper.Map<DocumentDTO>(document);
        }
        public async Task<IEnumerable<DocumentDTO>> GetDocumentsByParameters(string name, int page, string orderBy, string direction)
        {
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
            return mappedDocuments;
        }
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
