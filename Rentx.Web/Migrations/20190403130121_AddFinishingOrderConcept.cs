using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentx.Web.Migrations
{
    public partial class AddFinishingOrderConcept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Orders");
        }
    }
}
