using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Requests
{
    public class ClientRequest
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Address { get; set; }
        [Required, StringLength(150)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required, StringLength(50)]
        public string Phone { get; set; }
        public int PriceListId { get; set; }
    }
}
