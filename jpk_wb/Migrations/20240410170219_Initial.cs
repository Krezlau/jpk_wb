using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jpk_wb.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankStatements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumerRachunku = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankStatements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NIP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PelnaNazwa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    REGON = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KodKraju = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wojewodztwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Powiat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gmina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrDomu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrLokalu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miejscowosc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poczta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataOperacji = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NazwaPodmiotu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpisOperacji = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KwotaOperacji = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoOperacji = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SymbolWaluty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankStatementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_BankStatements_BankStatementId",
                        column: x => x.BankStatementId,
                        principalTable: "BankStatements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfos_NIP",
                table: "CompanyInfos",
                column: "NIP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfos_PelnaNazwa",
                table: "CompanyInfos",
                column: "PelnaNazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfos_REGON",
                table: "CompanyInfos",
                column: "REGON",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankStatementId",
                table: "Transactions",
                column: "BankStatementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInfos");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BankStatements");
        }
    }
}
