using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeague_Interview.Migrations
{
    public partial class AddSeasonEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StandingRow_Standings_StandingsOfSeason",
                table: "StandingRow");

            migrationBuilder.RenameColumn(
                name: "OfSeason",
                table: "Standings",
                newName: "SeasonId");

            migrationBuilder.RenameColumn(
                name: "StandingsOfSeason",
                table: "StandingRow",
                newName: "StandingsSeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_StandingRow_StandingsOfSeason",
                table: "StandingRow",
                newName: "IX_StandingRow_StandingsSeasonId");

            migrationBuilder.AddColumn<int>(
                name: "SeasonYearEndOfSeason",
                table: "Standings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeasonYearStartOfSeason",
                table: "Standings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SeasonId",
                table: "Results",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SeasonYearEndOfSeason",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeasonYearStartOfSeason",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    YearStartOfSeason = table.Column<int>(type: "int", nullable: false),
                    YearEndOfSeason = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => new { x.YearStartOfSeason, x.YearEndOfSeason });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Standings",
                columns: new[] { "SeasonYearStartOfSeason", "SeasonYearEndOfSeason" });

            migrationBuilder.CreateIndex(
                name: "IX_Results_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Results",
                columns: new[] { "SeasonYearStartOfSeason", "SeasonYearEndOfSeason" });

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Seasons_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Results",
                columns: new[] { "SeasonYearStartOfSeason", "SeasonYearEndOfSeason" },
                principalTable: "Seasons",
                principalColumns: new[] { "YearStartOfSeason", "YearEndOfSeason" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StandingRow_Standings_StandingsSeasonId",
                table: "StandingRow",
                column: "StandingsSeasonId",
                principalTable: "Standings",
                principalColumn: "SeasonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Standings_Seasons_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Standings",
                columns: new[] { "SeasonYearStartOfSeason", "SeasonYearEndOfSeason" },
                principalTable: "Seasons",
                principalColumns: new[] { "YearStartOfSeason", "YearEndOfSeason" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Seasons_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_StandingRow_Standings_StandingsSeasonId",
                table: "StandingRow");

            migrationBuilder.DropForeignKey(
                name: "FK_Standings_Seasons_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Standings");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Standings_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Standings");

            migrationBuilder.DropIndex(
                name: "IX_Results_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SeasonYearEndOfSeason",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "SeasonYearStartOfSeason",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SeasonYearEndOfSeason",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SeasonYearStartOfSeason",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "SeasonId",
                table: "Standings",
                newName: "OfSeason");

            migrationBuilder.RenameColumn(
                name: "StandingsSeasonId",
                table: "StandingRow",
                newName: "StandingsOfSeason");

            migrationBuilder.RenameIndex(
                name: "IX_StandingRow_StandingsSeasonId",
                table: "StandingRow",
                newName: "IX_StandingRow_StandingsOfSeason");

            migrationBuilder.AddForeignKey(
                name: "FK_StandingRow_Standings_StandingsOfSeason",
                table: "StandingRow",
                column: "StandingsOfSeason",
                principalTable: "Standings",
                principalColumn: "OfSeason",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
