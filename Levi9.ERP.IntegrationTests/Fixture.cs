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
        public static List<PriceList> GeneratePriceListData()
        {

            var pricelists = new List<PriceList>
            {
                new PriceList
                {
                    Id = 1,
                    GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a43db41a"),
                    Name = "USD Price List",
                    LastUpdate = "634792557112051692",
                    Prices = new List<Price>
                    {
                        new Price
                        {
                            ProductId = 1,
                            PriceListId = 1,
                            Product = new Product
                            {
                                Id = 1,
                                GlobalId = new Guid("494ad824-8ea2-47c3-938f-2de7a43db41a"),
                                Name = "Shirt",
                                ImageUrl = "someurl123344444",
                                AvailableQuantity = 70,
                                LastUpdate = "634792557112051692"
                            },
                            GlobalId = new Guid("494ad824-8ee2-47c3-932f-2de7a43db41a"),
                            PriceValue = 12,
                            Currency = "USD",
                            LastUpdate = "634792557112051692",
                        }
                    }
                },
                new PriceList
                {
                    Id = 2,
                    GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a13db41a"),
                    Name = "EUR Price List",
                    LastUpdate = "634792557112051693",
                    Prices = new List<Price>
                    {
                        new Price
                        {
                            ProductId = 2,
                            PriceListId = 2,
                            Product = new Product
                            {
                                Id = 2,
                                GlobalId = new Guid("24ce0abb-74c1-4721-a103-7534faf5a6f3"),
                                Name = "B T-shirt",
                                ImageUrl = "slika.png",
                                AvailableQuantity = 140,
                                LastUpdate = "143567342843789438",
                            },
                            GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a20db41a"),
                            PriceValue = 30,
                            Currency = "EUR",
                            LastUpdate = "634792557112051693"
                        }
                    }
                },  
                new PriceList
                {
                    Id = 3,
                    GlobalId = new Guid("494b7014-8ee2-47c3-938f-2de7a43db41a"),
                    Name = "RSD Price List",
                    LastUpdate = "634792557112051694",
                    Prices = new List<Price>
                    {
                        new Price
                        {
                            ProductId = 3,
                            PriceListId = 3,
                            Product = new Product
                            {
                                Id = 3,
                                GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de71b3db41a"),
                                Name = "Z T-Shirt",
                                ImageUrl = "slika2.png",
                                AvailableQuantity = 150,
                                LastUpdate = "634792557112051694",
                            },
                            GlobalId = new Guid("492ad82b-8ee2-47c3-938f-2de7a43db41a"),
                            PriceValue = 1500,
                            Currency = "RSD",
                            LastUpdate = "634792557112051694",
                        },
                        new Price 
                        {
                            ProductId = 4,
                            PriceListId = 3,
                            Product = new Product
                            {
                                Id = 4,
                                GlobalId = new Guid("494ad824-8ee2-47c3-126f-2de7a43db41a"),
                                Name = "B T-Shirt",
                                ImageUrl = "slika3.png",
                                AvailableQuantity = 550,
                                LastUpdate = "634792557112051695",
                            },
                            GlobalId = new Guid("494bc224-8ee2-47c3-938f-2de7a43db41a"),
                            PriceValue = 1700,
                            Currency = "RSD",
                            LastUpdate = "634792557112051695"
                        }
                    }
                }
            };
            return pricelists;
        }
    }
}