using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPILevinci.Migrations
{
    /// <inheritdoc />
    public partial class deletepasshash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "User",
                type: "varbinary",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "User",
                type: "varbinary",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);
        }
    }
}
