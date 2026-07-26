using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_ProductionName",
                table: "OrderItem",
                newName: "Product_ProductName");

            migrationBuilder.RenameColumn(
                name: "Product_ProductionId",
                table: "OrderItem",
                newName: "Product_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "DeliveryMethods",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimai(8,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_ProductName",
                table: "OrderItem",
                newName: "Product_ProductionName");

            migrationBuilder.RenameColumn(
                name: "Product_ProductId",
                table: "OrderItem",
                newName: "Product_ProductionId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "DeliveryMethods",
                type: "decimai(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");
        }
    }
}
