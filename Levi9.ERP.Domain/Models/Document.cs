using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Domain.Models
{
    public class Document
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        [StringLength(18), MinLength(18)]
        public string LastUpdate { get; set; }
        [StringLength(20)]
        public string DocumentType { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<ProductDocument> ProductDocuments { get; set; }
    }
}
