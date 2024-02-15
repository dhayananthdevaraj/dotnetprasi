using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teams_Team1Id",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teams_Team2Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Team1Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Team2Id",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team1Id",
                table: "Schedules",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team2Id",
                table: "Schedules",
                column: "Team2Id");

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
        }
    }
}
