using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Levi9.ERP.IntegrationTests
{
    public static class Fixture
    {
        public static string GenerateJwt()
        {
            var securityKey = Encoding.UTF8.GetBytes("some-signing-key-here");
            var symetricKey = new SymmetricSecurityKey(securityKey);
            var signingCredentials = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                                            issuer: "https://localhost:7281",
                                            audience: "https://localhost:7281",
                                            expires: DateTime.Now.Add(new TimeSpan(3600)),
                                            signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}