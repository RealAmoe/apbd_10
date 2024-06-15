using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task10.Migrations
{
    /// <inheritdoc />
    public partial class appuseraddedNewData1111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccesstToken",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccesstToken",
                table: "User",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
