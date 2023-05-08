using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Domain.Models
{
    public class ProductDocument
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        [Required, Range(0, float.MaxValue)]
        public float PriceValue { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [StringLength(3), MinLength(3)]
        public string Currency { get; set; }
    }
}
