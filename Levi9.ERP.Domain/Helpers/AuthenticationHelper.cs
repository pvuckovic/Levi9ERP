﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Levi9.ERP.Domain.Helpers
{
    public static class AuthenticationHelper
    {
        public static string GenerateRandomSalt(int length = 10)
        {
            string randomString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(length));
            return new string(randomString);
        }
        public static string HashPassword(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
        public static bool ValidateUser(string userPassword, string salt, string loginPassword)
        {
            loginPassword = HashPassword(loginPassword, salt);
            return userPassword == loginPassword;
        }

        public static string GenerateJwt(JwtOptions jwtOptions)
        {
            var securityKey = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
            var symetricKey = new SymmetricSecurityKey(securityKey);
            var signingCredentials = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                                            issuer: jwtOptions.Issuer,
                                            audience: jwtOptions.Audience,
                                            expires: DateTime.Now.Add(new TimeSpan(jwtOptions.ExpirationSeconds)),
                                            signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
