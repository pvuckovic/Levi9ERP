using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class PriceRequest
    {
        [Required]
        public int PriceListId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        [StringLength(3), MinLength(3)]
        [RegularExpression("RSD|EUR|GBP|USD|RMB|INR|JPY", ErrorMessage = "Currency must be one of RSD, EUR, GBP, USD, RMB, INR, JPY")]
        public string Currency { get; set; }
    }
}
