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
                    { 8, new Guid("c5dee5e3-de21-4131-85e9-3b489abd7951"), "634792557112051697", "RSD 2 Price List" },
                    { 9, new Guid("36d47358-cfe8-470d-a4a0-0dab9fd9e11f"), "634792557112051698", "EUR 2 Price List" },
                    { 10, new Guid("765e9582-84fa-4690-9dc3-c0e2b8c9e8e7"), "634792557112051699", "GBP 2 Price List" },
                    { 11, new Guid("95a4a563-6683-4dd4-bb0d-9a0e6d49bbee"), "634792557112051701", "USD 2 Price List" },
                    { 12, new Guid("556fb74a-ead0-477b-9921-da0d8b0533cc"), "634792557112051702", "RMB 2 Price List" },
                    { 13, new Guid("1a871f54-4f1d-4ac0-b0a0-ce621c71c6f6"), "634792557112051703", "INR 2 Price List" },
                    { 14, new Guid("6a07e0ac-d8b3-4f7d-a20e-3882d4353613"), "634792557112051704", "JPY 2 Price List" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "GlobalId", "ImageUrl", "LastUpdate", "Name" },
                values: new object[,]
                {
                    { 1, 104, new Guid("6f68f25e-9645-4da9-b66f-8edbebb8a6e7"), "https://db3pap003files.storage.live.com/y4mbO0YJ-ovSlqOuXjgGPR2IsjN8sCk9ULksyzIkvLEJeRyYnqKUxHmuxppx-HMcp6oNzzOrMc6_6YRgmiaeA5_vxCRWCV9ARd_GK6hPLdp3tEcsgR7Us9uzAfvH331KiJrqlwCGiOPFDi_OsHLznB3tqbTk-bFfQIqQdyTkno03JxvSOQ8vwEjvCFJUa0Bx7IUBzg4bU_qzf3qpgZMeYI1y1rt9qCldOaIi3WgyU-E5J0?encodeFailures=1&width=500&height=500", "133294746178820303", "T-ShirtBlack" },
                    { 2, 138, new Guid("d68bce12-9f03-4e5f-b0b9-3ad1205afda4"), "https://db3pap003files.storage.live.com/y4m-kRT2GTNRM1fORXdHbHlvpzF8ne8_HsD3AoL-ytcD5vCq2S7olsPp-Nnw017SkIRz6yUxkIym7En4FjUFiENJ2BVJN0TWz9aqDHht8mlh5RrTwQ-ZBXD6Lm8fI9cy390BzcPCGZchRsm6dGkmTU3q3xZE-WU1VNKV-64xUJN02ofGt6w7I7vJxqaeaHh15q_ul04x3dCUumC53DWniI5oEilVornGdGyddxKc8xZoqw?encodeFailures=1&width=500&height=500", "133294746178820449", "T-ShirtBlue" },
                    { 3, 745, new Guid("2c103912-7b0f-4469-8769-3989a4d62bc2"), "https://db3pap003files.storage.live.com/y4mAsciDm3-1F9piIJKD-IzaefrUBqEbA6CPHpORsvcIBh6PK8Ze9TOhMEvKpBx0RhBUTOIP5AItxqcmLSMiYSz2g7KV8XG0jugLvNtkqC8R1DwnwOQRZHj3csgnd_iTL1qWPfzolxTny7naR0izqMtG3i-w3ziMWHQuEqzwiTc98QTHj8LfkRuMHhv72ZNttAQuoj3-Y2iHokZ4JsuU5DkNfzVpvAKZ4w5wgS5V99rOD8?encodeFailures=1&width=500&height=500", "133294746178820474", "T-ShirtBrown" },
                    { 4, 5, new Guid("a6c74b0b-b1f3-4e12-b6d2-6c38a3903ce6"), "https://db3pap003files.storage.live.com/y4mIMtsMKg4MteMPQCPtJgs6K1S-d2um3XXpYS8Du6_NWnd4lNDiHdlq6Eb524q2jskw_8_vshuYV5u-K6AO8mxW2llKQxnBEL01m5t-JNBM5LecnrYtGho2DiJipC6x4r3jD1VeW34xPSR1p_juV-1oqWdrrQUf8PbaDdD75jE3ar1KeD9yUlPhM5BUrCTjzaJusBxqVk0smMVMzQ7Bja62BV6-4jQ0W743Q1JiUNeOPA?encodeFailures=1&width=500&height=500", "133294746178820490", "T-ShirtGreen" },
                    { 5, 57, new Guid("8dbff2d4-42a0-4fe7-9864-05571f3c22db"), "https://db3pap003files.storage.live.com/y4m0eG0CddqsLAfGnFHIiySLlbn9TzPQ9pQOm7YcP9QXblbZJauurhybDbiJTbA4xb-obY2cjRUlaacpZakO8zQ1oDTZQrDaipf0vU9FiylYlxK64_CzrAuGrJenGE98iclH6BN9uWZH3k1V5XthDTzldVwtY2lvP_upoU-UQyNGfX0goTCeF3FnK7rHh5pwIPSQWwSeHNPserQ6skA9FWR0ehISp1axZbphMw6aYmQwhU?encodeFailures=1&width=500&height=500", "133294746178820506", "T-ShirtPurple" },
                    { 6, 224, new Guid("9f7c8b37-0e37-49ed-af1f-4e6a7428a873"), "https://db3pap003files.storage.live.com/y4mLQ8P5MJROOmihR3K_1SU4Y_Iisgc0y1DeC-ZizOELnj82YZeiGhXEY8cavf9xNTC12wtRKmUN42mQysBXyV066A8XFzE9XNScQNdflZhIhCKM0xUmgKHlCG6P9z3jIiqdxENbwxFU6dBr1UEpJpaZXWRHliG8nJNI1TttSsDw533HGJR5gCO9hpdlDDz4W0ltkmYE_oPT24JKDwnLNZyHZv6dfzI-qqzOrlBa5TnPQQ?encodeFailures=1&width=500&height=500", "133294746178820524", "T-ShirtRed" },
                    { 7, 150, new Guid("e0a01813-5f43-4a15-934f-9946ef8f4182"), "https://db3pap003files.storage.live.com/y4m0BTJrLTQNPzne4u5bX6n47paz88nldiOZnxawfhv4oJKFk8fP4TZR91HGDs43pSIw2wEXKbJq3cJ8pItmZ-tu9LSPBEpjbTo5jIuiMsFipvDwkZoT4r9C0DHCYD6vVjnRdGP0OgbxKBKGgtOXUlc3hMb3KalQVb5xlK1EC_ukMs5cLmBeHqGzEmI2SBe3zCyeoNgaf3GpysUv9NAcoG1ppL0FCXfJMJbM0zWUbcSfJ0?encodeFailures=1&width=500&height=500", "133294746178820543", "T-ShirtRose" },
                    { 8, 186, new Guid("156fd903-9fd2-41af-9b09-600d598b31c4"), "https://db3pap003files.storage.live.com/y4m8PzFIhNEKewi3wbogTAR2veLJqFbaOBgSNlzytwuowu0Lv2SIQxwPhsPe9EFmuc0N8lgEdowWmm29Cavcz6isxa4cpPVOr6jtl9BW5Ajv4lcvKSE4oXqNIMeElwiw53q6Bg84U_VG1c3ntS0UCXSMdo4iRDZJURcBbeIDv7GFCnRKcgMHQ6FD1md_owl3q55SGzBIEsFOpyMku_CcvDb77ttqpDhJ4YPF9CCbzQ8esk?encodeFailures=1&width=500&height=500", "133294746178820559", "T-ShirtYellow" },
                    { 9, 104, new Guid("6f8799a7-097b-45f4-a8f0-805784ee24a6"), "https://db3pap003files.storage.live.com/y4mcROrUsGJ1C_DaPGGRsunJPAdSjzSLWBSqcDa4cHQt2VRY9AZqbHg4T7EPdSV1btrGwXH7VAJuf61Ld1knMXgnsRqdUuLpQkQCtWFYohlEv9VAK3QZzHC6XaVQNugbmcDj03yOM09WghkBH2eDm_EV5MF8iXSjIOwAktBC9Qet-CY16VN2MSm7UFFgw94gEbasO_GBbkRvzfCwOmUcXICRMopHO6obWAB_ATRGjlLHO4?encodeFailures=1&width=500&height=500", "133294746178820574", "T-ShirtBlack 9 Gradient" },
                    { 10, 138, new Guid("49674251-6b45-437f-8c7a-34a0b1a34c01"), "https://db3pap003files.storage.live.com/y4myPaEj8lwOAp9D6FSSd6vqVOshe2DZNPeTkOqpYexCSHAtxzO73MHXGGEkScMtu8k1iYqm2lKRBn5DMz5f0zerCAOvzrd8pGwL_fmarBVTMSmopv0Vy1YV5FDpu1-hPgVG0zKhYbu0jy8x2n4_ZOpZTDjzcrwrJrE78DuISNhBLvF2SGidifBduSxqFfXd7DPF_2Gr4dIMcymuT-OBT2opZWcpARi9ccxq-vfz-KPnqA?encodeFailures=1&width=500&height=500", "133294746178820593", "T-ShirtBlue 9 Gradient" },
                    { 11, 745, new Guid("7b5ae8b9-12a0-4a19-9c37-943bc4cc5a3e"), "https://db3pap003files.storage.live.com/y4maMJZDwT_Bt_9lYoj4QEBJplPpSpox0xs8mlm5MJZp-e4mC1TTol3ATfgm9TGwDlCcYnXzdhSqaPeqkZJK90tgRR_KQHtctku28zVSo3D8Rf_462QClZiF4CC03XosaZsn0ZWDdq7KhWdYpC8hUpkJQf9bbvjeRgYJfdHr8yqhGO3ENu8lAwvwUIMRs9LaXDIBAaXurelBc7zK18wxVt8opgjC4MvmdhJC0rjqWefNk8?encodeFailures=1&width=500&height=500", "133294746178820608", "T-ShirtBrown 9 Gradient" },
                    { 12, 5, new Guid("097dbb41-2aa0-4fbf-8986-bdf8e4e0d4b9"), "https://db3pap003files.storage.live.com/y4mywnSFEOeNPAc3Dv5LG3QKmt8cnkjDcCHmNyhpMMvkFiSJlUvZkss9mSJAYld9eYyTO4ELiz4sMXc2pgLtrqqsjzrTfWHItrt9Vesn1xHDXi7ONiXvdldHGHQwBDoTr5yQhbvShlKJVTfpL0UM67KIKTjDH3c9MqAUc2S8bwTjmai-g_WeOH4BdyJZz41mDWGTS88odSwCfN94gatrXq-Nc4vr96zUOhvoWSJHQPm-rE?encodeFailures=1&width=500&height=500", "133294746178820626", "T-ShirtGreen 9 Gradient" },
                    { 13, 57, new Guid("5f8d15b2-4c4d-4e7a-8902-d78d3b4cb10b"), "https://db3pap003files.storage.live.com/y4mkHeVXwv9fF7d7rmBK_fJ0Ge3c07dLoVpzqCrrF5HIEFDpKXFGkoRnJ9WmXzt1-bsSG3J5gSP8C3Aj5p8GjTS_tnZrGMCj2oNJbYvMRbyJbAzA3Gy7ynLFEdddnq8cNHwwWB4Jh2k8988ufJ7H-p2oe5lr4oQRvshHBzuIRt52RNWaGspRAVow1L551awcDC_BfmD2se_Yg9guDVlDTOMqVvO9PEqlMP3B-3y_I9oIz0?encodeFailures=1&width=500&height=500", "133294746178820696", "T-ShirtPurple 9 Gradient" },
                    { 14, 224, new Guid("4d50363a-2dc4-4ef0-8bde-682758f0f801"), "https://db3pap003files.storage.live.com/y4mAuqUBcOB4e_6Pkv5WtCBhsOp6xQyB3dhrzYEuJsoMAijFJ6pSGth6esbBkAIU2JpgSDze6vTEC07JiJWAPnyjuL2PsE_bmP1XypvPO3HZP2uIDAThpD-nk4Ews_QeQd0TNNorcNqO6JqaEqIP9OpEEfkLnBcK5Q0FHBI3IxP6octHzAlY5_M-z93ypRdrhwBjEjnmpEBsZ7eeZwORwXxRa1nw2b-TwPSr6F6uZZ2MUs?encodeFailures=1&width=500&height=500", "133294746178820713", "T-ShirtRed 9 Gradient" },
                    { 15, 150, new Guid("1835ee0e-3275-4ff9-9d79-1a2e3b5e15e3"), "https://db3pap003files.storage.live.com/y4mUU4G3Dpx9xkjqniNIZeMWz-wr3t18PGCtpoQxOv522aU8IDNVk6-MWFtqONztKzH6BelSIR6NhOXklxW5CEhTct5nyscvSGwHujoHepodUBhtjuXXW2KudArDxA2g4ARoELCaWVCAvdAm46C7BnzDcbXRf4QBFvz-iUG9V-Nm7O1adpQ6gnF96Xkp2iyNrkGkpc0x2hJ06TVqNWDYM7XfrJooKEbFfYcllC7cy17X5E?encodeFailures=1&width=500&height=500", "133294746178820729", "T-ShirtRose 9 Gradient" },
                    { 16, 186, new Guid("e5e4a551-2b64-43fc-86e1-4c1c0f6880fe"), "https://db3pap003files.storage.live.com/y4m9Oo0CoS06pvC58i8iE_2_WfJE-rEuIMGRWZcbdjThze0gTjJBWWtaDGSsnxJmhCweSY3lvNc6ZYxLMV8zlKM5CTJ24-WbT4iLjCtOgYg_Oy3hxxhcZOTXkMefthHQhT2OxtSmUDKkQIf4shsWTIu81eHGEovGq63_UqM-gkXR0uqYZXZLfgmVR9cTgk4Hyt8dmweIrouQ_eGM4L7TS5xP3d74G3moTJ72Lzjv8XLbSo?encodeFailures=1&width=500&height=500", "133294746178820745", "T-ShirtYellow 9 Gradient" }
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
                    { 1, 1, "RSD", new Guid("259de7c2-fece-49ba-8300-c3e72918714c"), "133294746178820860", 9750.25f },
                    { 2, 1, "EUR", new Guid("3bf53f3b-04fe-4572-a524-0a7693d36a27"), "133294746178820885", 82.88f },
                    { 3, 1, "GBP", new Guid("ed1e8d0c-9650-4086-bc0d-99832bc546a0"), "133294746178820904", 70.2f },
                    { 4, 1, "USD", new Guid("e8cecd9f-811d-4f25-861a-40cae611e246"), "133294746178820923", 94.58f },
                    { 5, 1, "RMB", new Guid("18e434ea-e988-428d-a449-bfca22646955"), "133294746178820940", 607.44f },
                    { 6, 1, "INR", new Guid("3ddb025b-770f-41a2-99a1-b3297dcf1ea0"), "133294746178820960", 6922.68f },
                    { 7, 1, "JPY", new Guid("c15bc8fc-b5f4-4b22-a285-21b1f0f772b6"), "133294746178820991", 10237.76f },
                    { 1, 2, "RSD", new Guid("39a5163e-dce0-402b-8e5d-5c636f1792d2"), "133294746178821008", 4121.5f },
                    { 2, 2, "EUR", new Guid("4481890d-c9c9-42ad-8d31-8dec53cdbe3e"), "133294746178821024", 35.03f },
                    { 3, 2, "GBP", new Guid("e21c6464-4bdd-4c56-8378-a83d41866106"), "133294746178821043", 29.67f },
                    { 4, 2, "USD", new Guid("5b051a7d-d57b-4cbc-a442-fbe18e4fc97c"), "133294746178821061", 39.98f },
                    { 5, 2, "RMB", new Guid("85530d7c-49d2-4f1a-8554-ab79a0ced71d"), "133294746178821077", 256.77f },
                    { 6, 2, "INR", new Guid("31444b90-127b-444e-b0d4-312fde0e663f"), "133294746178821093", 2926.26f },
                    { 7, 2, "JPY", new Guid("3dcbda75-ba7e-4030-85c1-a76ec36033b1"), "133294746178821109", 4327.57f },
                    { 1, 3, "RSD", new Guid("1d86d346-a748-4676-891f-4f60c73262c7"), "133294746178821128", 6383.75f },
                    { 2, 3, "EUR", new Guid("a6243eee-892a-4615-850d-c2534c312905"), "133294746178821144", 54.26f },
                    { 3, 3, "GBP", new Guid("ec0e7fdc-cd91-49f7-bd24-d03151d710b7"), "133294746178821196", 45.96f },
                    { 4, 3, "USD", new Guid("4f945a4c-04f5-468b-95f4-268b14e69781"), "133294746178821216", 61.92f },
                    { 5, 3, "RMB", new Guid("6b24337e-a1fc-4317-93f9-d9667678e84d"), "133294746178821233", 397.71f },
                    { 6, 3, "INR", new Guid("c4dc2974-cdc9-480f-adab-90a1ff9f6e38"), "133294746178821248", 4532.46f },
                    { 7, 3, "JPY", new Guid("41f29f04-2ae9-43d6-9956-45811fd6c22f"), "133294746178821264", 6702.94f },
                    { 1, 4, "RSD", new Guid("82ae75b8-f573-4760-ab52-1f04aa63af21"), "133294746178821280", 8925f },
                    { 2, 4, "EUR", new Guid("742b33d9-d47f-4b43-89b5-26f0802dd0d6"), "133294746178821298", 75.86f },
                    { 3, 4, "GBP", new Guid("88029955-0ce7-4421-b1cc-ac2994507019"), "133294746178821315", 64.26f },
                    { 4, 4, "USD", new Guid("ed14876b-34f2-48df-9752-21441f8e5f88"), "133294746178821331", 86.57f },
                    { 5, 4, "RMB", new Guid("21b9e13f-67b2-4b8b-b52e-59bcf0202b7d"), "133294746178821346", 556.03f },
                    { 6, 4, "INR", new Guid("0b5d4289-a403-4716-85f2-1dcfff84668f"), "133294746178821362", 6336.75f },
                    { 7, 4, "JPY", new Guid("226404db-e71c-45a2-aa44-313826d83512"), "133294746178821378", 9371.25f },
                    { 1, 5, "RSD", new Guid("ee233c29-96bb-4566-87ce-07b74622f341"), "133294746178821394", 5241f },
                    { 2, 5, "EUR", new Guid("d86ab968-7775-4239-9f59-2ede5b29d763"), "133294746178821410", 44.55f },
                    { 3, 5, "GBP", new Guid("d4a24676-6b9e-4c72-ac41-af1eca57cc9a"), "133294746178821428", 37.74f },
                    { 4, 5, "USD", new Guid("a70a161b-1531-408b-9cab-d8923bc79f08"), "133294746178821444", 50.84f },
                    { 5, 5, "RMB", new Guid("2cdd5b5c-c7dd-45fd-a1c7-7a15acaf2139"), "133294746178821460", 326.51f },
                    { 6, 5, "INR", new Guid("0d5e11e9-c0c8-4acc-bd9a-302be6bd5d42"), "133294746178821478", 3721.11f },
                    { 7, 5, "JPY", new Guid("b48d6a32-4f01-4797-b084-ed460a47cefd"), "133294746178821494", 5503.05f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 6, "RSD", new Guid("7e2cf47a-7329-48ca-87b3-c036bc483339"), "133294746178821509", 7568.5f },
                    { 2, 6, "EUR", new Guid("1db67b4b-c13e-4611-a0f3-071b8649fb9a"), "133294746178821561", 64.33f },
                    { 3, 6, "GBP", new Guid("0732d02f-3672-4f63-8486-73b7a54954f0"), "133294746178821580", 54.49f },
                    { 4, 6, "USD", new Guid("3e53c50e-82ba-45aa-8647-c74ff961f30d"), "133294746178821599", 73.41f },
                    { 5, 6, "RMB", new Guid("12d47259-965a-4c86-b28b-5f1ec10e311b"), "133294746178821615", 471.52f },
                    { 6, 6, "INR", new Guid("d43b330f-3f2b-48c3-a535-3c196e79ffae"), "133294746178821631", 5373.63f },
                    { 7, 6, "JPY", new Guid("1298dcc6-1884-4bae-b669-e649c32d9eb1"), "133294746178821647", 7946.92f },
                    { 1, 7, "RSD", new Guid("d0f5eb45-ec89-4dd1-abc6-a012f08f7d68"), "133294746178821663", 1953.75f },
                    { 2, 7, "EUR", new Guid("e7ec76fa-9f7c-43f2-b570-5d8bfee90ab3"), "133294746178821678", 16.61f },
                    { 3, 7, "GBP", new Guid("c9c9dc61-bf26-47ec-9e8c-7607c7978379"), "133294746178821694", 14.07f },
                    { 4, 7, "USD", new Guid("86f17612-1662-49e9-8e74-418806d8b26b"), "133294746178821710", 18.95f },
                    { 5, 7, "RMB", new Guid("7110d2fb-7549-4631-bf01-46a8ff125616"), "133294746178821729", 121.72f },
                    { 6, 7, "INR", new Guid("ea2d083b-9c5b-4f44-9974-1e97d3196ce8"), "133294746178821745", 1387.16f },
                    { 7, 7, "JPY", new Guid("1b569417-93e8-43da-bacf-8ccb1d796b40"), "133294746178821760", 2051.44f },
                    { 1, 8, "RSD", new Guid("8e1d6bd7-1f11-4da3-ac65-9711325b76bc"), "133294746178821776", 6546.5f },
                    { 2, 8, "EUR", new Guid("bf144fe2-5eb4-4076-a9c6-4629aab44950"), "133294746178821792", 55.65f },
                    { 3, 8, "GBP", new Guid("005f5645-9e8b-4308-af00-b7fe1a57f4f6"), "133294746178821808", 47.13f },
                    { 4, 8, "USD", new Guid("d13a356d-550c-4fef-bb26-7c005e5a8fcc"), "133294746178821824", 63.5f },
                    { 5, 8, "RMB", new Guid("0016ab4e-46ee-490d-bd51-689b6c3adeb4"), "133294746178821839", 407.85f },
                    { 6, 8, "INR", new Guid("51a77025-b7e0-4561-9cd1-7a46a69c1d70"), "133294746178821857", 4648.01f },
                    { 7, 8, "JPY", new Guid("8644ea79-6190-4aec-83f8-a6fcf538312e"), "133294746178821873", 6873.82f },
                    { 1, 9, "RSD", new Guid("4741a7f7-8964-439d-be05-56cc30d44b90"), "133294746178821889", 4037f },
                    { 2, 9, "EUR", new Guid("0287fd91-91da-4940-84b9-0dbce01f4635"), "133294746178821904", 34.31f },
                    { 3, 9, "GBP", new Guid("5c04bbae-792b-465d-ae04-36ce4d9ba4ae"), "133294746178821920", 29.07f },
                    { 4, 9, "USD", new Guid("ba4bf892-ce1b-4f83-be7a-4523bd8a709c"), "133294746178822007", 39.16f },
                    { 5, 9, "RMB", new Guid("c3d319b9-15b6-46d3-8751-feb094ca22d6"), "133294746178822025", 251.51f },
                    { 6, 9, "INR", new Guid("49ae66c6-813c-46a9-8ded-d6945fab0f9b"), "133294746178822041", 2866.27f },
                    { 7, 9, "JPY", new Guid("a61c413d-8789-4d71-80cb-d892ddc05c99"), "133294746178822059", 4238.85f },
                    { 1, 10, "RSD", new Guid("ed5d7111-3de9-4698-b5a4-e4dbdc3ac6ea"), "133294746178822075", 8614.5f },
                    { 2, 10, "EUR", new Guid("c392bd35-25ec-4416-b32d-931ef0920913"), "133294746178822091", 73.22f },
                    { 3, 10, "GBP", new Guid("d088f778-695d-4bc4-8af8-b5fbaeb34335"), "133294746178822111", 62.02f },
                    { 4, 10, "USD", new Guid("19a4305f-5442-40ed-b952-c12c934aac04"), "133294746178822127", 83.56f },
                    { 5, 10, "RMB", new Guid("cafca3a9-40bf-4778-a3f6-c89f2f4fc7db"), "133294746178822143", 536.68f },
                    { 6, 10, "INR", new Guid("5758dbae-4eef-43aa-b579-14afd951c018"), "133294746178822158", 6116.29f },
                    { 7, 10, "JPY", new Guid("01411ce2-075d-490d-be2b-f558b42f9c7b"), "133294746178822173", 9045.22f },
                    { 1, 11, "RSD", new Guid("282d04c1-9e1f-4a9c-aaff-4cabe22fba1e"), "133294746178822191", 1386.5f },
                    { 2, 11, "EUR", new Guid("93985464-4c4d-4d58-93fa-a8ac0a4bb502"), "133294746178822207", 11.79f },
                    { 3, 11, "GBP", new Guid("0d9909b8-c8f7-4440-b4bc-92e8461a9319"), "133294746178822223", 9.98f },
                    { 4, 11, "USD", new Guid("cb289678-989c-47ec-9dd5-59c33704f83b"), "133294746178822238", 13.45f },
                    { 5, 11, "RMB", new Guid("50502c12-5390-4a8b-8c8d-199f47f6a617"), "133294746178822254", 86.38f },
                    { 6, 11, "INR", new Guid("f4275450-d03b-4555-add8-15d1322b38bb"), "133294746178822269", 984.41f },
                    { 7, 11, "JPY", new Guid("5129baaa-e661-4099-9064-0868cdd6e669"), "133294746178822285", 1455.82f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 12, "RSD", new Guid("6318b95a-44c7-4d3b-bb00-4082c8a2afed"), "133294746178822300", 5970.5f },
                    { 2, 12, "EUR", new Guid("d93389ed-6ba5-4665-abd6-5d2655dda8cf"), "133294746178822353", 50.75f },
                    { 3, 12, "GBP", new Guid("9ec7c73e-285f-4fd4-bda9-7b7db9f45204"), "133294746178822371", 42.99f },
                    { 4, 12, "USD", new Guid("99754951-fb56-4746-8a56-62b6e58f2893"), "133294746178822386", 57.91f },
                    { 5, 12, "RMB", new Guid("f782d4bc-56ac-4186-9219-ac2061588316"), "133294746178822402", 371.96f },
                    { 6, 12, "INR", new Guid("c4dd33a4-6614-42ab-8ed7-4c9c8b86f9f6"), "133294746178822417", 4239.05f },
                    { 7, 12, "JPY", new Guid("3e84492b-7f6a-4822-80c6-a8a463076da3"), "133294746178822432", 6269.02f },
                    { 1, 13, "RSD", new Guid("8a5db2dd-6f59-4939-874c-913d07c9b7c5"), "133294746178822448", 3177.25f },
                    { 2, 13, "EUR", new Guid("33ac43ab-67bc-44c7-be45-db52e7cd114b"), "133294746178822464", 27.01f },
                    { 3, 13, "GBP", new Guid("874e68e2-2ecc-4280-8096-3c5859373e32"), "133294746178822482", 22.88f },
                    { 4, 13, "USD", new Guid("9ae1fc38-c936-4a3d-90bc-e148867a950c"), "133294746178822498", 30.82f },
                    { 5, 13, "RMB", new Guid("f0fcdf9f-af0d-46d3-bbb8-06d7c9024056"), "133294746178822514", 197.94f },
                    { 6, 13, "INR", new Guid("2192c8f4-7e0b-4c71-bbc6-144d78b31d50"), "133294746178822529", 2255.85f },
                    { 7, 13, "JPY", new Guid("fd1be633-d5c1-4607-8454-b31b98941656"), "133294746178822545", 3336.11f },
                    { 1, 14, "RSD", new Guid("da518f92-2294-4951-aeff-c047270fca0f"), "133294746178822560", 9264.75f },
                    { 2, 14, "EUR", new Guid("5779c83b-1609-428e-9a21-721db5b8a728"), "133294746178822576", 78.75f },
                    { 3, 14, "GBP", new Guid("899a85cf-e5bd-489a-baa9-16bd74adecea"), "133294746178822591", 66.71f },
                    { 4, 14, "USD", new Guid("0c00cdbf-934a-4a24-828a-4c0d5781f343"), "133294746178822609", 89.87f },
                    { 5, 14, "RMB", new Guid("28f3f012-b98d-443e-ab2a-1a576bdfd4af"), "133294746178822625", 577.19f },
                    { 6, 14, "INR", new Guid("328315f6-2f9e-463f-ab30-916bf0857981"), "133294746178822641", 6577.97f },
                    { 7, 14, "JPY", new Guid("2580a1de-40a2-4cdf-84ba-356fe4cf1ea6"), "133294746178822656", 9727.99f },
                    { 1, 15, "RSD", new Guid("9b7639f7-71e6-403f-a7b4-aaaa253b4abd"), "133294746178822672", 7812f },
                    { 2, 15, "EUR", new Guid("4c4e342c-d91d-4b08-9472-a79d8c2c89cb"), "133294746178822688", 66.4f },
                    { 3, 15, "GBP", new Guid("f32eecf4-8059-4ebb-b297-b7b7c2fbe5a0"), "133294746178822736", 56.25f },
                    { 4, 15, "USD", new Guid("cf2d03a6-948f-44f9-b76e-bb98d8120f5b"), "133294746178822754", 75.78f },
                    { 5, 15, "RMB", new Guid("db085095-7249-4e85-8213-607d129b3933"), "133294746178822773", 486.69f },
                    { 6, 15, "INR", new Guid("b7bcaa83-8693-4fd2-811e-1bf1dde835a1"), "133294746178822788", 5546.52f },
                    { 7, 15, "JPY", new Guid("340ad93b-b449-4ed7-8834-8f6e4daacf1e"), "133294746178822803", 8202.6f },
                    { 1, 16, "RSD", new Guid("942ed9e0-dd95-4f2a-8413-d9363a545aea"), "133294746178822818", 2985.25f },
                    { 2, 16, "EUR", new Guid("ca38dab5-11ea-4e0a-a4cc-cce32404dd5d"), "133294746178822834", 25.37f },
                    { 3, 16, "GBP", new Guid("9c17affe-3814-4ef6-b63a-fffb6a1b71b4"), "133294746178822849", 21.49f },
                    { 4, 16, "USD", new Guid("6930034e-9e10-4c54-97a3-210387592b43"), "133294746178822865", 28.96f },
                    { 5, 16, "RMB", new Guid("d7bd9d25-21b9-40d9-9626-5d56804edfed"), "133294746178822881", 185.98f },
                    { 6, 16, "INR", new Guid("bc7bbdf4-23f1-4407-95ec-4cec70b76eb5"), "133294746178822899", 2119.53f },
                    { 7, 16, "JPY", new Guid("9681ca72-cee9-43ed-9e6f-fd13cb88a686"), "133294746178822915", 3134.51f }
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
