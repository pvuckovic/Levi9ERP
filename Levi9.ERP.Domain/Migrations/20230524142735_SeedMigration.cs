using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levi9.ERP.Domain.Migrations
{
    public partial class SeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    PriceListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PriceListId = table.Column<int>(type: "int", nullable: false),
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceValue = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => new { x.ProductId, x.PriceListId });
                    table.ForeignKey(
                        name: "FK_Prices_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDocuments",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    PriceValue = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDocuments", x => new { x.ProductId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_ProductDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDocuments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PriceLists",
                columns: new[] { "Id", "GlobalId", "LastUpdate", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("494ad824-8ee2-47c3-938f-2de7a43db411"), "634792557112051690", "RSD Price List" },
                    { 2, new Guid("494ad824-8ee2-47c3-938f-2de7a13db412"), "634792557112051691", "EUR Price List" },
                    { 3, new Guid("494b7014-8ee2-47c3-938f-2de7a43db413"), "634792557112051692", "GBP Price List" },
                    { 4, new Guid("494ad824-8ee2-47c3-938f-2de7a43db414"), "634792557112051693", "USD Price List" },
                    { 5, new Guid("494ad824-8ee2-47c3-938f-2de7a13db415"), "634792557112051694", "RMB Price List" },
                    { 6, new Guid("494b7014-8ee2-47c3-938f-2de7a43db416"), "634792557112051695", "INR Price List" },
                    { 7, new Guid("494b7014-8ee2-47c3-938f-2de7a43db41a"), "634792557112051696", "JPY Price List" },
                    { 8, new Guid("83a7470b-03db-48da-b5c2-be5173fab193"), "634792557112051697", "RSD 2 Price List" },
                    { 9, new Guid("1454ad88-ca65-41be-9467-1363507a94ab"), "634792557112051698", "EUR 2 Price List" },
                    { 10, new Guid("3114766f-bb82-4b80-bcab-fab12422b526"), "634792557112051699", "GBP 2 Price List" },
                    { 11, new Guid("a48e16ce-af75-49b6-a084-ae4dd9f1372f"), "634792557112051701", "USD 2 Price List" },
                    { 12, new Guid("4c520af1-6d25-41c9-9876-7459fc7ca051"), "634792557112051702", "RMB 2 Price List" },
                    { 13, new Guid("6a1f457f-8427-4e1f-b564-8006dea1d006"), "634792557112051703", "INR 2 Price List" },
                    { 14, new Guid("dd4f671a-a42a-44aa-8d18-33ae9d34047e"), "634792557112051704", "JPY 2 Price List" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "GlobalId", "ImageUrl", "LastUpdate", "Name" },
                values: new object[,]
                {
                    { 1, 104, new Guid("6f68f25e-9645-4da9-b66f-8edbebb8a6e7"), "https://db3pap003files.storage.live.com/y4mbO0YJ-ovSlqOuXjgGPR2IsjN8sCk9ULksyzIkvLEJeRyYnqKUxHmuxppx-HMcp6oNzzOrMc6_6YRgmiaeA5_vxCRWCV9ARd_GK6hPLdp3tEcsgR7Us9uzAfvH331KiJrqlwCGiOPFDi_OsHLznB3tqbTk-bFfQIqQdyTkno03JxvSOQ8vwEjvCFJUa0Bx7IUBzg4bU_qzf3qpgZMeYI1y1rt9qCldOaIi3WgyU-E5J0?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtBlack" },
                    { 2, 138, new Guid("d68bce12-9f03-4e5f-b0b9-3ad1205afda4"), "https://db3pap003files.storage.live.com/y4m-kRT2GTNRM1fORXdHbHlvpzF8ne8_HsD3AoL-ytcD5vCq2S7olsPp-Nnw017SkIRz6yUxkIym7En4FjUFiENJ2BVJN0TWz9aqDHht8mlh5RrTwQ-ZBXD6Lm8fI9cy390BzcPCGZchRsm6dGkmTU3q3xZE-WU1VNKV-64xUJN02ofGt6w7I7vJxqaeaHh15q_ul04x3dCUumC53DWniI5oEilVornGdGyddxKc8xZoqw?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtBlue" },
                    { 3, 745, new Guid("2c103912-7b0f-4469-8769-3989a4d62bc2"), "https://db3pap003files.storage.live.com/y4mAsciDm3-1F9piIJKD-IzaefrUBqEbA6CPHpORsvcIBh6PK8Ze9TOhMEvKpBx0RhBUTOIP5AItxqcmLSMiYSz2g7KV8XG0jugLvNtkqC8R1DwnwOQRZHj3csgnd_iTL1qWPfzolxTny7naR0izqMtG3i-w3ziMWHQuEqzwiTc98QTHj8LfkRuMHhv72ZNttAQuoj3-Y2iHokZ4JsuU5DkNfzVpvAKZ4w5wgS5V99rOD8?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtBrown" },
                    { 4, 5, new Guid("a6c74b0b-b1f3-4e12-b6d2-6c38a3903ce6"), "https://db3pap003files.storage.live.com/y4mIMtsMKg4MteMPQCPtJgs6K1S-d2um3XXpYS8Du6_NWnd4lNDiHdlq6Eb524q2jskw_8_vshuYV5u-K6AO8mxW2llKQxnBEL01m5t-JNBM5LecnrYtGho2DiJipC6x4r3jD1VeW34xPSR1p_juV-1oqWdrrQUf8PbaDdD75jE3ar1KeD9yUlPhM5BUrCTjzaJusBxqVk0smMVMzQ7Bja62BV6-4jQ0W743Q1JiUNeOPA?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtGreen" },
                    { 5, 57, new Guid("8dbff2d4-42a0-4fe7-9864-05571f3c22db"), "https://db3pap003files.storage.live.com/y4m0eG0CddqsLAfGnFHIiySLlbn9TzPQ9pQOm7YcP9QXblbZJauurhybDbiJTbA4xb-obY2cjRUlaacpZakO8zQ1oDTZQrDaipf0vU9FiylYlxK64_CzrAuGrJenGE98iclH6BN9uWZH3k1V5XthDTzldVwtY2lvP_upoU-UQyNGfX0goTCeF3FnK7rHh5pwIPSQWwSeHNPserQ6skA9FWR0ehISp1axZbphMw6aYmQwhU?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtPurple" },
                    { 6, 224, new Guid("9f7c8b37-0e37-49ed-af1f-4e6a7428a873"), "https://db3pap003files.storage.live.com/y4mLQ8P5MJROOmihR3K_1SU4Y_Iisgc0y1DeC-ZizOELnj82YZeiGhXEY8cavf9xNTC12wtRKmUN42mQysBXyV066A8XFzE9XNScQNdflZhIhCKM0xUmgKHlCG6P9z3jIiqdxENbwxFU6dBr1UEpJpaZXWRHliG8nJNI1TttSsDw533HGJR5gCO9hpdlDDz4W0ltkmYE_oPT24JKDwnLNZyHZv6dfzI-qqzOrlBa5TnPQQ?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtRed" },
                    { 7, 150, new Guid("e0a01813-5f43-4a15-934f-9946ef8f4182"), "https://db3pap003files.storage.live.com/y4m0BTJrLTQNPzne4u5bX6n47paz88nldiOZnxawfhv4oJKFk8fP4TZR91HGDs43pSIw2wEXKbJq3cJ8pItmZ-tu9LSPBEpjbTo5jIuiMsFipvDwkZoT4r9C0DHCYD6vVjnRdGP0OgbxKBKGgtOXUlc3hMb3KalQVb5xlK1EC_ukMs5cLmBeHqGzEmI2SBe3zCyeoNgaf3GpysUv9NAcoG1ppL0FCXfJMJbM0zWUbcSfJ0?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtRose" },
                    { 8, 186, new Guid("156fd903-9fd2-41af-9b09-600d598b31c4"), "https://db3pap003files.storage.live.com/y4m8PzFIhNEKewi3wbogTAR2veLJqFbaOBgSNlzytwuowu0Lv2SIQxwPhsPe9EFmuc0N8lgEdowWmm29Cavcz6isxa4cpPVOr6jtl9BW5Ajv4lcvKSE4oXqNIMeElwiw53q6Bg84U_VG1c3ntS0UCXSMdo4iRDZJURcBbeIDv7GFCnRKcgMHQ6FD1md_owl3q55SGzBIEsFOpyMku_CcvDb77ttqpDhJ4YPF9CCbzQ8esk?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtYellow" },
                    { 9, 104, new Guid("6f8799a7-097b-45f4-a8f0-805784ee24a6"), "https://db3pap003files.storage.live.com/y4mcROrUsGJ1C_DaPGGRsunJPAdSjzSLWBSqcDa4cHQt2VRY9AZqbHg4T7EPdSV1btrGwXH7VAJuf61Ld1knMXgnsRqdUuLpQkQCtWFYohlEv9VAK3QZzHC6XaVQNugbmcDj03yOM09WghkBH2eDm_EV5MF8iXSjIOwAktBC9Qet-CY16VN2MSm7UFFgw94gEbasO_GBbkRvzfCwOmUcXICRMopHO6obWAB_ATRGjlLHO4?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtBlack 9 Gradient" },
                    { 10, 138, new Guid("49674251-6b45-437f-8c7a-34a0b1a34c01"), "https://db3pap003files.storage.live.com/y4myPaEj8lwOAp9D6FSSd6vqVOshe2DZNPeTkOqpYexCSHAtxzO73MHXGGEkScMtu8k1iYqm2lKRBn5DMz5f0zerCAOvzrd8pGwL_fmarBVTMSmopv0Vy1YV5FDpu1-hPgVG0zKhYbu0jy8x2n4_ZOpZTDjzcrwrJrE78DuISNhBLvF2SGidifBduSxqFfXd7DPF_2Gr4dIMcymuT-OBT2opZWcpARi9ccxq-vfz-KPnqA?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtBlue 9 Gradient" },
                    { 11, 745, new Guid("7b5ae8b9-12a0-4a19-9c37-943bc4cc5a3e"), "https://db3pap003files.storage.live.com/y4maMJZDwT_Bt_9lYoj4QEBJplPpSpox0xs8mlm5MJZp-e4mC1TTol3ATfgm9TGwDlCcYnXzdhSqaPeqkZJK90tgRR_KQHtctku28zVSo3D8Rf_462QClZiF4CC03XosaZsn0ZWDdq7KhWdYpC8hUpkJQf9bbvjeRgYJfdHr8yqhGO3ENu8lAwvwUIMRs9LaXDIBAaXurelBc7zK18wxVt8opgjC4MvmdhJC0rjqWefNk8?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtBrown 9 Gradient" },
                    { 12, 5, new Guid("097dbb41-2aa0-4fbf-8986-bdf8e4e0d4b9"), "https://db3pap003files.storage.live.com/y4mywnSFEOeNPAc3Dv5LG3QKmt8cnkjDcCHmNyhpMMvkFiSJlUvZkss9mSJAYld9eYyTO4ELiz4sMXc2pgLtrqqsjzrTfWHItrt9Vesn1xHDXi7ONiXvdldHGHQwBDoTr5yQhbvShlKJVTfpL0UM67KIKTjDH3c9MqAUc2S8bwTjmai-g_WeOH4BdyJZz41mDWGTS88odSwCfN94gatrXq-Nc4vr96zUOhvoWSJHQPm-rE?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtGreen 9 Gradient" },
                    { 13, 57, new Guid("5f8d15b2-4c4d-4e7a-8902-d78d3b4cb10b"), "https://db3pap003files.storage.live.com/y4mkHeVXwv9fF7d7rmBK_fJ0Ge3c07dLoVpzqCrrF5HIEFDpKXFGkoRnJ9WmXzt1-bsSG3J5gSP8C3Aj5p8GjTS_tnZrGMCj2oNJbYvMRbyJbAzA3Gy7ynLFEdddnq8cNHwwWB4Jh2k8988ufJ7H-p2oe5lr4oQRvshHBzuIRt52RNWaGspRAVow1L551awcDC_BfmD2se_Yg9guDVlDTOMqVvO9PEqlMP3B-3y_I9oIz0?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtPurple 9 Gradient" },
                    { 14, 224, new Guid("4d50363a-2dc4-4ef0-8bde-682758f0f801"), "https://db3pap003files.storage.live.com/y4mAuqUBcOB4e_6Pkv5WtCBhsOp6xQyB3dhrzYEuJsoMAijFJ6pSGth6esbBkAIU2JpgSDze6vTEC07JiJWAPnyjuL2PsE_bmP1XypvPO3HZP2uIDAThpD-nk4Ews_QeQd0TNNorcNqO6JqaEqIP9OpEEfkLnBcK5Q0FHBI3IxP6octHzAlY5_M-z93ypRdrhwBjEjnmpEBsZ7eeZwORwXxRa1nw2b-TwPSr6F6uZZ2MUs?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtRed 9 Gradient" },
                    { 15, 150, new Guid("1835ee0e-3275-4ff9-9d79-1a2e3b5e15e3"), "https://db3pap003files.storage.live.com/y4mUU4G3Dpx9xkjqniNIZeMWz-wr3t18PGCtpoQxOv522aU8IDNVk6-MWFtqONztKzH6BelSIR6NhOXklxW5CEhTct5nyscvSGwHujoHepodUBhtjuXXW2KudArDxA2g4ARoELCaWVCAvdAm46C7BnzDcbXRf4QBFvz-iUG9V-Nm7O1adpQ6gnF96Xkp2iyNrkGkpc0x2hJ06TVqNWDYM7XfrJooKEbFfYcllC7cy17X5E?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtRose 9 Gradient" },
                    { 16, 186, new Guid("e5e4a551-2b64-43fc-86e1-4c1c0f6880fe"), "https://db3pap003files.storage.live.com/y4m9Oo0CoS06pvC58i8iE_2_WfJE-rEuIMGRWZcbdjThze0gTjJBWWtaDGSsnxJmhCweSY3lvNc6ZYxLMV8zlKM5CTJ24-WbT4iLjCtOgYg_Oy3hxxhcZOTXkMefthHQhT2OxtSmUDKkQIf4shsWTIu81eHGEovGq63_UqM-gkXR0uqYZXZLfgmVR9cTgk4Hyt8dmweIrouQ_eGM4L7TS5xP3d74G3moTJ72Lzjv8XLbSo?encodeFailures=1&width=500&height=500", "111111111111111111", "T-ShirtYellow 9 Gradient" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Email", "GlobalId", "LastUpdate", "Name", "Password", "Phone", "PriceListId", "Salt" },
                values: new object[,]
                {
                    { 1, "Test1 Address 123", "test1@example.com", new Guid("b8c03b21-adf0-4148-80ac-8852907419b7"), "111111111111111111", "Test1 Client", "mL+yK0IZ6Tbe5ZgQVme2GWD8ayRg9VtRI897U0LuB0w=", "0611234567", 1, "ypt+7c/LVq46Eg==" },
                    { 2, "Test2 Address 123", "test2@example.com", new Guid("1e3b3042-4123-4610-8bb6-51b1282dc768"), "111111111111111111", "Test2 Client", "MWpg/GsSmNKsvjH0ApaCvOG4C/G4sJb58hudQeRxzEc=", "0621234567", 2, "aTgpP/3Uf1JULg==" },
                    { 3, "Test3 Address 123", "test3@example.com", new Guid("5c69a8f5-8eb2-4d89-b21f-7e5e3aa00c08"), "111111111111111111", "Test3 Client", "uOEE7w8xH/lX2mmKdgb63SxqItyMrLfamMbYx5uSn7Y=", "0631234567", 3, "boAfkuX2rmOaaA==" },
                    { 4, "Test4 Address 123", "test4@example.com", new Guid("d3d4e623-e6bc-44e4-98cb-681ef30b1fd4"), "111111111111111111", "Test4 Client", "Zr9Dm1X54LUOAqAybdKj+96MS98kLfrk7dUSB2kXn6s=", "0641234567", 4, "ATpzta8FakmUvA==" },
                    { 5, "Test5 Address 123", "test5@example.com", new Guid("4da162c6-1af1-422e-be9c-debec2c62b54"), "111111111111111111", "Test5 Client", "PylZhPreW4GCLJ5zPs19QaVFqH+clUKonOGy+R3s+X8=", "0651234567", 5, "r6H61KFl8j041w==" },
                    { 6, "Test6 Address 123", "test6@example.com", new Guid("e4d4fc68-0fa9-4356-b68f-886342655080"), "111111111111111111", "Test6 Client", "kgrSYNIZgqg2NKyxe2xBgmiKmQBp/cUiprmFuxyqCnk=", "0661234567", 6, "QX8u19aR8y3QmQ==" },
                    { 7, "Test7 Address 123", "test7@example.com", new Guid("4470cd66-a960-4abc-ab54-45d92a700e80"), "111111111111111111", "Test7 Client", "uN1+O4ywbrGcKAVjKCXfCILz8K57UW+0fLzz/G+pMOg=", "0671234567", 7, "qWurv2IcifFGJQ==" }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 1, "RSD", new Guid("c7625086-f3ac-4e14-83fe-686103dba898"), "133294120548089189", 9750.25f },
                    { 2, 1, "EUR", new Guid("53764251-15ad-443c-b6b5-c5e633bfb4ca"), "133294120548089274", 82.88f },
                    { 3, 1, "GBP", new Guid("f149b020-8f47-4345-8331-f0717d8fa3d1"), "133294120548089292", 70.2f },
                    { 4, 1, "USD", new Guid("0444ba5b-37ba-4eaa-90c1-ab7cb7edbe68"), "133294120548089309", 94.58f },
                    { 5, 1, "RMB", new Guid("b05e4144-8e4c-485f-9167-116e36fec88f"), "133294120548089325", 607.44f },
                    { 6, 1, "INR", new Guid("4689bb46-1e23-499a-80dd-a26636bd9946"), "133294120548089341", 6922.68f },
                    { 7, 1, "JPY", new Guid("a6e321f0-eb08-4d1c-aee9-d23d21df5111"), "133294120548089364", 10237.76f },
                    { 1, 2, "RSD", new Guid("125cc3bb-31eb-4bf0-8143-5c0de74ab38e"), "133294120548089382", 4121.5f },
                    { 2, 2, "EUR", new Guid("99a3e6b6-75a3-45a5-9503-ba6b40b95ba9"), "133294120548089397", 35.03f },
                    { 3, 2, "GBP", new Guid("617618ad-b47a-472f-b547-5316e071768d"), "133294120548089413", 29.67f },
                    { 4, 2, "USD", new Guid("d5e1bf76-f7a1-4dd7-adba-ad0e3ba0d6b4"), "133294120548089428", 39.98f },
                    { 5, 2, "RMB", new Guid("d7b5d332-ea95-4d6c-b161-1e0ae08084d2"), "133294120548089443", 256.77f },
                    { 6, 2, "INR", new Guid("791e2170-6fdf-4e56-bd76-9f8281bd01bc"), "133294120548089458", 2926.26f },
                    { 7, 2, "JPY", new Guid("79d3d510-3926-4cfa-8b95-08cad56fab7e"), "133294120548089473", 4327.57f },
                    { 1, 3, "RSD", new Guid("1a362e99-12a6-46d8-b034-4f193967d18c"), "133294120548089490", 6383.75f },
                    { 2, 3, "EUR", new Guid("09a5cbf4-44d6-499e-ab9a-048648e101fc"), "133294120548089505", 54.26f },
                    { 3, 3, "GBP", new Guid("07a17286-b850-40a5-abd8-7c158e331df7"), "133294120548089520", 45.96f },
                    { 4, 3, "USD", new Guid("b9a34e31-53e6-4a0a-ba1e-a3b2f8c3d66b"), "133294120548089537", 61.92f },
                    { 5, 3, "RMB", new Guid("65b36c96-977e-4a86-96df-b49c70aa799e"), "133294120548089552", 397.71f },
                    { 6, 3, "INR", new Guid("3672bd4e-4000-4b16-ac26-77ecfc073c84"), "133294120548089567", 4532.46f },
                    { 7, 3, "JPY", new Guid("537fedd9-2e92-4e48-b997-e8bf92453598"), "133294120548089612", 6702.94f },
                    { 1, 4, "RSD", new Guid("0f9cd6f7-eded-43aa-998f-202b3b812356"), "133294120548089630", 8925f },
                    { 2, 4, "EUR", new Guid("24f54f43-f3e5-4bc5-af31-e3b8828e7228"), "133294120548089648", 75.86f },
                    { 3, 4, "GBP", new Guid("7fb3c118-a1e1-4235-bd07-658b88f6f8a6"), "133294120548089663", 64.26f },
                    { 4, 4, "USD", new Guid("07c9f56f-fe15-4c84-a44b-0e7a4fa06b81"), "133294120548089678", 86.57f },
                    { 5, 4, "RMB", new Guid("95e70c48-17bf-4af0-8907-d2ee20333616"), "133294120548089693", 556.03f },
                    { 6, 4, "INR", new Guid("91bf3825-d075-4fd3-b538-4b1bdff44977"), "133294120548089708", 6336.75f },
                    { 7, 4, "JPY", new Guid("3258ce4d-2b62-4a38-9bc0-051149bcbf0e"), "133294120548089723", 9371.25f },
                    { 1, 5, "RSD", new Guid("a069a11b-1917-40b7-9967-2a3648eb56cf"), "133294120548089738", 5241f },
                    { 2, 5, "EUR", new Guid("ce2944e6-84fd-4bdc-87c9-6f0127bc6924"), "133294120548089752", 44.55f },
                    { 3, 5, "GBP", new Guid("2346f9e6-f9d6-4cd6-8e87-49288b14682a"), "133294120548089769", 37.74f },
                    { 4, 5, "USD", new Guid("ac427ee6-398c-4b7d-9cab-b4fb6f19ad10"), "133294120548089784", 50.84f },
                    { 5, 5, "RMB", new Guid("30f08b38-6563-4190-95e2-43dc5fbb8693"), "133294120548089799", 326.51f },
                    { 6, 5, "INR", new Guid("4d3bfecf-6700-4534-ad6f-ba26956d6333"), "133294120548089815", 3721.11f },
                    { 7, 5, "JPY", new Guid("06dd754b-f9ec-4cbf-bfe9-206d377a7a50"), "133294120548089830", 5503.05f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 6, "RSD", new Guid("f1de492f-324c-40bf-84c9-db7d0743b30e"), "133294120548089844", 7568.5f },
                    { 2, 6, "EUR", new Guid("c4d170b8-273a-4803-ac7a-d9587e55097f"), "133294120548089859", 64.33f },
                    { 3, 6, "GBP", new Guid("3857a94b-ed25-4fd0-99fb-218b634f1a3c"), "133294120548089873", 54.49f },
                    { 4, 6, "USD", new Guid("7a1c2a44-96c2-450e-b6f6-ec657648eb02"), "133294120548089890", 73.41f },
                    { 5, 6, "RMB", new Guid("1953a2e5-9986-425c-812d-e153b84eec32"), "133294120548089905", 471.52f },
                    { 6, 6, "INR", new Guid("b318981a-5042-4997-a56c-688aac5cf60b"), "133294120548089919", 5373.63f },
                    { 7, 6, "JPY", new Guid("f27fe3f8-619c-42d8-b82c-32f8628f726c"), "133294120548089963", 7946.92f },
                    { 1, 7, "RSD", new Guid("78a83957-b0d5-4e65-ba64-16e5634462f4"), "133294120548089980", 1953.75f },
                    { 2, 7, "EUR", new Guid("2bce7a6c-1810-43eb-b781-e995f58db26a"), "133294120548089995", 16.61f },
                    { 3, 7, "GBP", new Guid("3a3556fc-b279-4883-b031-aec375dab542"), "133294120548090010", 14.07f },
                    { 4, 7, "USD", new Guid("181be247-a519-407c-b9de-14e9c75eb833"), "133294120548090025", 18.95f },
                    { 5, 7, "RMB", new Guid("e1e454f9-9edc-417c-a7f8-9ad24e9e9e13"), "133294120548090041", 121.72f },
                    { 6, 7, "INR", new Guid("e86caa02-0144-4ecc-90de-abcad19b340b"), "133294120548090056", 1387.16f },
                    { 7, 7, "JPY", new Guid("3a88e44a-d62f-4ded-9fe2-4e8af0e84914"), "133294120548090071", 2051.44f },
                    { 1, 8, "RSD", new Guid("8c4d6aa2-89e2-4197-9031-123369cb67bf"), "133294120548090085", 6546.5f },
                    { 2, 8, "EUR", new Guid("d31c8ef8-7725-4c0f-8155-a0e30c5307c1"), "133294120548090100", 55.65f },
                    { 3, 8, "GBP", new Guid("00f7cac2-92e0-4644-8130-317465fd0ad0"), "133294120548090115", 47.13f },
                    { 4, 8, "USD", new Guid("7a2219b6-0b53-4ec8-a010-17ec7c0ebf92"), "133294120548090129", 63.5f },
                    { 5, 8, "RMB", new Guid("3d860df0-8fa2-4501-8973-627d3c6f0025"), "133294120548090144", 407.85f },
                    { 6, 8, "INR", new Guid("ea91b45c-0aad-4b4c-b8de-3747fa71f4a0"), "133294120548090160", 4648.01f },
                    { 7, 8, "JPY", new Guid("c3b26b98-d24d-48cd-842f-521c1ce2e6f4"), "133294120548090175", 6873.82f },
                    { 1, 9, "RSD", new Guid("3b4c5d85-6d2d-410e-bcbc-2e00a4429a5a"), "133294120548090189", 4037f },
                    { 2, 9, "EUR", new Guid("2f8c5919-9e5c-49d2-bfbb-a4a704ae77ab"), "133294120548090204", 34.31f },
                    { 3, 9, "GBP", new Guid("bfc226b0-13dc-47a6-a1a7-ac5be49b3266"), "133294120548090218", 29.07f },
                    { 4, 9, "USD", new Guid("deaba8f4-324e-4f73-8e79-73ae70d27c43"), "133294120548090232", 39.16f },
                    { 5, 9, "RMB", new Guid("6ef01671-4df2-4717-96c9-cbd5c56d54cc"), "133294120548090247", 251.51f },
                    { 6, 9, "INR", new Guid("a0129533-ed6d-45de-b4ed-2a042563e4c9"), "133294120548090261", 2866.27f },
                    { 7, 9, "JPY", new Guid("8ac47f49-f688-4b74-be34-3aba2a9036ba"), "133294120548090278", 4238.85f },
                    { 1, 10, "RSD", new Guid("990879c5-a46d-436e-a5fe-0e0fd10a2ed6"), "133294120548090341", 8614.5f },
                    { 2, 10, "EUR", new Guid("48fcaab6-66e7-4a35-9f5f-1388b03c75d8"), "133294120548090361", 73.22f },
                    { 3, 10, "GBP", new Guid("cda6717c-5d77-4043-9250-b1875b14079f"), "133294120548090377", 62.02f },
                    { 4, 10, "USD", new Guid("05490fa5-4694-4f53-805a-29111c8f9293"), "133294120548090392", 83.56f },
                    { 5, 10, "RMB", new Guid("6143f308-2384-406a-936e-a68ac6345192"), "133294120548090406", 536.68f },
                    { 6, 10, "INR", new Guid("29b83926-cc80-47fe-9597-bd9a38c3cf14"), "133294120548090420", 6116.29f },
                    { 7, 10, "JPY", new Guid("d789e092-f6ba-4156-a857-88030fb67f77"), "133294120548090434", 9045.22f },
                    { 1, 11, "RSD", new Guid("edd71760-bff6-492f-8949-f190423d02b0"), "133294120548090451", 1386.5f },
                    { 2, 11, "EUR", new Guid("c4a77347-2a5d-4543-b701-af9ef5c9e2a0"), "133294120548090465", 11.79f },
                    { 3, 11, "GBP", new Guid("ec53ad64-4d5f-4870-8dc0-1aa9786899ab"), "133294120548090480", 9.98f },
                    { 4, 11, "USD", new Guid("f8e34bf4-20f9-4f26-9ef9-a374b2d8052a"), "133294120548090494", 13.45f },
                    { 5, 11, "RMB", new Guid("a62cfdb4-63b8-4c41-bfcc-653a0f68a718"), "133294120548090509", 86.38f },
                    { 6, 11, "INR", new Guid("bce0c9f7-cc95-44fa-8f33-8b47de9cf101"), "133294120548090524", 984.41f },
                    { 7, 11, "JPY", new Guid("6b812f27-dce7-4bb6-93b7-81ff0ff8d037"), "133294120548090538", 1455.82f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 12, "RSD", new Guid("bbdc2544-aa80-4c0b-a4d6-72378a94efff"), "133294120548090552", 5970.5f },
                    { 2, 12, "EUR", new Guid("ae3c8fed-f27e-41fa-b12a-d2691840d34f"), "133294120548090569", 50.75f },
                    { 3, 12, "GBP", new Guid("1b6d6365-9aea-4b83-96db-ac7b7b335270"), "133294120548090584", 42.99f },
                    { 4, 12, "USD", new Guid("e786fb29-a909-45ff-80c9-c101b35877a7"), "133294120548090598", 57.91f },
                    { 5, 12, "RMB", new Guid("0d81cfc0-76d6-442c-8429-0e2ae905e05e"), "133294120548090612", 371.96f },
                    { 6, 12, "INR", new Guid("9128f8f3-9dde-4771-bb8b-a2551bd5a576"), "133294120548090626", 4239.05f },
                    { 7, 12, "JPY", new Guid("34cd0a26-53a1-4804-a649-90747fa8e8d9"), "133294120548090672", 6269.02f },
                    { 1, 13, "RSD", new Guid("8d7703a1-4bb4-4a18-869e-81f1db487865"), "133294120548090687", 3177.25f },
                    { 2, 13, "EUR", new Guid("d4d13d3f-34a2-4b81-bd55-c77d69b3b65a"), "133294120548090702", 27.01f },
                    { 3, 13, "GBP", new Guid("12c5b02a-2e95-4449-80fe-95a07389a0ad"), "133294120548090718", 22.88f },
                    { 4, 13, "USD", new Guid("5fa4c9a3-92c4-477a-8073-01b22d162a94"), "133294120548090733", 30.82f },
                    { 5, 13, "RMB", new Guid("c1803f12-f315-4862-8f11-5a8b07db5165"), "133294120548090747", 197.94f },
                    { 6, 13, "INR", new Guid("307698c5-72e6-4861-ba6d-2aa0fa16acd6"), "133294120548090762", 2255.85f },
                    { 7, 13, "JPY", new Guid("6d0ac40d-61d6-433f-8896-e87214615790"), "133294120548090776", 3336.11f },
                    { 1, 14, "RSD", new Guid("5ae52256-bbe4-4370-b7d2-fd60f9a53160"), "133294120548090790", 9264.75f },
                    { 2, 14, "EUR", new Guid("024503ec-0e40-48f5-80fd-56c045dea65f"), "133294120548090804", 78.75f },
                    { 3, 14, "GBP", new Guid("aa7640fe-fb46-4434-ab6d-2c28fdc1ebdf"), "133294120548090819", 66.71f },
                    { 4, 14, "USD", new Guid("7f69ed09-ca80-4f3d-bce0-03a2c6198ee3"), "133294120548090835", 89.87f },
                    { 5, 14, "RMB", new Guid("b33e01b0-13a0-4232-91d7-19a0abb72836"), "133294120548090849", 577.19f },
                    { 6, 14, "INR", new Guid("b300e455-387b-4c66-888b-a090cb07bfca"), "133294120548090864", 6577.97f },
                    { 7, 14, "JPY", new Guid("0958b354-c5fb-42fb-a8c5-5b8d35816b22"), "133294120548090878", 9727.99f },
                    { 1, 15, "RSD", new Guid("d345a242-0616-41b2-980c-0f1f3c14ac41"), "133294120548090892", 7812f },
                    { 2, 15, "EUR", new Guid("3b568f26-2fc9-4737-93ca-6d7bb7842bfe"), "133294120548090907", 66.4f },
                    { 3, 15, "GBP", new Guid("56d9ae5f-7dfc-4d5b-97c3-b01de5202f3e"), "133294120548090921", 56.25f },
                    { 4, 15, "USD", new Guid("3d9b033c-cbf5-436a-a309-f76908bb0e74"), "133294120548090935", 75.78f },
                    { 5, 15, "RMB", new Guid("63176c83-5ba3-4fa9-a86d-570cfb0d5f56"), "133294120548090951", 486.69f },
                    { 6, 15, "INR", new Guid("87f24255-fdd3-43a5-9f92-bdf1cecb0fe9"), "133294120548090966", 5546.52f },
                    { 7, 15, "JPY", new Guid("ac8ea95f-6fc0-4607-8bcf-ad169f5b960e"), "133294120548090980", 8202.6f },
                    { 1, 16, "RSD", new Guid("e6234bb5-58b4-4dbd-85b8-d3377233d0bc"), "133294120548091028", 2985.25f },
                    { 2, 16, "EUR", new Guid("46c8316c-556d-4961-94f8-d54fd320d723"), "133294120548091044", 25.37f },
                    { 3, 16, "GBP", new Guid("84f7fbd6-b718-4293-889f-e5456090f14d"), "133294120548091059", 21.49f },
                    { 4, 16, "USD", new Guid("1937ecf9-e4b5-4d2f-9a0a-9a38dac51a35"), "133294120548091073", 28.96f },
                    { 5, 16, "RMB", new Guid("99926c08-b6f7-4445-a49b-2cb7010fc76d"), "133294120548091087", 185.98f },
                    { 6, 16, "INR", new Guid("280c3b84-675c-4f15-9b46-c1226344c023"), "133294120548091103", 2119.53f },
                    { 7, 16, "JPY", new Guid("b72368ac-cd74-48ad-8c3a-a2fbed421c15"), "133294120548091118", 3134.51f }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "ClientId", "DocumentType", "GlobalId", "LastUpdate" },
                values: new object[] { 1, 1, "INVOICE", new Guid("494ad822-8ee2-47c3-938f-2de7a43db41a"), "634792557112051692" });

            migrationBuilder.InsertData(
                table: "ProductDocuments",
                columns: new[] { "DocumentId", "ProductId", "Currency", "PriceValue", "Quantity" },
                values: new object[] { 1, 1, "USD", 12f, 11 });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PriceListId",
                table: "Clients",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ClientId",
                table: "Documents",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_PriceListId",
                table: "Prices",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocuments_DocumentId",
                table: "ProductDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "ProductDocuments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "PriceLists");
        }
    }
}
