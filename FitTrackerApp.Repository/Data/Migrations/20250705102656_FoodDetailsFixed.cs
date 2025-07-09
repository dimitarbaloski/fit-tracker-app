using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrackerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FoodDetailsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Query",
                table: "MealItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Query",
                table: "MealItems");
        }
    }
}
