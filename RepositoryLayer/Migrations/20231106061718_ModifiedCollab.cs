using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class ModifiedCollab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollabName",
                table: "Collabs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Collabs");

            migrationBuilder.AddColumn<string>(
                name: "CollabEmail",
                table: "Collabs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollabEmail",
                table: "Collabs");

            migrationBuilder.AddColumn<string>(
                name: "CollabName",
                table: "Collabs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Collabs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
