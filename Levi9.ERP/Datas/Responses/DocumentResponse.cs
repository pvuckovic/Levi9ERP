using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Datas.Responses
{
    public class DocumentResponse
    {
        public int id { get; set; }
        public Guid GlobalId { get; set; }
        public string LastUpdate { get; set; }
        public int ClientId { get; set; }
        public string DocumentType { get; set; }
        public List<DocumentItemDTO> Items { get; set; }
    }
}
