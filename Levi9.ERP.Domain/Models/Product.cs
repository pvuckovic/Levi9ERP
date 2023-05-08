using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Domain.Models
{
    [Index(nameof(Product.Name), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }
        [StringLength(18), MinLength(18)]
        public string LastUpdate { get; set; }
        public List<ProductDocument> ProductDocuments { get; set; }
        public List<Price> Prices { get; set; }
    }
}
