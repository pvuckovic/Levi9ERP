using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levi9.ERP.Domain.Migrations
{
    public partial class Initial : Migration
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
                    { 7, new Guid("494b7014-8ee2-47c3-938f-2de7a43db41a"), "634792557112051696", "JPY Price List" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "GlobalId", "ImageUrl", "LastUpdate", "Name" },
                values: new object[,]
                {
                    { 1, 104, new Guid("6f68f25e-9645-4da9-b66f-8edbebb8a6e7"), "images/tshirtimage/T-ShirtBlack.png", "133293148993252618", "T-ShirtBlack" },
                    { 2, 138, new Guid("d68bce12-9f03-4e5f-b0b9-3ad1205afda4"), "images/tshirtimage/T-ShirtBlue.png", "133293148993252777", "T-ShirtBlue" },
                    { 3, 745, new Guid("2c103912-7b0f-4469-8769-3989a4d62bc2"), "images/tshirtimage/T-ShirtBrown.png", "133293148993252829", "T-ShirtBrown" },
                    { 4, 5, new Guid("a6c74b0b-b1f3-4e12-b6d2-6c38a3903ce6"), "images/tshirtimage/T-ShirtGreen.png", "133293148993252877", "T-ShirtGreen" },
                    { 5, 57, new Guid("8dbff2d4-42a0-4fe7-9864-05571f3c22db"), "images/tshirtimage/T-ShirtPurple.png", "133293148993252928", "T-ShirtPurple" },
                    { 6, 224, new Guid("9f7c8b37-0e37-49ed-af1f-4e6a7428a873"), "images/tshirtimage/T-ShirtRed.png", "133293148993252979", "T-ShirtRed" },
                    { 7, 150, new Guid("e0a01813-5f43-4a15-934f-9946ef8f4182"), "images/tshirtimage/T-ShirtRose.png", "133293148993253027", "T-ShirtRose" },
                    { 8, 186, new Guid("156fd903-9fd2-41af-9b09-600d598b31c4"), "images/tshirtimage/T-ShirtYellow.png", "133293148993253075", "T-ShirtYellow" },
                    { 9, 104, new Guid("6f8799a7-097b-45f4-a8f0-805784ee24a6"), "images/tshirtimage/T-ShirtBlack9gradient.png", "133293148993253118", "T-ShirtBlack 9 Gradient" },
                    { 10, 138, new Guid("49674251-6b45-437f-8c7a-34a0b1a34c01"), "images/tshirtimage/T-ShirtBlue9gradient.png", "133293148993253167", "T-ShirtBlue 9 Gradient" },
                    { 11, 745, new Guid("7b5ae8b9-12a0-4a19-9c37-943bc4cc5a3e"), "images/tshirtimage/T-ShirtBrown9gradient.png", "133293148993253213", "T-ShirtBrown 9 Gradient" },
                    { 12, 5, new Guid("097dbb41-2aa0-4fbf-8986-bdf8e4e0d4b9"), "images/tshirtimage/T-ShirtGreen9gradient.png", "133293148993253258", "T-ShirtGreen 9 Gradient" },
                    { 13, 57, new Guid("5f8d15b2-4c4d-4e7a-8902-d78d3b4cb10b"), "images/tshirtimage/T-ShirtPurple9gradient.png", "133293148993253303", "T-ShirtPurple 9 Gradient" },
                    { 14, 224, new Guid("4d50363a-2dc4-4ef0-8bde-682758f0f801"), "images/tshirtimage/T-ShirtRed9gradient.png", "133293148993253350", "T-ShirtRed 9 Gradient" },
                    { 15, 150, new Guid("1835ee0e-3275-4ff9-9d79-1a2e3b5e15e3"), "images/tshirtimage/T-ShirtRose9gradient.png", "133293148993253394", "T-ShirtRose 9 Gradient" },
                    { 16, 186, new Guid("e5e4a551-2b64-43fc-86e1-4c1c0f6880fe"), "images/tshirtimage/T-ShirtYellow9gradient.png", "133293148993253435", "T-ShirtYellow 9 Gradient" }
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
                    { 1, 1, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993253817", 9750.25f },
                    { 2, 1, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993253907", 82.88f },
                    { 3, 1, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993253955", 70.2f },
                    { 4, 1, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993253999", 94.58f },
                    { 5, 1, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254045", 607.44f },
                    { 6, 1, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254096", 6922.68f },
                    { 7, 1, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254141", 10237.76f },
                    { 1, 2, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254189", 4121.5f },
                    { 2, 2, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254231", 35.03f },
                    { 3, 2, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254276", 29.67f },
                    { 4, 2, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254321", 39.98f },
                    { 5, 2, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254367", 256.77f },
                    { 6, 2, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254411", 2926.26f },
                    { 7, 2, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254452", 4327.57f },
                    { 1, 3, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254494", 6383.75f },
                    { 2, 3, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254537", 54.26f },
                    { 3, 3, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254581", 45.96f },
                    { 4, 3, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254718", 61.92f },
                    { 5, 3, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254765", 397.71f },
                    { 6, 3, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254807", 4532.46f },
                    { 7, 3, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993254851", 6702.94f },
                    { 1, 4, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255007", 8925f },
                    { 2, 4, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255054", 75.86f },
                    { 3, 4, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255098", 64.26f },
                    { 4, 4, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255139", 86.57f },
                    { 5, 4, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255182", 556.03f },
                    { 6, 4, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255224", 6336.75f },
                    { 7, 4, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255268", 9371.25f },
                    { 1, 5, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255313", 5241f },
                    { 2, 5, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255358", 44.55f },
                    { 3, 5, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255399", 37.74f },
                    { 4, 5, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255440", 50.84f },
                    { 5, 5, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255481", 326.51f },
                    { 6, 5, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255528", 3721.11f },
                    { 7, 5, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255568", 5503.05f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 6, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255609", 7568.5f },
                    { 2, 6, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255651", 64.33f },
                    { 3, 6, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255694", 54.49f },
                    { 4, 6, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255739", 73.41f },
                    { 5, 6, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255780", 471.52f },
                    { 6, 6, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255820", 5373.63f },
                    { 7, 6, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993255866", 7946.92f },
                    { 1, 7, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256015", 1953.75f },
                    { 2, 7, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256060", 16.61f },
                    { 3, 7, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256103", 14.07f },
                    { 4, 7, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256146", 18.95f },
                    { 5, 7, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256188", 121.72f },
                    { 6, 7, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256229", 1387.16f },
                    { 7, 7, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256272", 2051.44f },
                    { 1, 8, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256312", 6546.5f },
                    { 2, 8, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256351", 55.65f },
                    { 3, 8, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256390", 47.13f },
                    { 4, 8, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256429", 63.5f },
                    { 5, 8, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256469", 407.85f },
                    { 6, 8, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256506", 4648.01f },
                    { 7, 8, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256545", 6873.82f },
                    { 1, 9, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256584", 4037f },
                    { 2, 9, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256623", 34.31f },
                    { 3, 9, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256661", 29.07f },
                    { 4, 9, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256699", 39.16f },
                    { 5, 9, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256737", 251.51f },
                    { 6, 9, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256777", 2866.27f },
                    { 7, 9, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256818", 4238.85f },
                    { 1, 10, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256859", 8614.5f },
                    { 2, 10, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993256996", 73.22f },
                    { 3, 10, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257046", 62.02f },
                    { 4, 10, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257087", 83.56f },
                    { 5, 10, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257128", 536.68f },
                    { 6, 10, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257168", 6116.29f },
                    { 7, 10, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257207", 9045.22f },
                    { 1, 11, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257247", 1386.5f },
                    { 2, 11, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257287", 11.79f },
                    { 3, 11, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257330", 9.98f },
                    { 4, 11, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257373", 13.45f },
                    { 5, 11, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257413", 86.38f },
                    { 6, 11, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257457", 984.41f },
                    { 7, 11, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257500", 1455.82f }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 12, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257542", 5970.5f },
                    { 2, 12, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257583", 50.75f },
                    { 3, 12, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257623", 42.99f },
                    { 4, 12, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257664", 57.91f },
                    { 5, 12, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257704", 371.96f },
                    { 6, 12, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257746", 4239.05f },
                    { 7, 12, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257883", 6269.02f },
                    { 1, 13, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257937", 3177.25f },
                    { 2, 13, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993257978", 27.01f },
                    { 3, 13, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258021", 22.88f },
                    { 4, 13, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258062", 30.82f },
                    { 5, 13, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258103", 197.94f },
                    { 6, 13, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258143", 2255.85f },
                    { 7, 13, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258183", 3336.11f },
                    { 1, 14, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258225", 9264.75f },
                    { 2, 14, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258265", 78.75f },
                    { 3, 14, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258305", 66.71f },
                    { 4, 14, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258345", 89.87f },
                    { 5, 14, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258387", 577.19f },
                    { 6, 14, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258427", 6577.97f },
                    { 7, 14, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258468", 9727.99f },
                    { 1, 15, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258508", 7812f },
                    { 2, 15, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258547", 66.4f },
                    { 3, 15, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258587", 56.25f },
                    { 4, 15, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258627", 75.78f },
                    { 5, 15, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258668", 486.69f },
                    { 6, 15, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258711", 5546.52f },
                    { 7, 15, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258753", 8202.6f },
                    { 1, 16, "RSD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258895", 2985.25f },
                    { 2, 16, "EUR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258948", 25.37f },
                    { 3, 16, "GBP", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993258990", 21.49f },
                    { 4, 16, "USD", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993260078", 28.96f },
                    { 5, 16, "RMB", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993260295", 185.98f },
                    { 6, 16, "INR", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993260347", 2119.53f },
                    { 7, 16, "JPY", new Guid("00000000-0000-0000-0000-000000000000"), "133293148993260392", 3134.51f }
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
