using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyCalculatorSystem.Infrastructure.Data.Migrations.Bussiness
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyIsoCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    CurrencyIsoNumber = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CurrencyStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteCurrencies",
                columns: table => new
                {
                    FavouriteCurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavouriteCurrency = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteCurrencies", x => x.FavouriteCurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProviderCountryCode = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    ProviderRateType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProviderPivotCurrency = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyDate = table.Column<DateTime>(type: "date", nullable: false),
                    RateValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CurrencyBaseId = table.Column<int>(type: "int", nullable: false),
                    QuoteCurrencyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.RateId);
                    table.ForeignKey(
                        name: "FK_Rates_Currencies_CurrencyBaseId",
                        column: x => x.CurrencyBaseId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Currencies_QuoteCurrencyId",
                        column: x => x.QuoteCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UX_Currencies_IsoCode",
                table: "Currencies",
                column: "CurrencyIsoCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Providers_ProviderKey",
                table: "Providers",
                column: "ProviderKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CurrencyBaseId",
                table: "Rates",
                column: "CurrencyBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ProviderId",
                table: "Rates",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_QuoteCurrencyId",
                table: "Rates",
                column: "QuoteCurrencyId");

            migrationBuilder.CreateIndex(
                name: "UX_Rates_Date_Base_Quote_Provider",
                table: "Rates",
                columns: new[] { "CurrencyDate", "CurrencyBaseId", "QuoteCurrencyId", "ProviderId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteCurrencies");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
