using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Models.DTO.DocumentDto
{
    public class DocumentItemSyncDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public float PriceValue { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
    }
}
