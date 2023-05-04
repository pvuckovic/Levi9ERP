using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levi9.ERP.Data.Migrations.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_PriceLists_PriceListId1",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Products_ProductId1",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocuments_Documents_DocumentId1",
                table: "ProductDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocuments_Products_ProductId1",
                table: "ProductDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ProductDocuments_DocumentId1",
                table: "ProductDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ProductDocuments_ProductId1",
                table: "ProductDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Prices_PriceListId1",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_ProductId1",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "DocumentId1",
                table: "ProductDocuments");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductDocuments");

            migrationBuilder.DropColumn(
                name: "PriceListId1",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Prices");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "ProductDocuments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductDocuments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "ProductDocuments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductDocuments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentId1",
                table: "ProductDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceListId1",
                table: "Prices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Prices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocuments_DocumentId1",
                table: "ProductDocuments",
                column: "DocumentId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocuments_ProductId1",
                table: "ProductDocuments",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_PriceListId1",
                table: "Prices",
                column: "PriceListId1");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductId1",
                table: "Prices",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_PriceLists_PriceListId1",
                table: "Prices",
                column: "PriceListId1",
                principalTable: "PriceLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Products_ProductId1",
                table: "Prices",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocuments_Documents_DocumentId1",
                table: "ProductDocuments",
                column: "DocumentId1",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocuments_Products_ProductId1",
                table: "ProductDocuments",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
