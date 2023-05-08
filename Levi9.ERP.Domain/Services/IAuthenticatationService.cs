namespace Levi9.ERP.Domain.Services
{
    public interface IAuthenticatationService
    {
        public string GenerateRandomSalt(int length = 10);
        public string HashPassword(string password, string salt);

    }
}
