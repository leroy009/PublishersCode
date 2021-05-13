using Microsoft.EntityFrameworkCore.Migrations;

namespace ADMPublishers.DataAccess.Migrations
{
    public partial class AuthorAddressSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adddress",
                table: "Authors",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Authors",
                newName: "Adddress");
        }
    }
}
