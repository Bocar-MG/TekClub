using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekClub.Migrations
{
    /// <inheritdoc />
    public partial class AddedClubTempProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "clubTemp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clubTemp",
                table: "AspNetUsers");
        }
    }
}
