using Microsoft.EntityFrameworkCore.Migrations;

namespace PrsCapBackendProject.Migrations
{
    public partial class DecimalTotalChangedTo12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Requests",
                type: "decimal(12, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Requests",
                type: "decimal(11, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 2)");
        }
    }
}
