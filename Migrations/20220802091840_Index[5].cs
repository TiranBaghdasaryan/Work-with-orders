using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Work_with_orders.Migrations
{
    /// <inheritdoc />
    public partial class Index5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateCreated", "Email", "FirstName", "IsVerified", "LastName", "Password", "PhoneNumber", "Role" },
                values: new object[] { 1L, "Erevean", new DateTime(2022, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), "tirbaghdasaryan0808@gmail.com", "Tiran", true, "Baghdasaryan", "00_root_00", "+374-99-63-10-71", 2 });
        }
    }
}
