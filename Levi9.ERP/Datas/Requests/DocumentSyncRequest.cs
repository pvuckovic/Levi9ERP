using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class DocumentSyncRequest
    {
        [Required]
        public Guid GlobalId { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        [RegularExpression("^(INVOICE|RECEIPTS|PURCHASE)$", ErrorMessage = "The value of Document type can be: INVOICE, RECEIPTS, or PURCHASE.")]
        public string DocumentType { get; set; }
        [Required]
        public List<DocumentItemSyncRequest> Items { get; set; }
    }
}
