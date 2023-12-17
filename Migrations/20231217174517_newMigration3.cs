using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eLibrary.Migrations
{
    /// <inheritdoc />
    public partial class newMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Author_AuthorId",
                table: "Tracks");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_AuthorId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tracks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Tracks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Pseudonym = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AuthorId",
                table: "Tracks",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Author_AuthorId",
                table: "Tracks",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
