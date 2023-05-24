using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levi9.ERP.Domain.Migrations
{
    public partial class InitMig : Migration
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
                    { 8, new Guid("4a3cf575-b6bf-416a-84d3-72f9be1530c9"), "634792557112051697", "RSD 2 Price List" },
                    { 9, new Guid("3cb95d3c-12c1-4d85-99d1-6e115de1ae7b"), "634792557112051698", "EUR 2 Price List" },
                    { 10, new Guid("dfec18a3-c756-4a65-a680-05f280f3cf72"), "634792557112051699", "GBP 2 Price List" },
                    { 11, new Guid("7212f0f2-22c0-4a00-8e35-1edb1193c6e1"), "634792557112051701", "USD 2 Price List" },
                    { 12, new Guid("3c798b1b-849d-41c9-b8dd-916f542e170c"), "634792557112051702", "RMB 2 Price List" },
                    { 13, new Guid("b7558651-e751-4385-b6e9-a3574692f71b"), "634792557112051703", "INR 2 Price List" },
                    { 14, new Guid("86377c6d-1075-40fc-a49b-35c4cca9b53c"), "634792557112051704", "JPY 2 Price List" }
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
                    { 1, 1, "RSD", new Guid("023aa3ac-69f0-4aa8-a4ce-52ccd3029fe8"), "133294124516592237", 9750.25f },
                    { 2, 1, "EUR", new Guid("ab68f406-34ba-434e-b504-0ccdd7ba099b"), "133294124516592314", 82.88f },
                    { 3, 1, "GBP", new Guid("b8a44f0a-b047-4281-9de3-f94969f64fa8"), "133294124516592332", 70.2f },
                    { 4, 1, "USD", new Guid("d33a6183-be25-4cf3-b5da-ca9d05ffd062"), "133294124516592356", 94.58f },
                    { 5, 1, "RMB", new Guid("e664be1f-7773-43cf-986b-6bc2dfc23d13"), "133294124516592373", 607.44f },
                    { 6, 1, "INR", new Guid("1fb497a5-3119-4518-ae06-f0cb5168d33c"), "133294124516592390", 6922.68f },
                    { 7, 1, "JPY", new Guid("7a3568cb-5fb7-45f0-a4d5-dabd85739ef6"), "133294124516592405", 10237.76f },
                    { 1, 2, "RSD", new Guid("a5f267a8-f56e-422b-9738-ddcb55fa93d8"), "133294124516592421", 4121.5f },
                    { 2, 2, "EUR", new Guid("98e7f4ce-fee5-4cdb-8961-64c5a5b75d6d"), "133294124516592437", 35.03f },
                    { 3, 2, "GBP", new Guid("f3f153e6-74f7-4fa4-8f3c-e5843c94e337"), "133294124516592453", 29.67f },
                    { 4, 2, "USD", new Guid("ebebca2b-2460-4429-af09-b16eeaf21c21"), "133294124516592468", 39.98f },
                    { 5, 2, "RMB", new Guid("08f470d9-14ca-4f83-b213-d7b67b710bfb"), "133294124516592485", 256.77f },
                    { 6, 2, "INR", new Guid("66436012-e7c3-41ce-b50d-4c253cb9aea4"), "133294124516592500", 2926.26f },
                    { 7, 2, "JPY", new Guid("8c403570-c2a6-43f1-bd07-84df317d2b78"), "133294124516592515", 4327.57f },
                    { 1, 3, "RSD", new Guid("b8d13016-21aa-43e7-924f-d2c3f17fe1e8"), "133294124516592530", 6383.75f },
                    { 2, 3, "EUR", new Guid("8d173d67-1b37-4be7-98a2-9e044adb034d"), "133294124516592545", 54.26f },
                    { 3, 3, "GBP", new Guid("14c55ef1-dcd6-42ec-9403-d5de90e84a43"), "133294124516592559", 45.96f },
                    { 4, 3, "USD", new Guid("ca7dff79-3e56-47c0-8853-efde6f2d4e85"), "133294124516592575", 61.92f },
                    { 5, 3, "RMB", new Guid("a6fc1026-1a84-4aad-b5b0-4e3de9fea902"), "133294124516592623", 397.71f },
                    { 6, 3, "INR", new Guid("75b58041-8122-489e-bfdc-a1cf2734efb0"), "133294124516592642", 4532.46f },
                    { 7, 3, "JPY", new Guid("a14f1295-7d14-4811-9dd8-2a26eb54b9b0"), "133294124516592657", 6702.94f },
                    { 1, 4, "RSD", new Guid("0cd16cdb-c96f-4599-a2ec-a7d26d0b150c"), "133294124516592671", 8925f },
                    { 2, 4, "EUR", new Guid("c8df9e58-c66b-49df-9e9c-723261bdee38"), "133294124516592686", 75.86f },
                    { 3, 4, "GBP", new Guid("2035d506-24ed-4f5a-ac44-546b43d1c0f5"), "133294124516592700", 64.26f },
                    { 4, 4, "USD", new Guid("1ea448b3-09fd-4c68-a7f2-85806c72d095"), "133294124516592714", 86.57f },
                    { 5, 4, "RMB", new Guid("628a501f-f5bb-43e5-87fb-48e6cb13f53b"), "133294124516592728", 556.03f },
                    { 6, 4, "INR", new Guid("887b38c1-7274-4a39-b7d3-c7fe2efc7c09"), "133294124516592743", 6336.75f },
                    { 7, 4, "JPY", new Guid("76ce0b62-a52c-46a8-99c3-ec2e0e51f1a1"), "133294124516592759", 9371.25f },
                    { 1, 5, "RSD", new Guid("6d8dadcd-ea15-4590-87e3-d535d769d7e7"), "133294124516592773", 5241f },
                    { 2, 5, "EUR", new Guid("55c3eca9-8ad2-4c7f-9fdc-67d9237abf5c"), "133294124516592788", 44.55f },
                    { 3, 5, "GBP", new Guid("0dd3018d-21cd-4e84-98e3-a915010c98de"), "133294124516592802", 37.74f },
                    { 4, 5, "USD", new Guid("0d744046-f80a-462c-8da6-f04ea1495ba1"), "133294124516592816", 50.84f },
                    { 5, 5, "RMB", new Guid("762f69bb-d8bd-458a-8a27-9c23c2e7af82"), "133294124516592831", 326.51f },
                    { 6, 5, "INR", new Guid("9bcd7e0e-4833-4c1d-88da-9056d433aad0"), "133294124516592847", 3721.11f },
                    { 7, 5, "JPY", new Guid("27f2aa3a-3cab-4365-999a-fc8123abea75"), "133294124516592862", 5503.05f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 6, "RSD", new Guid("ec9135ae-fdee-4a5b-bd42-0cb2bd99b144"), "133294124516592878", 7568.5f },
                    { 2, 6, "EUR", new Guid("6a26ead0-6c7d-4b7e-ad9f-13f7426e727a"), "133294124516592893", 64.33f },
                    { 3, 6, "GBP", new Guid("7c84f7a8-b45e-463b-a34d-c636c3f7db62"), "133294124516592907", 54.49f },
                    { 4, 6, "USD", new Guid("baa48451-f1ec-498e-b213-d0dbdac9a51f"), "133294124516592921", 73.41f },
                    { 5, 6, "RMB", new Guid("0720c017-93fa-4e52-8466-f88fc5acce85"), "133294124516592969", 471.52f },
                    { 6, 6, "INR", new Guid("60d080d2-38ad-4247-a7a7-ade2c47829f2"), "133294124516592985", 5373.63f },
                    { 7, 6, "JPY", new Guid("5ccd86fb-e1a0-43b8-a59d-a00f6848d32e"), "133294124516593000", 7946.92f },
                    { 1, 7, "RSD", new Guid("029668e4-e6e9-444b-b113-ba28b5979c2f"), "133294124516593014", 1953.75f },
                    { 2, 7, "EUR", new Guid("0d1c70f3-ebab-4fbb-ae8d-b176e256ec3d"), "133294124516593030", 16.61f },
                    { 3, 7, "GBP", new Guid("898bdb73-41b2-4353-bfe4-0506d0575ba2"), "133294124516593046", 14.07f },
                    { 4, 7, "USD", new Guid("6f5c55db-de12-45b7-9de5-fdc8eec6e07d"), "133294124516593060", 18.95f },
                    { 5, 7, "RMB", new Guid("f711a2ee-806f-4bbb-952e-d81064a44f4f"), "133294124516593074", 121.72f },
                    { 6, 7, "INR", new Guid("9f03ab39-9664-4209-bea4-f9d6c6770ac1"), "133294124516593088", 1387.16f },
                    { 7, 7, "JPY", new Guid("8802426d-967f-4813-9e2a-97755e1ddfc3"), "133294124516593103", 2051.44f },
                    { 1, 8, "RSD", new Guid("4b43344f-0909-40c9-9036-75dca636d96a"), "133294124516593117", 6546.5f },
                    { 2, 8, "EUR", new Guid("f971e36b-5476-42a1-8742-a707478e0d26"), "133294124516593131", 55.65f },
                    { 3, 8, "GBP", new Guid("190e4445-d006-4ae4-9641-5a2be4b2ee50"), "133294124516593147", 47.13f },
                    { 4, 8, "USD", new Guid("0c51b2e4-4501-448f-8402-8c2b7251814e"), "133294124516593162", 63.5f },
                    { 5, 8, "RMB", new Guid("82a95b38-3ec8-47cf-96fe-7b4aaa1bb1cf"), "133294124516593177", 407.85f },
                    { 6, 8, "INR", new Guid("8f7b5451-422f-4e2b-bd7e-2606c74fa13f"), "133294124516593191", 4648.01f },
                    { 7, 8, "JPY", new Guid("1c2e4499-7e90-4c50-be3d-217171236398"), "133294124516593205", 6873.82f },
                    { 1, 9, "RSD", new Guid("937d337d-eae6-4eda-9e8e-a3d1143c044a"), "133294124516593220", 4037f },
                    { 2, 9, "EUR", new Guid("a4b5e0c8-1543-4b44-8652-d57fb6ef648f"), "133294124516593234", 34.31f },
                    { 3, 9, "GBP", new Guid("5b68d6bb-5ead-4f9b-92ad-dc5b75b943bf"), "133294124516593249", 29.07f },
                    { 4, 9, "USD", new Guid("b2e3258c-f504-4ee1-a83b-7dd262c7a9b3"), "133294124516593264", 39.16f },
                    { 5, 9, "RMB", new Guid("fd9b0556-3ed1-4c2a-afaf-198923f54370"), "133294124516593279", 251.51f },
                    { 6, 9, "INR", new Guid("b5fd513f-5e4d-454b-b4b6-e973f0223ffd"), "133294124516593325", 2866.27f },
                    { 7, 9, "JPY", new Guid("e7297730-6851-474a-8b3d-07ebf15b3fbf"), "133294124516593341", 4238.85f },
                    { 1, 10, "RSD", new Guid("9f4787d1-eebe-437a-9d00-5ee92a584050"), "133294124516593355", 8614.5f },
                    { 2, 10, "EUR", new Guid("76f3ce67-6013-4487-85c7-d281202bd256"), "133294124516593369", 73.22f },
                    { 3, 10, "GBP", new Guid("9ca1eef3-d7ac-43b1-8e3f-5e545a0a4b64"), "133294124516593386", 62.02f },
                    { 4, 10, "USD", new Guid("75f9de70-e17c-47ec-b0f7-747931e6e4b2"), "133294124516593400", 83.56f },
                    { 5, 10, "RMB", new Guid("44e01457-8048-42b9-bf4a-cf7b3d88933c"), "133294124516593417", 536.68f },
                    { 6, 10, "INR", new Guid("0751fb76-3800-49fb-9e27-144ce75e2957"), "133294124516593431", 6116.29f },
                    { 7, 10, "JPY", new Guid("875c248d-ae7a-465e-8d81-48cc9abb1cbf"), "133294124516593446", 9045.22f },
                    { 1, 11, "RSD", new Guid("ed5f78b6-1529-4ad9-be36-04759f1df9f7"), "133294124516593461", 1386.5f },
                    { 2, 11, "EUR", new Guid("0be11327-7f12-40ee-a0e0-4035a2e8cab2"), "133294124516593475", 11.79f },
                    { 3, 11, "GBP", new Guid("0fcb4b4c-a774-45dd-a5cd-e8cf5e81809c"), "133294124516593490", 9.98f },
                    { 4, 11, "USD", new Guid("71d46f26-a754-4f1e-a6fb-9b382c29e720"), "133294124516593504", 13.45f },
                    { 5, 11, "RMB", new Guid("46cfab78-f1d3-4806-8f42-16629d48c1ac"), "133294124516593518", 86.38f },
                    { 6, 11, "INR", new Guid("3038feb2-f4df-4e74-9696-13772386a8be"), "133294124516593534", 984.41f },
                    { 7, 11, "JPY", new Guid("9119a34d-9ca6-4b33-8e27-497c15ba94a1"), "133294124516593549", 1455.82f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 12, "RSD", new Guid("5531e261-4d61-4dd5-a0f1-d785836429bd"), "133294124516593563", 5970.5f },
                    { 2, 12, "EUR", new Guid("7013ab56-0cb7-44cf-b2e7-68dffa5f73f7"), "133294124516593577", 50.75f },
                    { 3, 12, "GBP", new Guid("c4f860dc-f537-4ddf-ae16-c0b4a3c045a8"), "133294124516593592", 42.99f },
                    { 4, 12, "USD", new Guid("41ee6aa6-ed8c-4f58-a250-4c8b733c19ae"), "133294124516593639", 57.91f },
                    { 5, 12, "RMB", new Guid("1064cfcd-e796-4a7a-b309-1731c5f70610"), "133294124516593656", 371.96f },
                    { 6, 12, "INR", new Guid("4b23c446-48b9-4c93-94e5-a955ac3f7461"), "133294124516593670", 4239.05f },
                    { 7, 12, "JPY", new Guid("0108f0ec-5c8e-41d1-97d8-ceb2ef00d04b"), "133294124516593686", 6269.02f },
                    { 1, 13, "RSD", new Guid("9ff506ab-0c39-47a5-bb4a-ac18cadf5b49"), "133294124516593701", 3177.25f },
                    { 2, 13, "EUR", new Guid("2f4963d8-ab85-4988-91db-bbfa084bf84f"), "133294124516593715", 27.01f },
                    { 3, 13, "GBP", new Guid("d568839a-71dd-477f-93ca-9e114f61b8cc"), "133294124516593729", 22.88f },
                    { 4, 13, "USD", new Guid("a5ec2228-b3e7-4492-81ed-d13a1d5bf3c5"), "133294124516593744", 30.82f },
                    { 5, 13, "RMB", new Guid("279b9e2b-5636-46c5-9449-0d84e51517c8"), "133294124516593758", 197.94f },
                    { 6, 13, "INR", new Guid("2dc695a6-fe5a-4f45-ab71-3c1ff48fc8e0"), "133294124516593772", 2255.85f },
                    { 7, 13, "JPY", new Guid("d41176ad-ce27-4e45-840e-0f28de3d7a84"), "133294124516593786", 3336.11f },
                    { 1, 14, "RSD", new Guid("0a1f6069-266d-4bf6-923e-88643c5f469c"), "133294124516593802", 9264.75f },
                    { 2, 14, "EUR", new Guid("5389e098-792c-4631-9ddb-add32b52296e"), "133294124516593817", 78.75f },
                    { 3, 14, "GBP", new Guid("e0f5ad1d-78ba-4dcb-ac1c-3190ee715a2a"), "133294124516593832", 66.71f },
                    { 4, 14, "USD", new Guid("4d58e5ab-df16-411b-9145-4f039efea11c"), "133294124516593846", 89.87f },
                    { 5, 14, "RMB", new Guid("ead22f44-573e-42be-92af-4ab80c338965"), "133294124516593860", 577.19f },
                    { 6, 14, "INR", new Guid("c6003f5d-e50c-4714-88d0-e9cfbf46d28d"), "133294124516593874", 6577.97f },
                    { 7, 14, "JPY", new Guid("efbbddca-1040-40d1-b5f7-27170918ed58"), "133294124516593889", 9727.99f },
                    { 1, 15, "RSD", new Guid("013a6fec-ee0d-4242-8239-6eab52cb4248"), "133294124516593903", 7812f },
                    { 2, 15, "EUR", new Guid("5c42be4f-5a3a-4760-949a-4ae178f42e3d"), "133294124516593919", 66.4f },
                    { 3, 15, "GBP", new Guid("59a1db88-358a-48f1-b611-25b9ffac0a28"), "133294124516593934", 56.25f },
                    { 4, 15, "USD", new Guid("fcdd5f3a-5031-47c8-9a1b-172d980c3fe7"), "133294124516593948", 75.78f },
                    { 5, 15, "RMB", new Guid("71d34311-3b81-44e6-8854-64e77e55a242"), "133294124516593992", 486.69f },
                    { 6, 15, "INR", new Guid("e55456ef-66d1-4c50-9e97-60eb765e3571"), "133294124516594010", 5546.52f },
                    { 7, 15, "JPY", new Guid("545837f3-6fc2-4786-b934-c868f38cec6c"), "133294124516594024", 8202.6f },
                    { 1, 16, "RSD", new Guid("65b987c7-f023-41f2-b8b4-b7bc799a596e"), "133294124516594039", 2985.25f },
                    { 2, 16, "EUR", new Guid("45d2338d-c6fc-42df-bf95-9e42fef344c2"), "133294124516594053", 25.37f },
                    { 3, 16, "GBP", new Guid("226d1b59-f1f3-4759-8fe3-c096808289c4"), "133294124516594070", 21.49f },
                    { 4, 16, "USD", new Guid("b9d8a18e-cdf3-4727-9ab3-f4a885a499fe"), "133294124516594084", 28.96f },
                    { 5, 16, "RMB", new Guid("74291caa-8218-4b44-b77f-dd4f33e67fd8"), "133294124516594099", 185.98f },
                    { 6, 16, "INR", new Guid("e4a09eb0-047c-4b8a-a89f-eac03b8575a6"), "133294124516594113", 2119.53f },
                    { 7, 16, "JPY", new Guid("ce84f6f4-66cc-4997-aa6e-1e7792a73ba6"), "133294124516594127", 3134.51f }
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
