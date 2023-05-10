using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class PriceRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int PriceListId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int ProductId { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter a positive value")]
        public float Price { get; set; }
        [Required]
        [StringLength(3), MinLength(3)]
        [RegularExpression("RSD|EUR|GBP|USD|RMB|INR|JPY", ErrorMessage = "Currency must be one of RSD, EUR, GBP, USD, RMB, INR, JPY")]
        public string Currency { get; set; }
    }
}
