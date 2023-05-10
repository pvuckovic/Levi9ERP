using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class AuthenticationRequest
    {
        [Required, StringLength(150)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
    }
}
