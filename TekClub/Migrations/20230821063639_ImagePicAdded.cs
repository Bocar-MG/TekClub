using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekClub.Migrations
{
    /// <inheritdoc />
    public partial class ImagePicAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImagePic",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePic",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
