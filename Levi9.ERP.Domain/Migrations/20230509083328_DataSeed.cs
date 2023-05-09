using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levi9.ERP.Domain.Migrations
{
    public partial class DataSeed : Migration
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
                    { 1, new Guid("8664a890-ae23-478b-a0d6-062cb605faa5"), "634792557112051692", "USD Price List" },
                    { 2, new Guid("a30d7989-498f-42b5-b269-8f1174a003d9"), "634792557112051693", "EUR Price List" },
                    { 3, new Guid("dccff2b3-ff87-41b9-aae8-270c2f255c70"), "634792557112051694", "RSD Price List" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "GlobalId", "ImageUrl", "LastUpdate", "Name" },
                values: new object[,]
                {
                    { 1, 70, new Guid("43d019b8-70d6-426c-8acf-590d82067dcc"), "someurl123344444", "634792557112051692", "Shirt" },
                    { 2, 140, new Guid("3ab7697b-13c4-47f8-a4be-7d13150129ea"), "slika.png", "634792557112051693", "A T-Shirt" },
                    { 3, 150, new Guid("d43436b9-dade-4469-950e-9b577cd691e2"), "slika2.png", "634792557112051694", "Z T-Shirt" },
                    { 4, 550, new Guid("734ca172-b580-441d-9113-20cb79850424"), "slika3.png", "634792557112051695", "B T-Shirt" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Email", "GlobalId", "LastUpdate", "Name", "Phone", "PriceListId" },
                values: new object[] { 1, "Njegoseva 2", "zlatko123@gmail.com", new Guid("cec1a21c-e8c5-4579-80d0-c6aaa3d8ef8b"), "634792557112051692", "Zlatko", "064322222", 1 });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceListId", "ProductId", "Currency", "GlobalId", "LastUpdate", "PriceValue" },
                values: new object[,]
                {
                    { 1, 1, "USD", new Guid("5b148410-72b2-40fb-bd0e-e9bbb843c0e6"), "634792557112051692", 12f },
                    { 2, 2, "EUR", new Guid("f50c5412-49e8-461c-8322-a5734d538f9f"), "634792557112051693", 30f },
                    { 3, 3, "RSD", new Guid("a1e47326-9d3a-454e-9c3e-b3b8b4c648db"), "634792557112051694", 1500f },
                    { 3, 4, "RSD", new Guid("1794f9cc-5790-427c-b00c-c6d660eada51"), "634792557112051695", 1700f }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "ClientId", "DocumentType", "GlobalId", "LastUpdate" },
                values: new object[] { 1, 1, "INVOICE", new Guid("ea6dca9e-b279-4a37-a8f7-30bbeb0c7529"), "634792557112051692" });

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
