using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaStore.Migrations
{
    public partial class EditNamesCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeightI",
                table: "Ingredients",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "PriceI",
                table: "Ingredients",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PhotoI",
                table: "Ingredients",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "NameI",
                table: "Ingredients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameC",
                table: "Categories",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Ingredients",
                newName: "WeightI");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Ingredients",
                newName: "PriceI");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Ingredients",
                newName: "PhotoI");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ingredients",
                newName: "NameI");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "NameC");
        }
    }
}
