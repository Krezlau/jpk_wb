using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jpk_wb.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBankStatement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InformacjePodmiotuId",
                table: "BankStatements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BankStatements_InformacjePodmiotuId",
                table: "BankStatements",
                column: "InformacjePodmiotuId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankStatements_CompanyInfos_InformacjePodmiotuId",
                table: "BankStatements",
                column: "InformacjePodmiotuId",
                principalTable: "CompanyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankStatements_CompanyInfos_InformacjePodmiotuId",
                table: "BankStatements");

            migrationBuilder.DropIndex(
                name: "IX_BankStatements_InformacjePodmiotuId",
                table: "BankStatements");

            migrationBuilder.DropColumn(
                name: "InformacjePodmiotuId",
                table: "BankStatements");
        }
    }
}
