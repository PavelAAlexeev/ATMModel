using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATMModel.Migrations
{
    public partial class AddedOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Card_CardNumber",
                table: "Card");

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardId = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationType = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Operation_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardNumber",
                table: "Card",
                column: "CardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_CardId",
                table: "Operation",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Card_CardNumber",
                table: "Card");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardNumber",
                table: "Card",
                column: "CardNumber");
        }
    }
}
