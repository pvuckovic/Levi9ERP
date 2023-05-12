using Levi9.ERP.Domain.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class DocumentRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ClientId cannot be 0!.")]
        public int ClientId { get; set; }
        [Required]
        [RegularExpression("^(INVOICE|RECEIPTS|PURCHASE)$", ErrorMessage = "The value of Document type can be: INVOICE, RECEIPTS, or PURCHASE.")]
        public string DocumentType { get; set; }
        [Required]
        public List<DocumentItemDTO> Items { get; set; }

    }
}
