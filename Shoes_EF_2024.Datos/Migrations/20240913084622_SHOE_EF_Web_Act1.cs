using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoes_EF_2024.Datos.Migrations
{
    public partial class SHOE_EF_Web_Act1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_BrandId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Colors_ColorID",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_GenreId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_SportId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Sport_Name",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Size",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Genre_Name",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Color_Name",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Brands_Name",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "sizeNumber",
                table: "Sizes",
                newName: "SizeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Sizes_sizeNumber",
                table: "Sizes",
                newName: "IX_Sizes_SizeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_QuantityInStock",
                table: "ShoeSizes",
                newName: "IX_ShoeSizes_QuantityInStock");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Shoes",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Shoes",
                newName: "Suspended");

            migrationBuilder.RenameIndex(
                name: "FK_Shoes_SportId",
                table: "Shoes",
                newName: "IX_Shoes_SportId");

            migrationBuilder.RenameIndex(
                name: "FK_Shoes_GenreId",
                table: "Shoes",
                newName: "IX_Shoes_GenreId");

            migrationBuilder.RenameIndex(
                name: "FK_Shoes_BrandId",
                table: "Shoes",
                newName: "IX_Shoes_BrandId");

            migrationBuilder.AlterColumn<string>(
                name: "SportName",
                table: "Sports",
                type: "nvarchar(20)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Shoes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Stock",
                table: "Shoes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Sports_SportName",
                table: "Sports",
                column: "SportName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Brands_BrandId",
                table: "Shoes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Colors_ColorID",
                table: "Shoes",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Genre_GenreId",
                table: "Shoes",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Sports_SportId",
                table: "Shoes",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Brands_BrandId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Colors_ColorID",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Genre_GenreId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Sports_SportId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Sports_SportName",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Shoes");

            migrationBuilder.RenameColumn(
                name: "SizeNumber",
                table: "Sizes",
                newName: "sizeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Sizes_SizeNumber",
                table: "Sizes",
                newName: "IX_Sizes_sizeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ShoeSizes_QuantityInStock",
                table: "ShoeSizes",
                newName: "IX_QuantityInStock");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Shoes",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Suspended",
                table: "Shoes",
                newName: "Active");

            migrationBuilder.RenameIndex(
                name: "IX_Shoes_SportId",
                table: "Shoes",
                newName: "FK_Shoes_SportId");

            migrationBuilder.RenameIndex(
                name: "IX_Shoes_GenreId",
                table: "Shoes",
                newName: "FK_Shoes_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes",
                newName: "FK_Shoes_BrandId");

            migrationBuilder.AlterColumn<string>(
                name: "SportName",
                table: "Sports",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 100);
        }
    }
}

