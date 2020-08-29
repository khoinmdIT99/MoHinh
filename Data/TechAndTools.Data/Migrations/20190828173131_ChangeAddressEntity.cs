using Microsoft.EntityFrameworkCore.Migrations;

namespace TechAndTools.Data.Migrations
{
    public partial class ChangeAddressEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Quarter",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Addresses",
                newName: "CityAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityAddress",
                table: "Addresses",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quarter",
                table: "Addresses",
                nullable: true);
        }
    }
}
