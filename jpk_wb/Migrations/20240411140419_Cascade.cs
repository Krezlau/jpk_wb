using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jpk_wb.Migrations
{
    /// <inheritdoc />
    public partial class Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankStatements_BankStatementId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankStatements_BankStatementId",
                table: "Transactions",
                column: "BankStatementId",
                principalTable: "BankStatements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankStatements_BankStatementId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankStatements_BankStatementId",
                table: "Transactions",
                column: "BankStatementId",
                principalTable: "BankStatements",
                principalColumn: "Id");
        }
    }
}
