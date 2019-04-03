using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentx.Web.Migrations
{
    public partial class RemoveColumnFromOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaveInformation",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SaveInformation",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }
    }
}
