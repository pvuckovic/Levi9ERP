using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Address { get; set; }
        [Required, StringLength(150)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Phone { get; set; }
        [StringLength(18), MinLength(18)]
        public string LastUpdate { get; set; }
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }
    }
}
