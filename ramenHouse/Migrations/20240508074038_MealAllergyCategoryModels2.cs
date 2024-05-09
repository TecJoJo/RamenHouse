using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ramenHouse.Migrations
{
    /// <inheritdoc />
    public partial class MealAllergyCategoryModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Meals_MealId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MealId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MealId",
                table: "Categories",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Meals_MealId",
                table: "Categories",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
