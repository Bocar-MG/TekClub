using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekClub.Migrations
{
    /// <inheritdoc />
    public partial class ProfilPicIForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profileImageUrl",
                table: "AspNetUsers",
                newName: "ProfileImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                newName: "profileImageUrl");
        }
    }
}
