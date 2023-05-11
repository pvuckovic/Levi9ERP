using Levi9.ERP.Domain.Models;
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
        public CurrencyType Currency { get; set; }
    }
}
