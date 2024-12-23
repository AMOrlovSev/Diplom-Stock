using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrokerageAccounts",
                columns: table => new
                {
                    BrokerageAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrokerageAccountType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerageAccounts", x => x.BrokerageAccountID);
                });

            migrationBuilder.CreateTable(
                name: "Brokers",
                columns: table => new
                {
                    BrokerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrokerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.BrokerID);
                });

            migrationBuilder.CreateTable(
                name: "StockInformations",
                columns: table => new
                {
                    SecID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ListLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInformations", x => x.SecID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BrokerID = table.Column<int>(type: "int", nullable: false),
                    BrokerageAccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_Portfolios_BrokerageAccounts_BrokerageAccountID",
                        column: x => x.BrokerageAccountID,
                        principalTable: "BrokerageAccounts",
                        principalColumn: "BrokerageAccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portfolios_Brokers_BrokerID",
                        column: x => x.BrokerID,
                        principalTable: "Brokers",
                        principalColumn: "BrokerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portfolios_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockActives",
                columns: table => new
                {
                    StockActiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    SecID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockActives", x => x.StockActiveID);
                    table.ForeignKey(
                        name: "FK_StockActives_Portfolios_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockActives_StockInformations_SecID",
                        column: x => x.SecID,
                        principalTable: "StockInformations",
                        principalColumn: "SecID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_BrokerageAccountID",
                table: "Portfolios",
                column: "BrokerageAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_BrokerID",
                table: "Portfolios",
                column: "BrokerID");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_UserID",
                table: "Portfolios",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_StockActives_PortfolioID",
                table: "StockActives",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_StockActives_SecID",
                table: "StockActives",
                column: "SecID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockActives");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "StockInformations");

            migrationBuilder.DropTable(
                name: "BrokerageAccounts");

            migrationBuilder.DropTable(
                name: "Brokers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
