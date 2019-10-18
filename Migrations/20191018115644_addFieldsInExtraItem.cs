using Microsoft.EntityFrameworkCore.Migrations;

namespace eatklik.Migrations
{
    public partial class addFieldsInExtraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "RestaurantExtraItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImagePath",
                table: "Cities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "RestaurantExtraItems");

            migrationBuilder.DropColumn(
                name: "CoverImagePath",
                table: "Cities");
        }
    }
}
