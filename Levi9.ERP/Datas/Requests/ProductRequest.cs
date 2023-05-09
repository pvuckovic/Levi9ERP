using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Data.Requests
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
