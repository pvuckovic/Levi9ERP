using System.Security.Cryptography;
using System.Text;

namespace Levi9.ERP.Domain.Services
{
    public class AuthenticationService : IAuthenticatationService
    {
        public string GenerateRandomSalt(int length = 10)
        {
            string randomString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(length));
            return new string(randomString);
        }

        public string HashPassword(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
