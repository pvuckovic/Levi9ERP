using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levi9.ERP.Domain.Migrations
{
    public partial class InitialMigration : Migration
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
                    { 8, new Guid("16cabf28-d1ae-4dc4-adb3-a047e10cd113"), "634792557112051697", "RSD 2 Price List" },
                    { 9, new Guid("dbaf7a71-4b18-40bd-a720-a515817cc468"), "634792557112051698", "EUR 2 Price List" },
                    { 10, new Guid("dd67964e-3b02-4e86-95d3-9d16485bbb8f"), "634792557112051699", "GBP 2 Price List" },
                    { 11, new Guid("1b132289-f817-4d98-8f21-3691617054e0"), "634792557112051701", "USD 2 Price List" },
                    { 12, new Guid("481a7110-98ee-471b-9646-a781f9fcadb6"), "634792557112051702", "RMB 2 Price List" },
                    { 13, new Guid("f98bd15f-f7d3-4522-866e-cc57ad3c5d38"), "634792557112051703", "INR 2 Price List" },
                    { 14, new Guid("b6718658-f490-4357-82ea-40acf1fd5259"), "634792557112051704", "JPY 2 Price List" }
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
                    { 1, 1, "RSD", new Guid("f7b145a2-1b70-4384-b747-5dd88ed7e735"), "133295606978041387", 9750.25f },
                    { 2, 1, "EUR", new Guid("752e4749-d1d6-4ecc-97eb-911d5e346782"), "133295606978041491", 82.88f },
                    { 3, 1, "GBP", new Guid("6c4bb723-30ca-43ca-bcf8-aaef7379b877"), "133295606978041512", 70.2f },
                    { 4, 1, "USD", new Guid("d23b1cd4-802d-4f9e-befc-e0ec7e1ba7b8"), "133295606978041531", 94.58f },
                    { 5, 1, "RMB", new Guid("9f7d8f0a-9878-4b5e-b20f-68a8b9f45b06"), "133295606978041548", 607.44f },
                    { 6, 1, "INR", new Guid("11a8dd97-c5b9-462e-a24b-f38b3706baf1"), "133295606978041568", 6922.68f },
                    { 7, 1, "JPY", new Guid("fae86c15-bd88-4247-b1d1-366832031eb2"), "133295606978041601", 10237.76f },
                    { 1, 2, "RSD", new Guid("72540990-e38f-43b9-83f8-e37d9266e57b"), "133295606978041620", 4121.5f },
                    { 2, 2, "EUR", new Guid("6af6d230-1f71-419a-9656-115dbe33431e"), "133295606978041637", 35.03f },
                    { 3, 2, "GBP", new Guid("40144a0d-ebdf-46d6-bc43-350d1f0eea86"), "133295606978041657", 29.67f },
                    { 4, 2, "USD", new Guid("6e2d1fb5-051a-4c20-b7ef-da50d5a889b3"), "133295606978041674", 39.98f },
                    { 5, 2, "RMB", new Guid("7232adc2-91be-4b95-9971-143228c16657"), "133295606978041691", 256.77f },
                    { 6, 2, "INR", new Guid("6bd10c85-8ceb-4549-80c5-6f78fd6be054"), "133295606978041744", 2926.26f },
                    { 7, 2, "JPY", new Guid("7face104-8068-4227-9e3f-b0616bb33166"), "133295606978041764", 4327.57f },
                    { 1, 3, "RSD", new Guid("b15f994f-2379-4193-995e-697524808aeb"), "133295606978041784", 6383.75f },
                    { 2, 3, "EUR", new Guid("160fbd02-027c-45a9-8195-7ccc8a6fbcd7"), "133295606978041800", 54.26f },
                    { 3, 3, "GBP", new Guid("1b741325-c66e-4b60-bee5-5f9d6b584665"), "133295606978041817", 45.96f },
                    { 4, 3, "USD", new Guid("aad1fa94-5615-4591-ac2d-8519f8ef6798"), "133295606978041836", 61.92f },
                    { 5, 3, "RMB", new Guid("60c67ff3-536e-4fb7-a469-474156562e3a"), "133295606978041853", 397.71f },
                    { 6, 3, "INR", new Guid("f32c7bb6-f0c7-4f14-9e93-88060f872081"), "133295606978041869", 4532.46f },
                    { 7, 3, "JPY", new Guid("f9a84799-ca11-4e54-adaa-825299012dbb"), "133295606978041886", 6702.94f },
                    { 1, 4, "RSD", new Guid("c951aeb5-8589-4028-aa11-b0d3973e28a9"), "133295606978041902", 8925f },
                    { 2, 4, "EUR", new Guid("356926c3-beb9-44e5-a253-82846699a279"), "133295606978041921", 75.86f },
                    { 3, 4, "GBP", new Guid("e714102e-2eb2-4509-b945-e8670085f852"), "133295606978041937", 64.26f },
                    { 4, 4, "USD", new Guid("5fec5b05-a84d-4e8d-ae1b-f4141baad088"), "133295606978041953", 86.57f },
                    { 5, 4, "RMB", new Guid("f9d269ce-577e-4bb4-85ee-1e4b3840438d"), "133295606978041970", 556.03f },
                    { 6, 4, "INR", new Guid("b2341e2a-63a7-42b0-8649-c9de20be0e79"), "133295606978041986", 6336.75f },
                    { 7, 4, "JPY", new Guid("caf3fde0-1274-4dae-83d5-ef364cefb1e5"), "133295606978042002", 9371.25f },
                    { 1, 5, "RSD", new Guid("e6f40e6f-fbcd-4498-ba88-65f28abad478"), "133295606978042019", 5241f },
                    { 2, 5, "EUR", new Guid("6c3e25b2-33c5-4479-bed5-41717a2fdfb0"), "133295606978042035", 44.55f },
                    { 3, 5, "GBP", new Guid("39854d4b-0330-49d4-a68a-c61cf95b98c0"), "133295606978042054", 37.74f },
                    { 4, 5, "USD", new Guid("010a4ab9-00e8-4ad4-8b46-4fda33f837d8"), "133295606978042070", 50.84f },
                    { 5, 5, "RMB", new Guid("d692ee60-2223-4eba-9686-b395c9bd12c3"), "133295606978042087", 326.51f },
                    { 6, 5, "INR", new Guid("2172ae92-c4ca-4209-a839-60e1fef491f0"), "133295606978042138", 3721.11f },
                    { 7, 5, "JPY", new Guid("77e81505-7edc-4399-b106-8a311563bb60"), "133295606978042156", 5503.05f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 6, "RSD", new Guid("185e8adf-1d4d-4878-8e6b-2bb61ac7a708"), "133295606978042172", 7568.5f },
                    { 2, 6, "EUR", new Guid("b9d812e4-c53a-4f59-b629-1d0c2dbac95c"), "133295606978042191", 64.33f },
                    { 3, 6, "GBP", new Guid("1fb87dae-7d38-4c28-9724-2606351a617e"), "133295606978042207", 54.49f },
                    { 4, 6, "USD", new Guid("b7fc25b3-274c-4855-a2f4-c87859ddc948"), "133295606978042226", 73.41f },
                    { 5, 6, "RMB", new Guid("58537486-ee0f-4272-9bf2-b323c646b120"), "133295606978042242", 471.52f },
                    { 6, 6, "INR", new Guid("a4f280a1-f930-408a-bb15-c32cd74d601e"), "133295606978042258", 5373.63f },
                    { 7, 6, "JPY", new Guid("b8f10ba1-3a74-4868-aefd-56adbf2de327"), "133295606978042274", 7946.92f },
                    { 1, 7, "RSD", new Guid("787666b2-b37a-4a1c-8482-3c80fabcbd28"), "133295606978042290", 1953.75f },
                    { 2, 7, "EUR", new Guid("364866df-6921-4754-a297-ce7080b72356"), "133295606978042306", 16.61f },
                    { 3, 7, "GBP", new Guid("01df1fc3-95c9-4e47-85f6-6c28de55ad7a"), "133295606978042324", 14.07f },
                    { 4, 7, "USD", new Guid("0a66ca2a-e991-46ca-a018-57a7076cd3da"), "133295606978042339", 18.95f },
                    { 5, 7, "RMB", new Guid("fb76a2ca-3acd-43f3-b834-bf26cf9e1f83"), "133295606978042359", 121.72f },
                    { 6, 7, "INR", new Guid("39b600fa-89c4-4be0-977b-39fe375a4e23"), "133295606978042375", 1387.16f },
                    { 7, 7, "JPY", new Guid("45781bae-5507-4808-82f1-c61bf0356075"), "133295606978042391", 2051.44f },
                    { 1, 8, "RSD", new Guid("0bfc6803-9a80-440e-b2c3-8f19d739bf49"), "133295606978042407", 6546.5f },
                    { 2, 8, "EUR", new Guid("4423d091-9937-4b1c-ac68-1c15f335ae74"), "133295606978042425", 55.65f },
                    { 3, 8, "GBP", new Guid("598ce200-550a-4f95-9979-bcb571d6db09"), "133295606978042441", 47.13f },
                    { 4, 8, "USD", new Guid("f59b2577-5430-49e7-9a99-92c3eb8b54f7"), "133295606978042457", 63.5f },
                    { 5, 8, "RMB", new Guid("dcd873fa-3b40-41b4-ac96-3e7ab02e50a1"), "133295606978042473", 407.85f },
                    { 6, 8, "INR", new Guid("097cbda5-269d-47af-ad31-ba3f9d0538a1"), "133295606978042523", 4648.01f },
                    { 7, 8, "JPY", new Guid("09761da3-45a0-4730-b422-7b403eab14cc"), "133295606978042541", 6873.82f },
                    { 1, 9, "RSD", new Guid("dca9a655-e490-4efd-b046-24e0460ba056"), "133295606978042558", 4037f },
                    { 2, 9, "EUR", new Guid("6f343a07-6dbb-435f-a43a-efd6ae4f6325"), "133295606978042574", 34.31f },
                    { 3, 9, "GBP", new Guid("23db1c04-11d6-455c-96f2-2f9fcf508b72"), "133295606978042590", 29.07f },
                    { 4, 9, "USD", new Guid("21536de3-5514-41c4-b462-3947851c2169"), "133295606978042606", 39.16f },
                    { 5, 9, "RMB", new Guid("7c151516-abdd-40f4-b078-942ad7b2ccd5"), "133295606978042622", 251.51f },
                    { 6, 9, "INR", new Guid("18fd9a48-4d2a-40c0-9bdc-1f1a9931976f"), "133295606978042638", 2866.27f },
                    { 7, 9, "JPY", new Guid("36079cc4-cfde-41fe-aa90-9c7affcaff07"), "133295606978042656", 4238.85f },
                    { 1, 10, "RSD", new Guid("563eceb5-8293-413d-878b-ae36ce42d91f"), "133295606978042673", 8614.5f },
                    { 2, 10, "EUR", new Guid("d2370f3c-798e-4594-85b2-02b13a637345"), "133295606978042689", 73.22f },
                    { 3, 10, "GBP", new Guid("87426d7f-ef21-4795-8c5b-b5eda5dcf50a"), "133295606978042708", 62.02f },
                    { 4, 10, "USD", new Guid("87b35b55-40fb-4124-8500-6fb9c36aafa1"), "133295606978042724", 83.56f },
                    { 5, 10, "RMB", new Guid("f1574dda-54d4-4578-8974-47cc25f07f81"), "133295606978042740", 536.68f },
                    { 6, 10, "INR", new Guid("77f9630a-a78b-4fde-b71c-f7914a4a4fdd"), "133295606978042756", 6116.29f },
                    { 7, 10, "JPY", new Guid("c17ab520-e39e-41a4-a462-f8878f2d0931"), "133295606978042772", 9045.22f },
                    { 1, 11, "RSD", new Guid("d85ee87b-2d65-4862-9538-e63225dc9658"), "133295606978042791", 1386.5f },
                    { 2, 11, "EUR", new Guid("3022df15-ebeb-4bdd-a455-e7d36720000a"), "133295606978042807", 11.79f },
                    { 3, 11, "GBP", new Guid("652d4c24-3180-4e31-ab13-e6e91f0aa93d"), "133295606978042823", 9.98f },
                    { 4, 11, "USD", new Guid("c11cfa67-3f2e-4db4-86cb-c835fa1ff788"), "133295606978042839", 13.45f },
                    { 5, 11, "RMB", new Guid("8691b4a3-5cf1-404a-884c-9dcb7b970e37"), "133295606978042890", 86.38f },
                    { 6, 11, "INR", new Guid("eed833cc-c23b-45d7-bcbc-5b5f3d31bf76"), "133295606978042907", 984.41f },
                    { 7, 11, "JPY", new Guid("faf63647-a554-4777-9fe6-0698be5245af"), "133295606978042923", 1455.82f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 12, "RSD", new Guid("3a5e5da5-291f-4969-b31c-03be43da34c5"), "133295606978042939", 5970.5f },
                    { 2, 12, "EUR", new Guid("a2842945-4bf9-4ed3-af65-6936dfe8e523"), "133295606978042958", 50.75f },
                    { 3, 12, "GBP", new Guid("d5780211-1587-4fad-929c-88a34216642a"), "133295606978042974", 42.99f },
                    { 4, 12, "USD", new Guid("161d161a-ec34-4a55-9701-16289474eeff"), "133295606978042990", 57.91f },
                    { 5, 12, "RMB", new Guid("15abdd39-229e-4660-8dc1-e3045ae5981a"), "133295606978043006", 371.96f },
                    { 6, 12, "INR", new Guid("39782261-9352-4795-80c2-47366fe00949"), "133295606978043023", 4239.05f },
                    { 7, 12, "JPY", new Guid("db965054-dbdb-4d0d-95d3-f9dea29d5e25"), "133295606978043039", 6269.02f },
                    { 1, 13, "RSD", new Guid("278f6486-ab70-4d88-9338-d6db083484c0"), "133295606978043055", 3177.25f },
                    { 2, 13, "EUR", new Guid("ef617b3e-271d-43e5-bfbd-40e3ed34cd41"), "133295606978043071", 27.01f },
                    { 3, 13, "GBP", new Guid("e38b4f36-5c30-41f7-bc68-a9855cf732b9"), "133295606978043090", 22.88f },
                    { 4, 13, "USD", new Guid("fde599d6-a835-452c-ad31-2fc3130f1790"), "133295606978043107", 30.82f },
                    { 5, 13, "RMB", new Guid("2032e3e6-4892-4f6f-b3d3-8c9c852182ba"), "133295606978043123", 197.94f },
                    { 6, 13, "INR", new Guid("d23a0a3b-7cfe-4c71-b30f-4fd5c74a84dd"), "133295606978043139", 2255.85f },
                    { 7, 13, "JPY", new Guid("66ab40e1-b1b5-4530-a9ce-9f31170ad2c6"), "133295606978043156", 3336.11f },
                    { 1, 14, "RSD", new Guid("ef897741-466e-489c-9d56-334230b0af41"), "133295606978043172", 9264.75f },
                    { 2, 14, "EUR", new Guid("6ea3a5c3-cf64-424b-93fd-c40bbca263c3"), "133295606978043189", 78.75f },
                    { 3, 14, "GBP", new Guid("92829b6c-6f46-445b-993e-9cb4b253e41a"), "133295606978043205", 66.71f },
                    { 4, 14, "USD", new Guid("2b42fe2b-1530-4725-a73c-7aaed03928f0"), "133295606978043223", 89.87f },
                    { 5, 14, "RMB", new Guid("8190a995-b534-4753-b2c5-a3e86a20857e"), "133295606978043240", 577.19f },
                    { 6, 14, "INR", new Guid("37337a81-6984-4053-bd31-75385b55f39c"), "133295606978043314", 6577.97f },
                    { 7, 14, "JPY", new Guid("41ffc365-ebdd-4f17-802b-82399e04640e"), "133295606978043332", 9727.99f },
                    { 1, 15, "RSD", new Guid("07063319-2b93-4de7-8ec0-f7cc25b08d24"), "133295606978043348", 7812f },
                    { 2, 15, "EUR", new Guid("b29fcf48-a90c-4a9a-8d58-84bb19367656"), "133295606978043364", 66.4f },
                    { 3, 15, "GBP", new Guid("77f6d077-c691-46fe-87e2-3cbb7b6db4ae"), "133295606978043380", 56.25f },
                    { 4, 15, "USD", new Guid("0c54f70b-c7f4-4c88-bacb-676b4cefd7ef"), "133295606978043397", 75.78f },
                    { 5, 15, "RMB", new Guid("afe5e1f3-ba65-4939-ab66-1df9b33614da"), "133295606978043415", 486.69f },
                    { 6, 15, "INR", new Guid("30ef7e2c-3268-43f4-9081-5ce8cf11bc66"), "133295606978043432", 5546.52f },
                    { 7, 15, "JPY", new Guid("b372687f-937d-45c8-a604-495ca6fbe58c"), "133295606978043448", 8202.6f },
                    { 1, 16, "RSD", new Guid("7a063f5f-cf2d-419f-b53b-24179e5c7ccd"), "133295606978043464", 2985.25f },
                    { 2, 16, "EUR", new Guid("fd96ec09-10dd-4e5e-a23c-e4fab1a60ccc"), "133295606978043480", 25.37f },
                    { 3, 16, "GBP", new Guid("66115dbf-033f-49bb-9127-e5b36dfa0230"), "133295606978043497", 21.49f },
                    { 4, 16, "USD", new Guid("10901642-4a5a-460f-bb11-adc1f6bf083d"), "133295606978043513", 28.96f },
                    { 5, 16, "RMB", new Guid("9b7a3351-5452-4959-931b-f2c8d81f40d6"), "133295606978043529", 185.98f },
                    { 6, 16, "INR", new Guid("17a9d986-b85d-4d07-8719-5cda4e5ab2db"), "133295606978043548", 2119.53f },
                    { 7, 16, "JPY", new Guid("6957370e-8b3a-4a69-9149-fa9864872259"), "133295606978043564", 3134.51f }
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
