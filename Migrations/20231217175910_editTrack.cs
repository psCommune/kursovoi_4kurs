using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eLibrary.Migrations
{
    /// <inheritdoc />
    public partial class editTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Authors_AuthorId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Authors");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Tracks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Authors_AuthorId",
                table: "Tracks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Authors_AuthorId",
                table: "Tracks");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Tracks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Authors_AuthorId",
                table: "Tracks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
