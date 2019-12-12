using Microsoft.EntityFrameworkCore.Migrations;

namespace eatklik.Migrations
{
    public partial class AddRiderRatingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiderRatings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Value = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    RiderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiderRatings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiderRatings_Riders_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Riders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiderRatings_CustomerId",
                table: "RiderRatings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RiderRatings_RiderId",
                table: "RiderRatings",
                column: "RiderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiderRatings");
        }
    }
}
