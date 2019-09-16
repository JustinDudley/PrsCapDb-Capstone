using Microsoft.EntityFrameworkCore.Migrations;

namespace PrsCapBackendProject.Migrations
{
    public partial class UniqueColumnsAddedInThreeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IDX_Code",
                table: "Vendors",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_PartNbr",
                table: "Products",
                column: "PartNbr",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_Code",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IDX_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IDX_PartNbr",
                table: "Products");
        }
    }
}
