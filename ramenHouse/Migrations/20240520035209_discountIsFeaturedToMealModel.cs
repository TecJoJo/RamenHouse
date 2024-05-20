using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ramenHouse.Migrations
{
    /// <inheritdoc />
    public partial class discountIsFeaturedToMealModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Meals",
                newName: "Discount");

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Meals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Meals",
                newName: "Price");
        }
    }
}
