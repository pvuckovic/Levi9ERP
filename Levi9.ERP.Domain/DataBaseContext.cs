using Levi9.ERP.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Levi9.ERP.Domain
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Price>()
            .HasKey(e => new { e.ProductId, e.PriceListId });

            modelBuilder.Entity<Price>()
                .HasOne(e => e.Product)
                .WithMany(e => e.Prices)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Price>()
                .HasOne(e => e.PriceList)
                .WithMany(e => e.Prices)
                .HasForeignKey(e => e.PriceListId);

            modelBuilder.Entity<ProductDocument>()
            .HasKey(e => new { e.ProductId, e.DocumentId });

            modelBuilder.Entity<ProductDocument>()
                .HasOne(e => e.Product)
                .WithMany(e => e.ProductDocuments)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<ProductDocument>()
                .HasOne(e => e.Document)
                .WithMany(e => e.ProductDocuments)
                .HasForeignKey(e => e.DocumentId);

            modelBuilder.Entity<Client>()
                .HasIndex(e => e.Email)
                .IsUnique();
            #region Clients
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 1,
                     GlobalId = new Guid("b8c03b21-adf0-4148-80ac-8852907419b7"),
                     Name = "Test1 Client",
                     Address = "Test1 Address 123",
                     Email = "test1@example.com",
                     Password = "mL+yK0IZ6Tbe5ZgQVme2GWD8ayRg9VtRI897U0LuB0w=",
                     Salt = "ypt+7c/LVq46Eg==",
                     Phone = "0611234567",
                     LastUpdate = "111111111111111111",
                     PriceListId = 1
                 });
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 2,
                     GlobalId = new Guid("1e3b3042-4123-4610-8bb6-51b1282dc768"),
                     Name = "Test2 Client",
                     Address = "Test2 Address 123",
                     Email = "test2@example.com",
                     Password = "MWpg/GsSmNKsvjH0ApaCvOG4C/G4sJb58hudQeRxzEc=",
                     Salt = "aTgpP/3Uf1JULg==",
                     Phone = "0621234567",
                     LastUpdate = "111111111111111111",
                     PriceListId = 2
                 });
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 3,
                     GlobalId = new Guid("5c69a8f5-8eb2-4d89-b21f-7e5e3aa00c08"),
                     Name = "Test3 Client",
                     Address = "Test3 Address 123",
                     Email = "test3@example.com",
                     Password = "uOEE7w8xH/lX2mmKdgb63SxqItyMrLfamMbYx5uSn7Y=",
                     Salt = "boAfkuX2rmOaaA==",
                     Phone = "0631234567",
                     LastUpdate = "111111111111111111",
                     PriceListId = 3
                 });
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 4,
                     GlobalId = new Guid("d3d4e623-e6bc-44e4-98cb-681ef30b1fd4"),
                     Name = "Test4 Client",
                     Address = "Test4 Address 123",
                     Email = "test4@example.com",
                     Password = "Zr9Dm1X54LUOAqAybdKj+96MS98kLfrk7dUSB2kXn6s=",
                     Salt = "ATpzta8FakmUvA==",
                     Phone = "0641234567",
                     LastUpdate = "111111111111111111",
                     PriceListId = 4
                 });
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 5,
                     GlobalId = new Guid("4da162c6-1af1-422e-be9c-debec2c62b54"),
                     Name = "Test5 Client",
                     Address = "Test5 Address 123",
                     Email = "test5@example.com",
                     Password = "PylZhPreW4GCLJ5zPs19QaVFqH+clUKonOGy+R3s+X8=",
                     Salt = "r6H61KFl8j041w==",
                     Phone = "0651234567",
                     LastUpdate = "111111111111111111",
                     PriceListId = 5
                 });
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 6,
                     GlobalId = new Guid("e4d4fc68-0fa9-4356-b68f-886342655080"),
                     Name = "Test6 Client",
                     Address = "Test6 Address 123",
                     Email = "test6@example.com",
                     Password = "kgrSYNIZgqg2NKyxe2xBgmiKmQBp/cUiprmFuxyqCnk=",
                     Salt = "QX8u19aR8y3QmQ==",
                     Phone = "0661234567",
                     LastUpdate = "111111111111111111",
                     PriceListId = 6
                 });
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 7,
                     GlobalId = new Guid("4470cd66-a960-4abc-ab54-45d92a700e80"),
                     Name = "Test7 Client",
                     Address = "Test7 Address 123",
                     Email = "test7@example.com",
                     Password = "uN1+O4ywbrGcKAVjKCXfCILz8K57UW+0fLzz/G+pMOg=",
                     Salt = "qWurv2IcifFGJQ==",
                     Phone = "0671234567",
                     LastUpdate = "111111111111111111",
                     PriceListId = 7
                 });
            #endregion

            #region Documents
            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    Id = 1,
                    GlobalId = new Guid("494ad822-8ee2-47c3-938f-2de7a43db41a"),
                    LastUpdate = "634792557112051692",
                    DocumentType = "INVOICE",
                    ClientId = 1
                }
               );
            modelBuilder.Entity<ProductDocument>().HasData(
               new ProductDocument
               {
                   PriceValue = 12,
                   Quantity = 11,
                   Currency = "USD",
                   ProductId = 1,
                   DocumentId = 1,
               }
              );
            #endregion

            #region Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    GlobalId = new Guid("6f68f25e-9645-4da9-b66f-8edbebb8a6e7"),
                    Name = "T-ShirtBlack",
                    ImageUrl = "images/tshirtimage/T-ShirtBlack.png",
                    AvailableQuantity = 104,
                    LastUpdate = "111111111111111111",
                }
               );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 2,
                    GlobalId = new Guid("d68bce12-9f03-4e5f-b0b9-3ad1205afda4"),
                    Name = "T-ShirtBlue",
                    ImageUrl = "images/tshirtimage/T-ShirtBlue.png",
                    AvailableQuantity = 138,
                    LastUpdate = "111111111111111111",
                }
                );
            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 3,
                   GlobalId = new Guid("2c103912-7b0f-4469-8769-3989a4d62bc2"),
                   Name = "T-ShirtBrown",
                   ImageUrl = "images/tshirtimage/T-ShirtBrown.png",
                   AvailableQuantity = 745,
                   LastUpdate = "111111111111111111",
               }
               );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 4,
                    GlobalId = new Guid("a6c74b0b-b1f3-4e12-b6d2-6c38a3903ce6"),
                    Name = "T-ShirtGreen",
                    ImageUrl = "images/tshirtimage/T-ShirtGreen.png",
                    AvailableQuantity = 5,
                    LastUpdate = "111111111111111111",
                }
                );
            modelBuilder.Entity<Product>().HasData(
             new Product
             {
                 Id = 5,
                 GlobalId = new Guid("8dbff2d4-42a0-4fe7-9864-05571f3c22db"),
                 Name = "T-ShirtPurple",
                 ImageUrl = "images/tshirtimage/T-ShirtPurple.png",
                 AvailableQuantity = 57,
                 LastUpdate = "111111111111111111",
             }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 6,
                    GlobalId = new Guid("9f7c8b37-0e37-49ed-af1f-4e6a7428a873"),
                    Name = "T-ShirtRed",
                    ImageUrl = "images/tshirtimage/T-ShirtRed.png",
                    AvailableQuantity = 224,
                    LastUpdate = "111111111111111111",
                }
                );
            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 7,
                   GlobalId = new Guid("e0a01813-5f43-4a15-934f-9946ef8f4182"),
                   Name = "T-ShirtRose",
                   ImageUrl = "images/tshirtimage/T-ShirtRose.png",
                   AvailableQuantity = 150,
                   LastUpdate = "111111111111111111",
               }
               );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 8,
                    GlobalId = new Guid("156fd903-9fd2-41af-9b09-600d598b31c4"),
                    Name = "T-ShirtYellow",
                    ImageUrl = "images/tshirtimage/T-ShirtYellow.png",
                    AvailableQuantity = 186,
                    LastUpdate = "111111111111111111",
                }
                );

            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 9,
                   GlobalId = new Guid("6f8799a7-097b-45f4-a8f0-805784ee24a6"),
                   Name = "T-ShirtBlack 9 Gradient",
                   ImageUrl = "images/tshirtimage/T-ShirtBlack9gradient.png",
                   AvailableQuantity = 104,
                   LastUpdate = "111111111111111111",
               }
              );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 10,
                    GlobalId = new Guid("49674251-6b45-437f-8c7a-34a0b1a34c01"),
                    Name = "T-ShirtBlue 9 Gradient",
                    ImageUrl = "images/tshirtimage/T-ShirtBlue9gradient.png",
                    AvailableQuantity = 138,
                    LastUpdate = "111111111111111111",
                }
                );
            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 11,
                   GlobalId = new Guid("7b5ae8b9-12a0-4a19-9c37-943bc4cc5a3e"),
                   Name = "T-ShirtBrown 9 Gradient",
                   ImageUrl = "images/tshirtimage/T-ShirtBrown9gradient.png",
                   AvailableQuantity = 745,
                   LastUpdate = "111111111111111111",
               }
               );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 12,
                    GlobalId = new Guid("097dbb41-2aa0-4fbf-8986-bdf8e4e0d4b9"),
                    Name = "T-ShirtGreen 9 Gradient",
                    ImageUrl = "images/tshirtimage/T-ShirtGreen9gradient.png",
                    AvailableQuantity = 5,
                    LastUpdate = "111111111111111111",
                }
                );
            modelBuilder.Entity<Product>().HasData(
             new Product
             {
                 Id = 13,
                 GlobalId = new Guid("5f8d15b2-4c4d-4e7a-8902-d78d3b4cb10b"),
                 Name = "T-ShirtPurple 9 Gradient",
                 ImageUrl = "images/tshirtimage/T-ShirtPurple9gradient.png",
                 AvailableQuantity = 57,
                 LastUpdate = "111111111111111111",
             }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 14,
                    GlobalId = new Guid("4d50363a-2dc4-4ef0-8bde-682758f0f801"),
                    Name = "T-ShirtRed 9 Gradient",
                    ImageUrl = "images/tshirtimage/T-ShirtRed9gradient.png",
                    AvailableQuantity = 224,
                    LastUpdate = "111111111111111111",
                }
                );
            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 15,
                   GlobalId = new Guid("1835ee0e-3275-4ff9-9d79-1a2e3b5e15e3"),
                   Name = "T-ShirtRose 9 Gradient",
                   ImageUrl = "images/tshirtimage/T-ShirtRose9gradient.png",
                   AvailableQuantity = 150,
                   LastUpdate = "111111111111111111",
               }
               );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 16,
                    GlobalId = new Guid("e5e4a551-2b64-43fc-86e1-4c1c0f6880fe"),
                    Name = "T-ShirtYellow 9 Gradient",
                    ImageUrl = "images/tshirtimage/T-ShirtYellow9gradient.png",
                    AvailableQuantity = 186,
                    LastUpdate = "111111111111111111",
                }
                );
            #endregion

            #region Price
            // List of product IDs
            int[] productIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int[] priceListIds = { 1, 2, 3, 4, 5, 6, 7 };
            float[] productRSDPrice = 
            {
                9750.25f,
                4121.50f,
                6383.75f,
                8925.00f,
                5241.00f,
                7568.50f,
                1953.75f,
                6546.50f,
                4037.00f,
                8614.50f,
                1386.50f,
                5970.50f,
                3177.25f,
                9264.75f,
                7812.00f,
                2985.25f
            };

            foreach (int productId in productIds)
            {
                foreach (int priceListId in priceListIds)
                {
                    string currency = GetCurrencyForPriceList(priceListId);
                    float convertedPrice = ConvertToCurrency((float)productRSDPrice[productId-1], currency);

                    modelBuilder.Entity<Price>().HasData(
                        new Price
                        {
                            GlobalId = new Guid(),
                            PriceValue = (float)Math.Round(convertedPrice, 2),
                            Currency = currency,
                            LastUpdate = DateTime.Now.ToFileTimeUtc().ToString(),
                            ProductId = productId,
                            PriceListId = priceListId
                        }
                    ); ;
                }
            }
            #region Price Functionalities
            string GetCurrencyForPriceList(int priceListId)
            {
                switch (priceListId)
                {
                    case 1:
                        return CurrencyType.RSD.ToString();
                    case 2:
                        return CurrencyType.EUR.ToString();
                    case 3:
                        return CurrencyType.GBP.ToString();
                    case 4:
                        return CurrencyType.USD.ToString();
                    case 5:
                        return CurrencyType.RMB.ToString();
                    case 6:
                        return CurrencyType.INR.ToString();
                    case 7:
                        return CurrencyType.JPY.ToString();
                    default:
                        return string.Empty;
                }
            }
            float ConvertToCurrency(float price, string currency)
            {
                float exchangeRate = 1.0f; // Default exchange rate if currency is not found
                switch (currency)
                {
                    case "RSD":
                        exchangeRate = 1.0f; // 1 RSD = 1 RSD
                        break;
                    case "EUR":
                        exchangeRate = 0.0085f; // 1 RSD = 0.0085 EUR
                        break;
                    case "GBP":
                        exchangeRate = 0.0072f; // 1 RSD = 0.0072 GBP
                        break;
                    case "USD":
                        exchangeRate = 0.0097f; // 1 RSD = 0.0097 USD
                        break;
                    case "RMB":
                        exchangeRate = 0.0623f; // 1 RSD = 0.0623 RMB
                        break;
                    case "INR":
                        exchangeRate = 0.71f; // 1 RSD = 0.71 INR
                        break;
                    case "JPY":
                        exchangeRate = 1.05f; // 1 RSD = 1.05 JPY
                        break;
                }
                return price * exchangeRate;
            }
            #endregion
            #endregion

            #region PriceList
            modelBuilder.Entity<PriceList>().HasData(
                 new PriceList
                 {
                     Id = 1,
                     GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a43db411"),
                     Name = "RSD Price List",
                     LastUpdate = "634792557112051690"
                 }
                );
            modelBuilder.Entity<PriceList>().HasData(
               new PriceList
               {
                   Id = 2,
                   GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a13db412"),
                   Name = "EUR Price List",
                   LastUpdate = "634792557112051691"
               }
               );
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 3,
                    GlobalId = new Guid("494b7014-8ee2-47c3-938f-2de7a43db413"),
                    Name = "GBP Price List",
                    LastUpdate = "634792557112051692"
                }
                );

            modelBuilder.Entity<PriceList>().HasData(
               new PriceList
               {
                   Id = 4,
                   GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a43db414"),
                   Name = "USD Price List",
                   LastUpdate = "634792557112051693"
               }
              );
            modelBuilder.Entity<PriceList>().HasData(
               new PriceList
               {
                   Id = 5,
                   GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a13db415"),
                   Name = "RMB Price List",
                   LastUpdate = "634792557112051694"
               }
               );
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 6,
                    GlobalId = new Guid("494b7014-8ee2-47c3-938f-2de7a43db416"),
                    Name = "INR Price List",
                    LastUpdate = "634792557112051695"
                }
                );

            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 7,
                    GlobalId = new Guid("494b7014-8ee2-47c3-938f-2de7a43db41a"),
                    Name = "JPY Price List",
                    LastUpdate = "634792557112051696"
                }
                );
            #endregion
        }
    }
}