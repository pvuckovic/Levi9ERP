namespace Levi9.ERP.Domain.Models.DTO
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string LastUpdate { get; set; }
        public int ClientId { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<DocumentItemDTO> Items { get; set; }
    }
}
