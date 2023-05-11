using AutoMapper;
using Levi9.ERP.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Document> GetDocumentById(int id)
        {
            var document = await _context.Documents
                .Include(x => x.ProductDocuments)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(d => d.Id == id);
            return document;
        }

        //var product = await _dataBaseContext.Products
        //        .Include(p => p.Prices)
        //            .ThenInclude(p => p.PriceList)
        //        .FirstOrDefaultAsync(p => p.Id == productId);
        //    return product;
    }
}
