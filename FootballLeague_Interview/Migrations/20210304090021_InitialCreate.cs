using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeague_Interview.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Standings",
                columns: table => new
                {
                    OfSeason = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standings", x => x.OfSeason);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomesticLeagueName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_DomesticLeagueName",
                        column: x => x.DomesticLeagueName,
                        principalTable: "Leagues",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeTeamId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AwayTeamId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GoalsScoredHomeTeam = table.Column<int>(type: "int", nullable: false),
                    GoalsScoredAwayTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Results_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StandingRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Played = table.Column<int>(type: "int", nullable: false),
                    GoalsScored = table.Column<int>(type: "int", nullable: false),
                    GoalsConceived = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    StandingsOfSeason = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandingRow_Standings_StandingsOfSeason",
                        column: x => x.StandingsOfSeason,
                        principalTable: "Standings",
                        principalColumn: "OfSeason",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StandingRow_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_AwayTeamId",
                table: "Results",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_HomeTeamId",
                table: "Results",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingRow_StandingsOfSeason",
                table: "StandingRow",
                column: "StandingsOfSeason");

            migrationBuilder.CreateIndex(
                name: "IX_StandingRow_TeamId",
                table: "StandingRow",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DomesticLeagueName",
                table: "Teams",
                column: "DomesticLeagueName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "StandingRow");

            migrationBuilder.DropTable(
                name: "Standings");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
