using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupHub.Infra.Migrations
{
    public partial class deleteColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedPassword",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "HashedPassword");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "Users",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedPassword",
                table: "Users",
                nullable: true);
        }
    }
}
