using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountGuid",
                table: "tb_m_educations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_educations_AccountGuid",
                table: "tb_m_educations",
                column: "AccountGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_accounts_AccountGuid",
                table: "tb_m_educations",
                column: "AccountGuid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_accounts_AccountGuid",
                table: "tb_m_educations");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_educations_AccountGuid",
                table: "tb_m_educations");

            migrationBuilder.DropColumn(
                name: "AccountGuid",
                table: "tb_m_educations");
        }
    }
}
