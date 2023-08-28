using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekClub.Migrations
{
    /// <inheritdoc />
    public partial class ActivitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenement_Clubs_ClubId",
                table: "Evenement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evenement",
                table: "Evenement");

            migrationBuilder.RenameTable(
                name: "Evenement",
                newName: "Evenements");

            migrationBuilder.RenameIndex(
                name: "IX_Evenement_ClubId",
                table: "Evenements",
                newName: "IX_Evenements_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evenements",
                table: "Evenements",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Activités",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeActivité = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activités", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activités_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activités_ClubId",
                table: "Activités",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Clubs_ClubId",
                table: "Evenements",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Clubs_ClubId",
                table: "Evenements");

            migrationBuilder.DropTable(
                name: "Activités");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evenements",
                table: "Evenements");

            migrationBuilder.RenameTable(
                name: "Evenements",
                newName: "Evenement");

            migrationBuilder.RenameIndex(
                name: "IX_Evenements_ClubId",
                table: "Evenement",
                newName: "IX_Evenement_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evenement",
                table: "Evenement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenement_Clubs_ClubId",
                table: "Evenement",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }
    }
}
