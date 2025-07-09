using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrackerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserToMealDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MealItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_UserId",
                table: "MealItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_AspNetUsers_UserId",
                table: "MealItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_AspNetUsers_UserId",
                table: "MealItems");

            migrationBuilder.DropIndex(
                name: "IX_MealItems_UserId",
                table: "MealItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MealItems");
        }
    }
}
