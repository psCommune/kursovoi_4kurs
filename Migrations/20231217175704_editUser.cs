using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eLibrary.Migrations
{
    /// <inheritdoc />
    public partial class editUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "UserAge");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserAge",
                table: "Users",
                newName: "Age");
        }
    }
}
