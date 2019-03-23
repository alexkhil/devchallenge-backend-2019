using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SC.DevChallenge.DataAccess.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Instruments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerInstruments",
                schema: "dbo",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerInstruments", x => new { x.OwnerId, x.InstrumentId });
                    table.ForeignKey(
                        name: "FK_OwnerInstruments_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalSchema: "dbo",
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerInstruments_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "dbo",
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerPortfolios",
                schema: "dbo",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false),
                    PortfolioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerPortfolios", x => new { x.OwnerId, x.PortfolioId });
                    table.ForeignKey(
                        name: "FK_OwnerPortfolios_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "dbo",
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerPortfolios_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "dbo",
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                schema: "dbo",
                columns: table => new
                {
                    PortfolioId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Timeslot = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => new { x.PortfolioId, x.OwnerId, x.InstrumentId, x.Date });
                    table.ForeignKey(
                        name: "FK_Prices_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalSchema: "dbo",
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "dbo",
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "dbo",
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_Name",
                schema: "dbo",
                table: "Instruments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnerInstruments_InstrumentId",
                schema: "dbo",
                table: "OwnerInstruments",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerPortfolios_PortfolioId",
                schema: "dbo",
                table: "OwnerPortfolios",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Name",
                schema: "dbo",
                table: "Owners",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_Name",
                schema: "dbo",
                table: "Portfolios",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prices_InstrumentId",
                schema: "dbo",
                table: "Prices",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_OwnerId",
                schema: "dbo",
                table: "Prices",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerInstruments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OwnerPortfolios",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Prices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Instruments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Owners",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Portfolios",
                schema: "dbo");
        }
    }
}
