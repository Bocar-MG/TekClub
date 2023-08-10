using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekClub.Migrations
{
    /// <inheritdoc />
    public partial class ClubUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_ResponsableId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_ResponsableId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ResponsableId",
                table: "Clubs");

            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClubId",
                table: "AspNetUsers",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clubs_ClubId",
                table: "AspNetUsers",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Clubs_ClubId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClubId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ResponsableId",
                table: "Clubs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ResponsableId",
                table: "Clubs",
                column: "ResponsableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_ResponsableId",
                table: "Clubs",
                column: "ResponsableId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
