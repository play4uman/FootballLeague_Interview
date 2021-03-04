using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeague_Interview.Migrations
{
    public partial class AddResultStandingsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Results_SeasonId",
                table: "Results");

            migrationBuilder.AddColumn<string>(
                name: "LeagueId",
                table: "Results",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Results_LeagueId",
                table: "Results",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_SeasonId_LeagueId",
                table: "Results",
                columns: new[] { "SeasonId", "LeagueId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Leagues_LeagueId",
                table: "Results",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Standings_SeasonId_LeagueId",
                table: "Results",
                columns: new[] { "SeasonId", "LeagueId" },
                principalTable: "Standings",
                principalColumns: new[] { "SeasonId", "LeagueId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Leagues_LeagueId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Standings_SeasonId_LeagueId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_LeagueId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_SeasonId_LeagueId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Results");

            migrationBuilder.CreateIndex(
                name: "IX_Results_SeasonId",
                table: "Results",
                column: "SeasonId");
        }
    }
}
