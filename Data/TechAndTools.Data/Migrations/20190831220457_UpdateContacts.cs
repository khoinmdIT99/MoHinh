using Microsoft.EntityFrameworkCore.Migrations;

namespace TechAndTools.Data.Migrations
{
    public partial class UpdateContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MarkAsRead",
                table: "Contacts",
                newName: "IsArchived");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "Contacts",
                newName: "MarkAsRead");
        }
    }
}
