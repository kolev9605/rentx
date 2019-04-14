using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentx.Web.Migrations
{
    public partial class RentingConceptAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "RentPrice",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentPrice",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
