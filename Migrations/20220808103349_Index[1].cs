using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Work_with_orders.Migrations
{
    /// <inheritdoc />
    public partial class Index1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasketProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "BasketProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
