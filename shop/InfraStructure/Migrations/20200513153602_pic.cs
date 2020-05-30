using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class pic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "picUrl",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "picUrl",
                table: "users");
        }
    }
}
