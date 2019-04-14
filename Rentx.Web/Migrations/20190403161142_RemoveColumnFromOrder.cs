using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentx.Web.Migrations
{
    public partial class RemoveColumnFromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddressIsTheSameAsBillingAddress",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShippingAddressIsTheSameAsBillingAddress",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }
    }
}
