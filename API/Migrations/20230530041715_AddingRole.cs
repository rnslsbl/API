using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddingRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[,]
                {
                    //{ new Guid("42e77495-645e-4095-586f-08db60bf14ae"), new DateTime(2023, 5, 30, 11, 17, 14, 252, DateTimeKind.Local).AddTicks(8955), new DateTime(2023, 5, 30, 11, 17, 14, 252, DateTimeKind.Local).AddTicks(8967), "User" },
                    { new Guid("9ac396d5-82ca-48ec-5870-08db60bf14ae"), new DateTime(2023, 5, 30, 11, 17, 14, 252, DateTimeKind.Local).AddTicks(8973), new DateTime(2023, 5, 30, 11, 17, 14, 252, DateTimeKind.Local).AddTicks(8974), "Manager" },
                    { new Guid("b3ebe10c-41dc-4f2e-5871-08db60bf14ae"), new DateTime(2023, 5, 30, 11, 17, 14, 252, DateTimeKind.Local).AddTicks(8978), new DateTime(2023, 5, 30, 11, 17, 14, 252, DateTimeKind.Local).AddTicks(8979), "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("42e77495-645e-4095-586f-08db60bf14ae"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("9ac396d5-82ca-48ec-5870-08db60bf14ae"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("b3ebe10c-41dc-4f2e-5871-08db60bf14ae"));
        }
    }
}
