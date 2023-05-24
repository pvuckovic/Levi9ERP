using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levi9.ERP.Domain.Migrations
{
    public partial class initial : Migration
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
                    { 8, new Guid("a3aaa444-73d8-4241-9bf7-763a57a619e4"), "634792557112051697", "RSD 2 Price List" },
                    { 9, new Guid("0e9a133f-53fe-487e-81ea-c738ee4cf5b6"), "634792557112051698", "EUR 2 Price List" },
                    { 10, new Guid("9c5543fe-9ca0-4cf9-8265-663675ef8138"), "634792557112051699", "GBP 2 Price List" },
                    { 11, new Guid("22fcca6b-dea7-4998-9135-ec08267ce978"), "634792557112051701", "USD 2 Price List" },
                    { 12, new Guid("2b02525d-d550-4f28-807c-74650b309a6f"), "634792557112051702", "RMB 2 Price List" },
                    { 13, new Guid("0448a2ae-a641-4a93-a1b7-57eb7d6ff91d"), "634792557112051703", "INR 2 Price List" },
                    { 14, new Guid("482651f2-d4c4-4b4f-8177-c925b0970c5d"), "634792557112051704", "JPY 2 Price List" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "GlobalId", "ImageUrl", "LastUpdate", "Name" },
                values: new object[,]
                {
                    { 1, 104, new Guid("6f68f25e-9645-4da9-b66f-8edbebb8a6e7"), "https://db3pap003files.storage.live.com/y4mbO0YJ-ovSlqOuXjgGPR2IsjN8sCk9ULksyzIkvLEJeRyYnqKUxHmuxppx-HMcp6oNzzOrMc6_6YRgmiaeA5_vxCRWCV9ARd_GK6hPLdp3tEcsgR7Us9uzAfvH331KiJrqlwCGiOPFDi_OsHLznB3tqbTk-bFfQIqQdyTkno03JxvSOQ8vwEjvCFJUa0Bx7IUBzg4bU_qzf3qpgZMeYI1y1rt9qCldOaIi3WgyU-E5J0?encodeFailures=1&width=500&height=500", "133292999980720227", "T-ShirtBlack" },
                    { 2, 138, new Guid("d68bce12-9f03-4e5f-b0b9-3ad1205afda4"), "https://db3pap003files.storage.live.com/y4m-kRT2GTNRM1fORXdHbHlvpzF8ne8_HsD3AoL-ytcD5vCq2S7olsPp-Nnw017SkIRz6yUxkIym7En4FjUFiENJ2BVJN0TWz9aqDHht8mlh5RrTwQ-ZBXD6Lm8fI9cy390BzcPCGZchRsm6dGkmTU3q3xZE-WU1VNKV-64xUJN02ofGt6w7I7vJxqaeaHh15q_ul04x3dCUumC53DWniI5oEilVornGdGyddxKc8xZoqw?encodeFailures=1&width=500&height=500", "133292999980720333", "T-ShirtBlue" },
                    { 3, 745, new Guid("2c103912-7b0f-4469-8769-3989a4d62bc2"), "https://db3pap003files.storage.live.com/y4mAsciDm3-1F9piIJKD-IzaefrUBqEbA6CPHpORsvcIBh6PK8Ze9TOhMEvKpBx0RhBUTOIP5AItxqcmLSMiYSz2g7KV8XG0jugLvNtkqC8R1DwnwOQRZHj3csgnd_iTL1qWPfzolxTny7naR0izqMtG3i-w3ziMWHQuEqzwiTc98QTHj8LfkRuMHhv72ZNttAQuoj3-Y2iHokZ4JsuU5DkNfzVpvAKZ4w5wgS5V99rOD8?encodeFailures=1&width=500&height=500", "133292999980720350", "T-ShirtBrown" },
                    { 4, 5, new Guid("a6c74b0b-b1f3-4e12-b6d2-6c38a3903ce6"), "https://db3pap003files.storage.live.com/y4mIMtsMKg4MteMPQCPtJgs6K1S-d2um3XXpYS8Du6_NWnd4lNDiHdlq6Eb524q2jskw_8_vshuYV5u-K6AO8mxW2llKQxnBEL01m5t-JNBM5LecnrYtGho2DiJipC6x4r3jD1VeW34xPSR1p_juV-1oqWdrrQUf8PbaDdD75jE3ar1KeD9yUlPhM5BUrCTjzaJusBxqVk0smMVMzQ7Bja62BV6-4jQ0W743Q1JiUNeOPA?encodeFailures=1&width=500&height=500", "133292999980720364", "T-ShirtGreen" },
                    { 5, 57, new Guid("8dbff2d4-42a0-4fe7-9864-05571f3c22db"), "https://db3pap003files.storage.live.com/y4m0eG0CddqsLAfGnFHIiySLlbn9TzPQ9pQOm7YcP9QXblbZJauurhybDbiJTbA4xb-obY2cjRUlaacpZakO8zQ1oDTZQrDaipf0vU9FiylYlxK64_CzrAuGrJenGE98iclH6BN9uWZH3k1V5XthDTzldVwtY2lvP_upoU-UQyNGfX0goTCeF3FnK7rHh5pwIPSQWwSeHNPserQ6skA9FWR0ehISp1axZbphMw6aYmQwhU?encodeFailures=1&width=500&height=500", "133292999980720378", "T-ShirtPurple" },
                    { 6, 224, new Guid("9f7c8b37-0e37-49ed-af1f-4e6a7428a873"), "https://db3pap003files.storage.live.com/y4mLQ8P5MJROOmihR3K_1SU4Y_Iisgc0y1DeC-ZizOELnj82YZeiGhXEY8cavf9xNTC12wtRKmUN42mQysBXyV066A8XFzE9XNScQNdflZhIhCKM0xUmgKHlCG6P9z3jIiqdxENbwxFU6dBr1UEpJpaZXWRHliG8nJNI1TttSsDw533HGJR5gCO9hpdlDDz4W0ltkmYE_oPT24JKDwnLNZyHZv6dfzI-qqzOrlBa5TnPQQ?encodeFailures=1&width=500&height=500", "133292999980720393", "T-ShirtRed" },
                    { 7, 150, new Guid("e0a01813-5f43-4a15-934f-9946ef8f4182"), "https://db3pap003files.storage.live.com/y4m0BTJrLTQNPzne4u5bX6n47paz88nldiOZnxawfhv4oJKFk8fP4TZR91HGDs43pSIw2wEXKbJq3cJ8pItmZ-tu9LSPBEpjbTo5jIuiMsFipvDwkZoT4r9C0DHCYD6vVjnRdGP0OgbxKBKGgtOXUlc3hMb3KalQVb5xlK1EC_ukMs5cLmBeHqGzEmI2SBe3zCyeoNgaf3GpysUv9NAcoG1ppL0FCXfJMJbM0zWUbcSfJ0?encodeFailures=1&width=500&height=500", "133292999980720407", "T-ShirtRose" },
                    { 8, 186, new Guid("156fd903-9fd2-41af-9b09-600d598b31c4"), "https://db3pap003files.storage.live.com/y4m8PzFIhNEKewi3wbogTAR2veLJqFbaOBgSNlzytwuowu0Lv2SIQxwPhsPe9EFmuc0N8lgEdowWmm29Cavcz6isxa4cpPVOr6jtl9BW5Ajv4lcvKSE4oXqNIMeElwiw53q6Bg84U_VG1c3ntS0UCXSMdo4iRDZJURcBbeIDv7GFCnRKcgMHQ6FD1md_owl3q55SGzBIEsFOpyMku_CcvDb77ttqpDhJ4YPF9CCbzQ8esk?encodeFailures=1&width=500&height=500", "133292999980720419", "T-ShirtYellow" },
                    { 9, 104, new Guid("6f8799a7-097b-45f4-a8f0-805784ee24a6"), "https://db3pap003files.storage.live.com/y4mcROrUsGJ1C_DaPGGRsunJPAdSjzSLWBSqcDa4cHQt2VRY9AZqbHg4T7EPdSV1btrGwXH7VAJuf61Ld1knMXgnsRqdUuLpQkQCtWFYohlEv9VAK3QZzHC6XaVQNugbmcDj03yOM09WghkBH2eDm_EV5MF8iXSjIOwAktBC9Qet-CY16VN2MSm7UFFgw94gEbasO_GBbkRvzfCwOmUcXICRMopHO6obWAB_ATRGjlLHO4?encodeFailures=1&width=500&height=500", "133292999980720435", "T-ShirtBlack 9 Gradient" },
                    { 10, 138, new Guid("49674251-6b45-437f-8c7a-34a0b1a34c01"), "https://db3pap003files.storage.live.com/y4myPaEj8lwOAp9D6FSSd6vqVOshe2DZNPeTkOqpYexCSHAtxzO73MHXGGEkScMtu8k1iYqm2lKRBn5DMz5f0zerCAOvzrd8pGwL_fmarBVTMSmopv0Vy1YV5FDpu1-hPgVG0zKhYbu0jy8x2n4_ZOpZTDjzcrwrJrE78DuISNhBLvF2SGidifBduSxqFfXd7DPF_2Gr4dIMcymuT-OBT2opZWcpARi9ccxq-vfz-KPnqA?encodeFailures=1&width=500&height=500", "133292999980720451", "T-ShirtBlue 9 Gradient" },
                    { 11, 745, new Guid("7b5ae8b9-12a0-4a19-9c37-943bc4cc5a3e"), "https://db3pap003files.storage.live.com/y4maMJZDwT_Bt_9lYoj4QEBJplPpSpox0xs8mlm5MJZp-e4mC1TTol3ATfgm9TGwDlCcYnXzdhSqaPeqkZJK90tgRR_KQHtctku28zVSo3D8Rf_462QClZiF4CC03XosaZsn0ZWDdq7KhWdYpC8hUpkJQf9bbvjeRgYJfdHr8yqhGO3ENu8lAwvwUIMRs9LaXDIBAaXurelBc7zK18wxVt8opgjC4MvmdhJC0rjqWefNk8?encodeFailures=1&width=500&height=500", "133292999980720465", "T-ShirtBrown 9 Gradient" },
                    { 12, 5, new Guid("097dbb41-2aa0-4fbf-8986-bdf8e4e0d4b9"), "https://db3pap003files.storage.live.com/y4mywnSFEOeNPAc3Dv5LG3QKmt8cnkjDcCHmNyhpMMvkFiSJlUvZkss9mSJAYld9eYyTO4ELiz4sMXc2pgLtrqqsjzrTfWHItrt9Vesn1xHDXi7ONiXvdldHGHQwBDoTr5yQhbvShlKJVTfpL0UM67KIKTjDH3c9MqAUc2S8bwTjmai-g_WeOH4BdyJZz41mDWGTS88odSwCfN94gatrXq-Nc4vr96zUOhvoWSJHQPm-rE?encodeFailures=1&width=500&height=500", "133292999980720478", "T-ShirtGreen 9 Gradient" },
                    { 13, 57, new Guid("5f8d15b2-4c4d-4e7a-8902-d78d3b4cb10b"), "https://db3pap003files.storage.live.com/y4mkHeVXwv9fF7d7rmBK_fJ0Ge3c07dLoVpzqCrrF5HIEFDpKXFGkoRnJ9WmXzt1-bsSG3J5gSP8C3Aj5p8GjTS_tnZrGMCj2oNJbYvMRbyJbAzA3Gy7ynLFEdddnq8cNHwwWB4Jh2k8988ufJ7H-p2oe5lr4oQRvshHBzuIRt52RNWaGspRAVow1L551awcDC_BfmD2se_Yg9guDVlDTOMqVvO9PEqlMP3B-3y_I9oIz0?encodeFailures=1&width=500&height=500", "133292999980720490", "T-ShirtPurple 9 Gradient" },
                    { 14, 224, new Guid("4d50363a-2dc4-4ef0-8bde-682758f0f801"), "https://db3pap003files.storage.live.com/y4mAuqUBcOB4e_6Pkv5WtCBhsOp6xQyB3dhrzYEuJsoMAijFJ6pSGth6esbBkAIU2JpgSDze6vTEC07JiJWAPnyjuL2PsE_bmP1XypvPO3HZP2uIDAThpD-nk4Ews_QeQd0TNNorcNqO6JqaEqIP9OpEEfkLnBcK5Q0FHBI3IxP6octHzAlY5_M-z93ypRdrhwBjEjnmpEBsZ7eeZwORwXxRa1nw2b-TwPSr6F6uZZ2MUs?encodeFailures=1&width=500&height=500", "133292999980720504", "T-ShirtRed 9 Gradient" },
                    { 15, 150, new Guid("1835ee0e-3275-4ff9-9d79-1a2e3b5e15e3"), "https://db3pap003files.storage.live.com/y4mUU4G3Dpx9xkjqniNIZeMWz-wr3t18PGCtpoQxOv522aU8IDNVk6-MWFtqONztKzH6BelSIR6NhOXklxW5CEhTct5nyscvSGwHujoHepodUBhtjuXXW2KudArDxA2g4ARoELCaWVCAvdAm46C7BnzDcbXRf4QBFvz-iUG9V-Nm7O1adpQ6gnF96Xkp2iyNrkGkpc0x2hJ06TVqNWDYM7XfrJooKEbFfYcllC7cy17X5E?encodeFailures=1&width=500&height=500", "133292999980720517", "T-ShirtRose 9 Gradient" },
                    { 16, 186, new Guid("e5e4a551-2b64-43fc-86e1-4c1c0f6880fe"), "https://db3pap003files.storage.live.com/y4m9Oo0CoS06pvC58i8iE_2_WfJE-rEuIMGRWZcbdjThze0gTjJBWWtaDGSsnxJmhCweSY3lvNc6ZYxLMV8zlKM5CTJ24-WbT4iLjCtOgYg_Oy3hxxhcZOTXkMefthHQhT2OxtSmUDKkQIf4shsWTIu81eHGEovGq63_UqM-gkXR0uqYZXZLfgmVR9cTgk4Hyt8dmweIrouQ_eGM4L7TS5xP3d74G3moTJ72Lzjv8XLbSo?encodeFailures=1&width=500&height=500", "133292999980720530", "T-ShirtYellow 9 Gradient" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Email", "GlobalId", "LastUpdate", "Name", "Password", "Phone", "PriceListId", "Salt" },
                values: new object[,]
                {
                    { 1, "Test1 Address 123", "test1@example.com", new Guid("b8c03b21-adf0-4148-80ac-8852907419b7"), "133291695156186842", "Test1 Client", "mL+yK0IZ6Tbe5ZgQVme2GWD8ayRg9VtRI897U0LuB0w=", "0611234567", 1, "ypt+7c/LVq46Eg==" },
                    { 2, "Test2 Address 123", "test2@example.com", new Guid("1e3b3042-4123-4610-8bb6-51b1282dc768"), "133291695252052729", "Test2 Client", "MWpg/GsSmNKsvjH0ApaCvOG4C/G4sJb58hudQeRxzEc=", "0621234567", 2, "aTgpP/3Uf1JULg==" },
                    { 3, "Test3 Address 123", "test3@example.com", new Guid("5c69a8f5-8eb2-4d89-b21f-7e5e3aa00c08"), "133291695380809913", "Test3 Client", "uOEE7w8xH/lX2mmKdgb63SxqItyMrLfamMbYx5uSn7Y=", "0631234567", 3, "boAfkuX2rmOaaA==" },
                    { 4, "Test4 Address 123", "test4@example.com", new Guid("d3d4e623-e6bc-44e4-98cb-681ef30b1fd4"), "133291695478630236", "Test4 Client", "Zr9Dm1X54LUOAqAybdKj+96MS98kLfrk7dUSB2kXn6s=", "0641234567", 4, "ATpzta8FakmUvA==" },
                    { 5, "Test5 Address 123", "test5@example.com", new Guid("4da162c6-1af1-422e-be9c-debec2c62b54"), "133291695569534051", "Test5 Client", "PylZhPreW4GCLJ5zPs19QaVFqH+clUKonOGy+R3s+X8=", "0651234567", 5, "r6H61KFl8j041w==" },
                    { 6, "Test6 Address 123", "test6@example.com", new Guid("e4d4fc68-0fa9-4356-b68f-886342655080"), "133291695663444894", "Test6 Client", "kgrSYNIZgqg2NKyxe2xBgmiKmQBp/cUiprmFuxyqCnk=", "0661234567", 6, "QX8u19aR8y3QmQ==" },
                    { 7, "Test7 Address 123", "test7@example.com", new Guid("4470cd66-a960-4abc-ab54-45d92a700e80"), "133291695757736110", "Test7 Client", "uN1+O4ywbrGcKAVjKCXfCILz8K57UW+0fLzz/G+pMOg=", "0671234567", 7, "qWurv2IcifFGJQ==" }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 1, "RSD", new Guid("23078814-cca6-4853-ae34-80d72bbf6c8a"), "133292999980720615", 9750.25f },
                    { 2, 1, "EUR", new Guid("27254b1f-1d05-4d07-8ad2-34ef8ed2f505"), "133292999980720637", 82.88f },
                    { 3, 1, "GBP", new Guid("8f42e596-5c47-4cd2-a62a-422eb531d13a"), "133292999980720653", 70.2f },
                    { 4, 1, "USD", new Guid("69e8ef84-5530-4caa-94e2-d1e7b9eac64b"), "133292999980720668", 94.58f },
                    { 5, 1, "RMB", new Guid("b7ecb5f3-505f-4a24-a1a9-51f0b635aca2"), "133292999980720714", 607.44f },
                    { 6, 1, "INR", new Guid("33dbbe66-e187-4ecd-9699-199067872595"), "133292999980720785", 6922.68f },
                    { 7, 1, "JPY", new Guid("f4880041-f7e0-4e5d-a9dc-c9e4c4ccd273"), "133292999980720813", 10237.76f },
                    { 1, 2, "RSD", new Guid("b4cf5dab-5792-44f0-b2e7-00bd302df071"), "133292999980720830", 4121.5f },
                    { 2, 2, "EUR", new Guid("fdc7c017-10bc-4144-b759-6d1c4f3f660b"), "133292999980720845", 35.03f },
                    { 3, 2, "GBP", new Guid("bddb58c4-7948-4a51-b27f-323edf9526e5"), "133292999980720862", 29.67f },
                    { 4, 2, "USD", new Guid("1b667ad6-f330-4752-a5f6-f687babf1ed2"), "133292999980720877", 39.98f },
                    { 5, 2, "RMB", new Guid("91e38fbd-82e5-4126-ae7a-f69dc95df030"), "133292999980720892", 256.77f },
                    { 6, 2, "INR", new Guid("0c148662-2705-4e2d-8859-ce28baae7c53"), "133292999980720906", 2926.26f },
                    { 7, 2, "JPY", new Guid("8ac59fd0-7761-469a-a27f-d1d16d3d6064"), "133292999980720921", 4327.57f },
                    { 1, 3, "RSD", new Guid("4996fed4-ecfb-44b4-9d12-a6069e5f596b"), "133292999980720938", 6383.75f },
                    { 2, 3, "EUR", new Guid("9a7f5748-4cc5-44c7-8f37-f1168cbfc141"), "133292999980720953", 54.26f },
                    { 3, 3, "GBP", new Guid("d0f34300-b0bd-4e58-8280-dd48190ff286"), "133292999980720968", 45.96f },
                    { 4, 3, "USD", new Guid("f8334119-4aec-4324-90b6-1c9878a773e9"), "133292999980720985", 61.92f },
                    { 5, 3, "RMB", new Guid("b4d74943-50f6-45a0-bd7f-055204749f78"), "133292999980721001", 397.71f },
                    { 6, 3, "INR", new Guid("243dca9f-a0ad-4ad2-aeba-188cb68b4890"), "133292999980721015", 4532.46f },
                    { 7, 3, "JPY", new Guid("d4320b68-9938-4878-927c-7033774ea93a"), "133292999980721030", 6702.94f },
                    { 1, 4, "RSD", new Guid("9ff9314f-15e6-40da-9133-9be371e436b1"), "133292999980721045", 8925f },
                    { 2, 4, "EUR", new Guid("c5a888e3-1a04-4526-b275-653e4acdb3c4"), "133292999980721061", 75.86f },
                    { 3, 4, "GBP", new Guid("85037832-f91b-48aa-9cdd-fd578521c250"), "133292999980721077", 64.26f },
                    { 4, 4, "USD", new Guid("41af9e78-2e56-4c3a-8ed7-5f4584cd8592"), "133292999980721091", 86.57f },
                    { 5, 4, "RMB", new Guid("8e4ce0bb-a0e5-4941-88b5-63ab93b41d53"), "133292999980721105", 556.03f },
                    { 6, 4, "INR", new Guid("3ad6b790-8a16-43ef-9739-3b3f8035233b"), "133292999980721150", 6336.75f },
                    { 7, 4, "JPY", new Guid("22a0213d-d42f-4b64-8c90-59900eea8ebd"), "133292999980721166", 9371.25f },
                    { 1, 5, "RSD", new Guid("2e9b0baf-affe-4503-859f-23f29fa41254"), "133292999980721180", 5241f },
                    { 2, 5, "EUR", new Guid("a2bf0dae-77ff-453a-80b3-8a6aa048e75a"), "133292999980721195", 44.55f },
                    { 3, 5, "GBP", new Guid("54b228cb-dd2c-430d-9bb0-2db6198a9924"), "133292999980721211", 37.74f },
                    { 4, 5, "USD", new Guid("d87c7f1f-34e5-4759-874a-fd23d5e22bb1"), "133292999980721227", 50.84f },
                    { 5, 5, "RMB", new Guid("719a47c2-2b1a-4cf4-af8c-da76bae1685e"), "133292999980721241", 326.51f },
                    { 6, 5, "INR", new Guid("a6b7aa92-6cf0-494b-a671-2bd9b59e30cd"), "133292999980721258", 3721.11f },
                    { 7, 5, "JPY", new Guid("b035e7c6-eacc-4342-a15d-636add20f190"), "133292999980721273", 5503.05f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 6, "RSD", new Guid("0b7241ab-5e8a-4b55-a909-ba0636b8e8e5"), "133292999980721288", 7568.5f },
                    { 2, 6, "EUR", new Guid("c804ed01-ab50-48b7-a42d-64b7226f4bf8"), "133292999980721302", 64.33f },
                    { 3, 6, "GBP", new Guid("7f1ba9d8-7e85-4589-bd64-5ac1ccc9b48d"), "133292999980721316", 54.49f },
                    { 4, 6, "USD", new Guid("1a8fea10-7848-426d-8c45-87e04e84a894"), "133292999980721333", 73.41f },
                    { 5, 6, "RMB", new Guid("03451778-f9f5-49b2-bc69-0898b4f3d60a"), "133292999980721348", 471.52f },
                    { 6, 6, "INR", new Guid("b912dd5c-60f3-4c75-a5da-7c2badfca81d"), "133292999980721363", 5373.63f },
                    { 7, 6, "JPY", new Guid("3fdb9242-b388-478e-a942-d3736946682b"), "133292999980721377", 7946.92f },
                    { 1, 7, "RSD", new Guid("21e934b2-1474-4923-b18a-5467c1ef370b"), "133292999980721391", 1953.75f },
                    { 2, 7, "EUR", new Guid("014949bb-2579-4da7-b407-319536c0349f"), "133292999980721406", 16.61f },
                    { 3, 7, "GBP", new Guid("4fdc61a8-465b-4ece-bfc3-bd4a81e8be29"), "133292999980721420", 14.07f },
                    { 4, 7, "USD", new Guid("c4f93562-743e-4484-a65f-f79818ce9d4c"), "133292999980721435", 18.95f },
                    { 5, 7, "RMB", new Guid("7a8aa4ed-5f12-42e1-a046-a7aa0fae6d48"), "133292999980721451", 121.72f },
                    { 6, 7, "INR", new Guid("9f50505c-c74c-4e4d-9ee0-09330ed5e299"), "133292999980721496", 1387.16f },
                    { 7, 7, "JPY", new Guid("d0a64a41-5e60-4f96-b449-9330dfe18464"), "133292999980721513", 2051.44f },
                    { 1, 8, "RSD", new Guid("6ad103b7-8f32-40fc-82fd-171b04897904"), "133292999980721527", 6546.5f },
                    { 2, 8, "EUR", new Guid("c7e1a4e7-82d5-4c50-a869-fd80b953c24c"), "133292999980721542", 55.65f },
                    { 3, 8, "GBP", new Guid("0e5a0367-1701-432e-994a-442e06d1bc9c"), "133292999980721556", 47.13f },
                    { 4, 8, "USD", new Guid("725d13c6-5100-4f9e-8ab5-caba2127a0ec"), "133292999980721571", 63.5f },
                    { 5, 8, "RMB", new Guid("7da968b3-a5be-4aac-bdb3-872fef67cd5e"), "133292999980721585", 407.85f },
                    { 6, 8, "INR", new Guid("7f68c711-533c-4332-80da-174848f2c663"), "133292999980721602", 4648.01f },
                    { 7, 8, "JPY", new Guid("4fe316bb-11eb-4ba8-bdf5-3f7419a65f06"), "133292999980721617", 6873.82f },
                    { 1, 9, "RSD", new Guid("37090441-d799-48f1-bc99-9784352fc897"), "133292999980721631", 4037f },
                    { 2, 9, "EUR", new Guid("feb9f074-9fca-4857-bc68-ecd54ec4bf4f"), "133292999980721645", 34.31f },
                    { 3, 9, "GBP", new Guid("496b0b6e-ffdb-4145-9a4e-073e7f1dd1b5"), "133292999980721660", 29.07f },
                    { 4, 9, "USD", new Guid("70b7ad75-ecb1-4f19-aa55-eaf6aa5c8631"), "133292999980721674", 39.16f },
                    { 5, 9, "RMB", new Guid("12576be8-4d66-4092-a2f1-fcbe1c7307b3"), "133292999980721688", 251.51f },
                    { 6, 9, "INR", new Guid("88aa1328-09d6-4d06-aeba-cd32845c5b95"), "133292999980721703", 2866.27f },
                    { 7, 9, "JPY", new Guid("9660fe4c-d9c0-4734-a27d-35173adfd904"), "133292999980721719", 4238.85f },
                    { 1, 10, "RSD", new Guid("f7bc172c-c168-4bb5-95b8-c2d92fc6879a"), "133292999980721734", 8614.5f },
                    { 2, 10, "EUR", new Guid("ff47f22c-74c1-4c74-991e-b30db786a75a"), "133292999980721749", 73.22f },
                    { 3, 10, "GBP", new Guid("a6404d15-0843-4554-a662-2b82caf966fb"), "133292999980721766", 62.02f },
                    { 4, 10, "USD", new Guid("4eeccd4c-e634-4f64-8a0d-b297ab501d71"), "133292999980721781", 83.56f },
                    { 5, 10, "RMB", new Guid("3948d40e-86f3-4614-9b19-0167ef81ca40"), "133292999980721828", 536.68f },
                    { 6, 10, "INR", new Guid("5ca4e366-62ce-41c3-8d3f-f6d9d5362137"), "133292999980721843", 6116.29f },
                    { 7, 10, "JPY", new Guid("69e372f3-b9db-41e4-9e94-0fea3458e55d"), "133292999980721858", 9045.22f },
                    { 1, 11, "RSD", new Guid("c251f0d2-5a5d-433b-bae1-4284e4e82470"), "133292999980721875", 1386.5f },
                    { 2, 11, "EUR", new Guid("5e956fc6-bc22-455b-a5df-5ec658393369"), "133292999980721890", 11.79f },
                    { 3, 11, "GBP", new Guid("5092d6f3-e495-4e53-a85b-ef4398f64d10"), "133292999980721905", 9.98f },
                    { 4, 11, "USD", new Guid("52f711e3-44b3-4bf0-a6cc-64a9b2556b64"), "133292999980721919", 13.45f },
                    { 5, 11, "RMB", new Guid("e534347d-a3c3-4b2f-8c7e-6e134d463ab3"), "133292999980721933", 86.38f },
                    { 6, 11, "INR", new Guid("da843a2e-cf9a-4186-8724-b28b1e511a42"), "133292999980721947", 984.41f },
                    { 7, 11, "JPY", new Guid("9eab86be-234d-472b-a739-247016776536"), "133292999980721962", 1455.82f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 12, "RSD", new Guid("f3b95d34-964c-44e9-8abb-aae98c1e004f"), "133292999980721976", 5970.5f },
                    { 2, 12, "EUR", new Guid("36de7af3-6d7f-4b8a-a0a9-9fa9050e0cd1"), "133292999980721992", 50.75f },
                    { 3, 12, "GBP", new Guid("e3a836fa-4331-4f94-8581-43021fb8d797"), "133292999980722007", 42.99f },
                    { 4, 12, "USD", new Guid("361fe552-1ef3-405e-8910-fa69f8d1c2ce"), "133292999980722022", 57.91f },
                    { 5, 12, "RMB", new Guid("b94f089a-8bf9-4115-af6e-692306c36872"), "133292999980722036", 371.96f },
                    { 6, 12, "INR", new Guid("3ba4903e-ed22-42af-a116-1eafab088ef7"), "133292999980722050", 4239.05f },
                    { 7, 12, "JPY", new Guid("882a8602-5c81-4548-a434-4e02ee5b246e"), "133292999980722065", 6269.02f },
                    { 1, 13, "RSD", new Guid("43b43cc3-2a6d-4708-816a-1cf52ff88a35"), "133292999980722079", 3177.25f },
                    { 2, 13, "EUR", new Guid("77bb1caa-ae89-426d-8214-f71a8acf4f0b"), "133292999980722094", 27.01f },
                    { 3, 13, "GBP", new Guid("dce9b765-b3b2-46cd-ab06-56b327271db1"), "133292999980722110", 22.88f },
                    { 4, 13, "USD", new Guid("785a1912-970b-4bd0-ab54-d3d41d1067ea"), "133292999980722125", 30.82f },
                    { 5, 13, "RMB", new Guid("709fe605-1cc1-4822-a8a4-45dee6712cd4"), "133292999980722139", 197.94f },
                    { 6, 13, "INR", new Guid("47a312b9-63c8-4061-8fac-7c6f9eaca73c"), "133292999980722186", 2255.85f },
                    { 7, 13, "JPY", new Guid("70c92ac6-6691-4a49-a836-ad0b2d220292"), "133292999980722201", 3336.11f },
                    { 1, 14, "RSD", new Guid("990381a2-c834-4d59-927a-34d9b157d22f"), "133292999980722215", 9264.75f },
                    { 2, 14, "EUR", new Guid("7f61a7d1-87c1-43c0-a4cd-b679f57fb6a6"), "133292999980722230", 78.75f },
                    { 3, 14, "GBP", new Guid("bff9591d-85a2-4a50-90e7-b06fc484fc60"), "133292999980722244", 66.71f },
                    { 4, 14, "USD", new Guid("fd4bfccc-6b76-4127-a598-e7a9a164c181"), "133292999980722261", 89.87f },
                    { 5, 14, "RMB", new Guid("94301659-8cac-4450-a4a8-f3597f8a4163"), "133292999980722276", 577.19f },
                    { 6, 14, "INR", new Guid("19d8044a-0cd3-41ae-b683-fcf4645f2a94"), "133292999980722290", 6577.97f },
                    { 7, 14, "JPY", new Guid("52725a9f-60c9-4bf1-ae0e-9a0cec3dd751"), "133292999980722305", 9727.99f },
                    { 1, 15, "RSD", new Guid("e355e619-c160-490d-a00c-4c1ec4ffc31a"), "133292999980722319", 7812f },
                    { 2, 15, "EUR", new Guid("d1b79e57-d9b9-4e59-adf2-df43858ac256"), "133292999980722333", 66.4f },
                    { 3, 15, "GBP", new Guid("42a3ccac-c505-4bb8-aa53-ff34ee417b6f"), "133292999980722348", 56.25f },
                    { 4, 15, "USD", new Guid("c7f79a26-bf55-4b85-b41c-d9b26462de3d"), "133292999980722362", 75.78f },
                    { 5, 15, "RMB", new Guid("2ca62308-5e53-40a9-8aad-ce21a82d6706"), "133292999980722379", 486.69f },
                    { 6, 15, "INR", new Guid("3128581d-5bc0-49ea-b73c-9ec6963ae31f"), "133292999980722394", 5546.52f },
                    { 7, 15, "JPY", new Guid("8a2c2497-2df9-4c8a-9c72-3cc65a6b83f5"), "133292999980722408", 8202.6f },
                    { 1, 16, "RSD", new Guid("143f786b-c169-4394-aa11-3da3521bc909"), "133292999980722423", 2985.25f },
                    { 2, 16, "EUR", new Guid("83ff4c93-9aea-4321-b3ab-169cfe9cf4c9"), "133292999980722437", 25.37f },
                    { 3, 16, "GBP", new Guid("815abdf1-3a97-4e71-91cd-e91f9f9baf79"), "133292999980722451", 21.49f },
                    { 4, 16, "USD", new Guid("24ff4ad2-ccec-447a-a881-399cfbac1257"), "133292999980722466", 28.96f },
                    { 5, 16, "RMB", new Guid("d0b04245-81fc-474e-84a7-ea8370a0deb4"), "133292999980722480", 185.98f },
                    { 6, 16, "INR", new Guid("1620c87c-6de9-4d2f-9275-66db7fb2eac0"), "133292999980722497", 2119.53f },
                    { 7, 16, "JPY", new Guid("8e9e54ad-9096-4f33-9531-20e969cd9867"), "133292999980722542", 3134.51f }
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
