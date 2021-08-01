using Microsoft.EntityFrameworkCore.Migrations;

namespace ATMModel.Migrations
{
    public partial class AddedBalanceChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BalanceChange",
                table: "Operation",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceChange",
                table: "Operation");
        }
    }
}
