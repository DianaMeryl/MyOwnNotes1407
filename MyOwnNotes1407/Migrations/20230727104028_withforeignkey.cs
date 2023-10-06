using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOwnNotes1407.Data.Migrations
{
    /// <inheritdoc />
    public partial class withforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");
        }
    }
}
