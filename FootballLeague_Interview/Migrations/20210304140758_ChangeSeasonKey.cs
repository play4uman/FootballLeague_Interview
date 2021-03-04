using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeague_Interview.Migrations
{
    public partial class ChangeSeasonKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Seasons_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Standings_Seasons_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Standings");

            migrationBuilder.DropIndex(
                name: "IX_Standings_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Standings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

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
                name: "SeasonYearEndOfSeason",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SeasonYearStartOfSeason",
                table: "Results");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Seasons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SeasonId",
                table: "Results",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Results_SeasonId",
                table: "Results",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Seasons_SeasonId",
                table: "Results",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "FullName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Standings_Seasons_SeasonId",
                table: "Standings",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "FullName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Seasons_SeasonId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Standings_Seasons_SeasonId",
                table: "Standings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Results_SeasonId",
                table: "Results");

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

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Seasons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SeasonId",
                table: "Results",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                columns: new[] { "YearStartOfSeason", "YearEndOfSeason" });

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
                name: "FK_Standings_Seasons_SeasonYearStartOfSeason_SeasonYearEndOfSeason",
                table: "Standings",
                columns: new[] { "SeasonYearStartOfSeason", "SeasonYearEndOfSeason" },
                principalTable: "Seasons",
                principalColumns: new[] { "YearStartOfSeason", "YearEndOfSeason" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
