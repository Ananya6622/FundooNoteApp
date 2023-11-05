using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class UppdatedLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "NoteId",
                table: "Labels",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_NoteId",
                table: "Labels",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_UserId",
                table: "Labels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Notes_NoteId",
                table: "Labels",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Notes_NoteId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_NoteId",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_UserId",
                table: "Labels");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Labels",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
