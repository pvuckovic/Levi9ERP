using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class ClientSyncRequest
    {
        [Required]
        public Guid GlobalId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Address { get; set; }
        [Required, StringLength(150)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required, StringLength(50)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong phone number")]
        public string Phone { get; set; }
        public int PriceListId { get; set; } = 1;
    }
}
