using Microsoft.EntityFrameworkCore.Migrations;

namespace eatklik.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantLocations_Cities_CityId",
                table: "RestaurantLocations");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantLocations_CityId",
                table: "RestaurantLocations");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "RestaurantLocations");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Riders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "Riders",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeliveryCharges",
                table: "Orders",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CityId",
                table: "Restaurants",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Cities_CityId",
                table: "Restaurants",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Cities_CityId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_CityId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Riders");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "Riders");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "DeliveryCharges",
                table: "Orders");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "RestaurantLocations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantLocations_CityId",
                table: "RestaurantLocations",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantLocations_Cities_CityId",
                table: "RestaurantLocations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
