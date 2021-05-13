using Microsoft.EntityFrameworkCore.Migrations;

namespace ADMPublishers.DataAccess.Migrations
{
    public partial class AuthorPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthoId",
                table: "Authors",
                newName: "AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Authors",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Authors",
                newName: "AuthoId");
        }
    }
}
