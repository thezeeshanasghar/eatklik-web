using Microsoft.EntityFrameworkCore.Migrations;

namespace eatklik.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DelRadius",
                table: "RestaurantLocations",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelRadius",
                table: "RestaurantLocations");
        }
    }
}
