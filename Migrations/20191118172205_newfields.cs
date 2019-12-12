using Microsoft.EntityFrameworkCore.Migrations;

namespace eatklik.Migrations
{
    public partial class newfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CouponCodes");

            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Riders",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "RiderRatings",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Riders",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<long>(
                name: "Value",
                table: "RiderRatings",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "Discount",
                table: "CouponCodes",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
