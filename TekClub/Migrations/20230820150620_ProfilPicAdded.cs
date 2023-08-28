using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekClub.Migrations
{
    /// <inheritdoc />
    public partial class ProfilPicAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profileImageUrl",
                table: "AspNetUsers");
        }
    }
}
