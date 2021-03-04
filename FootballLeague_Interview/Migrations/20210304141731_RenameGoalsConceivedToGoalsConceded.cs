using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeague_Interview.Migrations
{
    public partial class RenameGoalsConceivedToGoalsConceded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoalsConceived",
                table: "StandingRow",
                newName: "GoalsConceded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoalsConceded",
                table: "StandingRow",
                newName: "GoalsConceived");
        }
    }
}
