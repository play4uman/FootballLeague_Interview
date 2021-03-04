using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeague_Interview.Migrations
{
    public partial class ChangeStandingsKeyToCompositeKeyOfSeasonAndLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StandingRow_Standings_StandingsSeasonId",
                table: "StandingRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Standings",
                table: "Standings");

            migrationBuilder.DropIndex(
                name: "IX_StandingRow_StandingsSeasonId",
                table: "StandingRow");

            migrationBuilder.AddColumn<string>(
                name: "LeagueId",
                table: "Standings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StandingsLeagueId",
                table: "StandingRow",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Standings",
                table: "Standings",
                columns: new[] { "SeasonId", "LeagueId" });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_LeagueId",
                table: "Standings",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingRow_StandingsSeasonId_StandingsLeagueId",
                table: "StandingRow",
                columns: new[] { "StandingsSeasonId", "StandingsLeagueId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StandingRow_Standings_StandingsSeasonId_StandingsLeagueId",
                table: "StandingRow",
                columns: new[] { "StandingsSeasonId", "StandingsLeagueId" },
                principalTable: "Standings",
                principalColumns: new[] { "SeasonId", "LeagueId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Standings_Leagues_LeagueId",
                table: "Standings",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StandingRow_Standings_StandingsSeasonId_StandingsLeagueId",
                table: "StandingRow");

            migrationBuilder.DropForeignKey(
                name: "FK_Standings_Leagues_LeagueId",
                table: "Standings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Standings",
                table: "Standings");

            migrationBuilder.DropIndex(
                name: "IX_Standings_LeagueId",
                table: "Standings");

            migrationBuilder.DropIndex(
                name: "IX_StandingRow_StandingsSeasonId_StandingsLeagueId",
                table: "StandingRow");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "StandingsLeagueId",
                table: "StandingRow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Standings",
                table: "Standings",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingRow_StandingsSeasonId",
                table: "StandingRow",
                column: "StandingsSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_StandingRow_Standings_StandingsSeasonId",
                table: "StandingRow",
                column: "StandingsSeasonId",
                principalTable: "Standings",
                principalColumn: "SeasonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
