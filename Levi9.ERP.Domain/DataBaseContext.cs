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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4mbO0YJ-ovSlqOuXjgGPR2IsjN8sCk9ULksyzIkvLEJeRyYnqKUxHmuxppx-HMcp6oNzzOrMc6_6YRgmiaeA5_vxCRWCV9ARd_GK6hPLdp3tEcsgR7Us9uzAfvH331KiJrqlwCGiOPFDi_OsHLznB3tqbTk-bFfQIqQdyTkno03JxvSOQ8vwEjvCFJUa0Bx7IUBzg4bU_qzf3qpgZMeYI1y1rt9qCldOaIi3WgyU-E5J0?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4m-kRT2GTNRM1fORXdHbHlvpzF8ne8_HsD3AoL-ytcD5vCq2S7olsPp-Nnw017SkIRz6yUxkIym7En4FjUFiENJ2BVJN0TWz9aqDHht8mlh5RrTwQ-ZBXD6Lm8fI9cy390BzcPCGZchRsm6dGkmTU3q3xZE-WU1VNKV-64xUJN02ofGt6w7I7vJxqaeaHh15q_ul04x3dCUumC53DWniI5oEilVornGdGyddxKc8xZoqw?encodeFailures=1&width=500&height=500",
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
                   ImageUrl = "https://db3pap003files.storage.live.com/y4mAsciDm3-1F9piIJKD-IzaefrUBqEbA6CPHpORsvcIBh6PK8Ze9TOhMEvKpBx0RhBUTOIP5AItxqcmLSMiYSz2g7KV8XG0jugLvNtkqC8R1DwnwOQRZHj3csgnd_iTL1qWPfzolxTny7naR0izqMtG3i-w3ziMWHQuEqzwiTc98QTHj8LfkRuMHhv72ZNttAQuoj3-Y2iHokZ4JsuU5DkNfzVpvAKZ4w5wgS5V99rOD8?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4mIMtsMKg4MteMPQCPtJgs6K1S-d2um3XXpYS8Du6_NWnd4lNDiHdlq6Eb524q2jskw_8_vshuYV5u-K6AO8mxW2llKQxnBEL01m5t-JNBM5LecnrYtGho2DiJipC6x4r3jD1VeW34xPSR1p_juV-1oqWdrrQUf8PbaDdD75jE3ar1KeD9yUlPhM5BUrCTjzaJusBxqVk0smMVMzQ7Bja62BV6-4jQ0W743Q1JiUNeOPA?encodeFailures=1&width=500&height=500",
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
                 ImageUrl = "https://db3pap003files.storage.live.com/y4m0eG0CddqsLAfGnFHIiySLlbn9TzPQ9pQOm7YcP9QXblbZJauurhybDbiJTbA4xb-obY2cjRUlaacpZakO8zQ1oDTZQrDaipf0vU9FiylYlxK64_CzrAuGrJenGE98iclH6BN9uWZH3k1V5XthDTzldVwtY2lvP_upoU-UQyNGfX0goTCeF3FnK7rHh5pwIPSQWwSeHNPserQ6skA9FWR0ehISp1axZbphMw6aYmQwhU?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4mLQ8P5MJROOmihR3K_1SU4Y_Iisgc0y1DeC-ZizOELnj82YZeiGhXEY8cavf9xNTC12wtRKmUN42mQysBXyV066A8XFzE9XNScQNdflZhIhCKM0xUmgKHlCG6P9z3jIiqdxENbwxFU6dBr1UEpJpaZXWRHliG8nJNI1TttSsDw533HGJR5gCO9hpdlDDz4W0ltkmYE_oPT24JKDwnLNZyHZv6dfzI-qqzOrlBa5TnPQQ?encodeFailures=1&width=500&height=500",
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
                   ImageUrl = "https://db3pap003files.storage.live.com/y4m0BTJrLTQNPzne4u5bX6n47paz88nldiOZnxawfhv4oJKFk8fP4TZR91HGDs43pSIw2wEXKbJq3cJ8pItmZ-tu9LSPBEpjbTo5jIuiMsFipvDwkZoT4r9C0DHCYD6vVjnRdGP0OgbxKBKGgtOXUlc3hMb3KalQVb5xlK1EC_ukMs5cLmBeHqGzEmI2SBe3zCyeoNgaf3GpysUv9NAcoG1ppL0FCXfJMJbM0zWUbcSfJ0?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4m8PzFIhNEKewi3wbogTAR2veLJqFbaOBgSNlzytwuowu0Lv2SIQxwPhsPe9EFmuc0N8lgEdowWmm29Cavcz6isxa4cpPVOr6jtl9BW5Ajv4lcvKSE4oXqNIMeElwiw53q6Bg84U_VG1c3ntS0UCXSMdo4iRDZJURcBbeIDv7GFCnRKcgMHQ6FD1md_owl3q55SGzBIEsFOpyMku_CcvDb77ttqpDhJ4YPF9CCbzQ8esk?encodeFailures=1&width=500&height=500",
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
                   ImageUrl = "https://db3pap003files.storage.live.com/y4mcROrUsGJ1C_DaPGGRsunJPAdSjzSLWBSqcDa4cHQt2VRY9AZqbHg4T7EPdSV1btrGwXH7VAJuf61Ld1knMXgnsRqdUuLpQkQCtWFYohlEv9VAK3QZzHC6XaVQNugbmcDj03yOM09WghkBH2eDm_EV5MF8iXSjIOwAktBC9Qet-CY16VN2MSm7UFFgw94gEbasO_GBbkRvzfCwOmUcXICRMopHO6obWAB_ATRGjlLHO4?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4myPaEj8lwOAp9D6FSSd6vqVOshe2DZNPeTkOqpYexCSHAtxzO73MHXGGEkScMtu8k1iYqm2lKRBn5DMz5f0zerCAOvzrd8pGwL_fmarBVTMSmopv0Vy1YV5FDpu1-hPgVG0zKhYbu0jy8x2n4_ZOpZTDjzcrwrJrE78DuISNhBLvF2SGidifBduSxqFfXd7DPF_2Gr4dIMcymuT-OBT2opZWcpARi9ccxq-vfz-KPnqA?encodeFailures=1&width=500&height=500",
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
                   ImageUrl = "https://db3pap003files.storage.live.com/y4maMJZDwT_Bt_9lYoj4QEBJplPpSpox0xs8mlm5MJZp-e4mC1TTol3ATfgm9TGwDlCcYnXzdhSqaPeqkZJK90tgRR_KQHtctku28zVSo3D8Rf_462QClZiF4CC03XosaZsn0ZWDdq7KhWdYpC8hUpkJQf9bbvjeRgYJfdHr8yqhGO3ENu8lAwvwUIMRs9LaXDIBAaXurelBc7zK18wxVt8opgjC4MvmdhJC0rjqWefNk8?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4mywnSFEOeNPAc3Dv5LG3QKmt8cnkjDcCHmNyhpMMvkFiSJlUvZkss9mSJAYld9eYyTO4ELiz4sMXc2pgLtrqqsjzrTfWHItrt9Vesn1xHDXi7ONiXvdldHGHQwBDoTr5yQhbvShlKJVTfpL0UM67KIKTjDH3c9MqAUc2S8bwTjmai-g_WeOH4BdyJZz41mDWGTS88odSwCfN94gatrXq-Nc4vr96zUOhvoWSJHQPm-rE?encodeFailures=1&width=500&height=500",
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
                 ImageUrl = "https://db3pap003files.storage.live.com/y4mkHeVXwv9fF7d7rmBK_fJ0Ge3c07dLoVpzqCrrF5HIEFDpKXFGkoRnJ9WmXzt1-bsSG3J5gSP8C3Aj5p8GjTS_tnZrGMCj2oNJbYvMRbyJbAzA3Gy7ynLFEdddnq8cNHwwWB4Jh2k8988ufJ7H-p2oe5lr4oQRvshHBzuIRt52RNWaGspRAVow1L551awcDC_BfmD2se_Yg9guDVlDTOMqVvO9PEqlMP3B-3y_I9oIz0?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4mAuqUBcOB4e_6Pkv5WtCBhsOp6xQyB3dhrzYEuJsoMAijFJ6pSGth6esbBkAIU2JpgSDze6vTEC07JiJWAPnyjuL2PsE_bmP1XypvPO3HZP2uIDAThpD-nk4Ews_QeQd0TNNorcNqO6JqaEqIP9OpEEfkLnBcK5Q0FHBI3IxP6octHzAlY5_M-z93ypRdrhwBjEjnmpEBsZ7eeZwORwXxRa1nw2b-TwPSr6F6uZZ2MUs?encodeFailures=1&width=500&height=500",
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
                   ImageUrl = "https://db3pap003files.storage.live.com/y4mUU4G3Dpx9xkjqniNIZeMWz-wr3t18PGCtpoQxOv522aU8IDNVk6-MWFtqONztKzH6BelSIR6NhOXklxW5CEhTct5nyscvSGwHujoHepodUBhtjuXXW2KudArDxA2g4ARoELCaWVCAvdAm46C7BnzDcbXRf4QBFvz-iUG9V-Nm7O1adpQ6gnF96Xkp2iyNrkGkpc0x2hJ06TVqNWDYM7XfrJooKEbFfYcllC7cy17X5E?encodeFailures=1&width=500&height=500",
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
                    ImageUrl = "https://db3pap003files.storage.live.com/y4m9Oo0CoS06pvC58i8iE_2_WfJE-rEuIMGRWZcbdjThze0gTjJBWWtaDGSsnxJmhCweSY3lvNc6ZYxLMV8zlKM5CTJ24-WbT4iLjCtOgYg_Oy3hxxhcZOTXkMefthHQhT2OxtSmUDKkQIf4shsWTIu81eHGEovGq63_UqM-gkXR0uqYZXZLfgmVR9cTgk4Hyt8dmweIrouQ_eGM4L7TS5xP3d74G3moTJ72Lzjv8XLbSo?encodeFailures=1&width=500&height=500",
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
                            GlobalId = Guid.NewGuid(),
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


            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 8,
                    GlobalId = Guid.NewGuid(),
                    Name = "RSD 2 Price List",
                    LastUpdate = "634792557112051697"
                }
               );
            modelBuilder.Entity<PriceList>().HasData(
               new PriceList
               {
                   Id = 9,
                   GlobalId = Guid.NewGuid(),
                   Name = "EUR 2 Price List",
                   LastUpdate = "634792557112051698"
               }
               );
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 10,
                    GlobalId = Guid.NewGuid(),
                    Name = "GBP 2 Price List",
                    LastUpdate = "634792557112051699"
                }
                );

            modelBuilder.Entity<PriceList>().HasData(
               new PriceList
               {
                   Id = 11,
                   GlobalId = Guid.NewGuid(),
                   Name = "USD 2 Price List",
                   LastUpdate = "634792557112051701"
               }
              );
            modelBuilder.Entity<PriceList>().HasData(
               new PriceList
               {
                   Id = 12,
                   GlobalId = Guid.NewGuid(),
                   Name = "RMB 2 Price List",
                   LastUpdate = "634792557112051702"
               }
               );
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 13,
                    GlobalId = Guid.NewGuid(),
                    Name = "INR 2 Price List",
                    LastUpdate = "634792557112051703"
                }
                );

            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 14,
                    GlobalId = Guid.NewGuid(),
                    Name = "JPY 2 Price List",
                    LastUpdate = "634792557112051704"
                }
                );
            #endregion
        }
    }
}