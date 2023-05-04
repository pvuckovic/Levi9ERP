using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Model
{
    public class Price
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }
        [Key, Column(Order = 1)]
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
