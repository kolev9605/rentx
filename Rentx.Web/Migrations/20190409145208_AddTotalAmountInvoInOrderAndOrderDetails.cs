using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentx.Web.Migrations
{
    public partial class AddTotalAmountInvoInOrderAndOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRentAmount",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductDetailPrice",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductDetailRentPrice",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalRentAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductDetailPrice",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductDetailRentPrice",
                table: "OrderDetails");
        }
    }
}
