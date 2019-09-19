using Microsoft.EntityFrameworkCore.Migrations;

namespace eatklik.Migrations
{
    public partial class AddedMenuExtraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuExtraItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<long>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    MenuItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuExtraItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuExtraItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuExtraItems_MenuItemId",
                table: "MenuExtraItems",
                column: "MenuItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuExtraItems");
        }
    }
}
