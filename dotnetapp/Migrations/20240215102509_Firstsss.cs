using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class Firstsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EventId",
                table: "Schedules",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RefereeId",
                table: "Schedules",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team1Id",
                table: "Schedules",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team2Id",
                table: "Schedules",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_VenueId",
                table: "Schedules",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Events_EventId",
                table: "Schedules",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Referees_RefereeId",
                table: "Schedules",
                column: "RefereeId",
                principalTable: "Referees",
                principalColumn: "RefereeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Teams_Team1Id",
                table: "Schedules",
                column: "Team1Id",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Teams_Team2Id",
                table: "Schedules",
                column: "Team2Id",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Venues_VenueId",
                table: "Schedules",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Events_EventId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Referees_RefereeId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teams_Team1Id",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teams_Team2Id",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Venues_VenueId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_EventId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_RefereeId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Team1Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Team2Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_VenueId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamId",
                table: "Players");
        }
    }
}
