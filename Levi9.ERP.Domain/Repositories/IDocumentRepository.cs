using Levi9.ERP.Domain.Models;

namespace Levi9.ERP.Domain.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document> GetDocumentById(int id);
    }
}
