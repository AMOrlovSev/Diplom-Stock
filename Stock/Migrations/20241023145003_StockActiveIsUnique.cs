using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class StockActiveIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockActives_PortfolioID",
                table: "StockActives");

            migrationBuilder.CreateIndex(
                name: "IX_StockActives_PortfolioID_SecID",
                table: "StockActives",
                columns: new[] { "PortfolioID", "SecID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockActives_PortfolioID_SecID",
                table: "StockActives");

            migrationBuilder.CreateIndex(
                name: "IX_StockActives_PortfolioID",
                table: "StockActives",
                column: "PortfolioID");
        }
    }
}
