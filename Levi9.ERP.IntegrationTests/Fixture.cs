using Levi9.ERP.Domain.Models;
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

        public static List<Product> GenerateProductData()
        {

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    GlobalId = new Guid("80ee1a50-522a-4813-842f-0a0766a4d71e"),
                    Name = "A-Jacket",
                    ImageUrl = "https://example.com/ajacket-image.jpg",
                    AvailableQuantity = 10,
                    LastUpdate = "143567349243789438",
                    ProductDocuments = new List<ProductDocument>(),
                    Prices = new List<Price>
                    {
                        new Price
                        {
                            ProductId = 1,
                            PriceListId = 1,
                            PriceList = new PriceList
                            {
                                Id = 1,
                                GlobalId = new Guid("dcbbd18e-fb01-44ad-b49a-54ae15479114"),
                                Name = "USD Price List",
                                LastUpdate = "143567349243789438",
                            },
                            GlobalId = new Guid("d085a9a4-cfb5-402a-b8ab-ae1072fbf4c1"),
                            PriceValue = 9.99f,
                            Currency = "USD",
                            LastUpdate = "143567349243789438"
                        }
                    }
                },
                new Product
                {
                    Id = 2,
                    GlobalId = new Guid("4b007376-13ed-4011-97e8-014b6dc5731b"),
                    Name = "B-Jacket",
                    ImageUrl = "https://example.com/bjacket-image.jpg",
                    AvailableQuantity = 100,
                    LastUpdate = "143567342843789438",
                    ProductDocuments = new List<ProductDocument>(),
                    Prices = new List<Price>
                    {
                        new Price
                        {
                            ProductId = 2,
                            PriceListId = 2,
                            PriceList = new PriceList
                            {
                                Id = 2,
                                GlobalId = new Guid("24ce0abb-74c1-4721-a103-7534faf5a6f3"),
                                Name = "USD Price List",
                                LastUpdate = "143567342843789438",
                            },
                            GlobalId = new Guid("e8a867d2-64ec-4371-bc83-7425b274a2f1"),
                            PriceValue = 9.99f,
                            Currency = "USD",
                            LastUpdate = "143567342843789438"
                        }
                    }
                }
            };
            return products;
        }

    }
}