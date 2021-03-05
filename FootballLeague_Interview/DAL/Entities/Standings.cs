using FootballLeague_Interview.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class Standings
    {
        [Required]
        public string SeasonId { get; set; }
        public Season Season { get; set; }
        [Required]
        public string LeagueId { get; set; }
        public DomesticLeague League { get; set; }
        public ICollection<StandingRow> StandingRows { get; set; }
        public ICollection<Result> ResultsDuringTheSeason { get; set; }

        public StandingsDTO ToDto()
        {
            var result = new StandingsDTO
            {
                LeagueName = League.Name,
                Season = SeasonId,
                Rows = StandingRows.Select(sr => new StandingsRowDTO
                {
                    LeagueName = League.Name,
                    Season = SeasonId,
                    TeamName = sr.Team.Name,
                    Played = sr.Played,
                    Wins = sr.Wins,
                    Draws = sr.Draws,
                    Losses = sr.Losses,
                    GoalsScored = sr.GoalsScored,
                    GoalsConceded = sr.GoalsConceded,
                    Points = sr.Points
                })
                .OrderByDescending(sr => sr.Points)
                .ThenBy(sr => sr.TeamName)
            };

            return result;
        }
    }
}
