using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Models.DTO
{
    public class PriceProductDTO
    {
        public int PriceListId { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
    }
}
