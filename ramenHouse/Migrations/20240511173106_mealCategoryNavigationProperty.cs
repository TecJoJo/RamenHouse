using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ramenHouse.Migrations
{
    /// <inheritdoc />
    public partial class mealCategoryNavigationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Meals",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_CategoryId",
                table: "Meals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_CategoryId",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Meals",
                newName: "Category");
        }
    }
}
