using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Domain.Models
{
    public class Price
    {
        public int ProductId { get; set; }
        public int PriceListId { get; set; }
        public Product Product { get; set; }
        public PriceList PriceList { get; set; }
        public Guid GlobalId { get; set; }
        [Required]
        public float PriceValue { get; set; }
        [StringLength(3), MinLength(3)]
        public string Currency { get; set; }
        [StringLength(18), MaxLength(18)]
        public string LastUpdate { get; set; }

    }
}
