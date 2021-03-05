using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class Result
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string LeagueId { get; set; }
        public DomesticLeague League { get; set; }

        [Required]
        public string HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        [Required]
        public string AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        [Required]
        public string SeasonId { get; set; }
        public Season Season { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsScoredHomeTeam { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsScoredAwayTeam { get; set; }

        public bool Draw { get => GoalsScoredHomeTeam == GoalsScoredAwayTeam; }
        public Team Winner 
        {
            get 
            {
                if (Draw)
                    return null;

                return GoalsScoredHomeTeam > GoalsScoredAwayTeam ? HomeTeam : AwayTeam;
            } 
        }
        
        [ForeignKey("SeasonId, LeagueId")]
        public Standings OfStandings { get; set; }

        public static Result FromRequest(PostResultRequest postResultRequest)
        {
            var result = new Result
            {
                SeasonId = postResultRequest.Season,
                LeagueId = postResultRequest.LeagueName,
                HomeTeamId = Team.GetIdFromNameAndLeague(postResultRequest.HomeTeamName, postResultRequest.LeagueName),
                AwayTeamId = Team.GetIdFromNameAndLeague(postResultRequest.AwayTeamName, postResultRequest.LeagueName),
                GoalsScoredHomeTeam = postResultRequest.GoalsScoredByHomeTeam,
                GoalsScoredAwayTeam = postResultRequest.GoalsScoredByAwayTeam,
            };

            return result;
        }

        public ResultDTO ToDto()
        {
            return new ResultDTO
            {
                Season = SeasonId,
                LeagueName = LeagueId,
                HomeTeamName = HomeTeam?.Name,
                AwayTeamName = AwayTeam?.Name,
                GoalsScoredByAwayTeam = GoalsScoredAwayTeam,
                GoalsScoredByHomeTeam = GoalsScoredHomeTeam,
                Winner = GetDtoWinner()
            };
        }

        private Winner GetDtoWinner()
        {
            if (Winner == null)
                return Shared.DTO.Response.Winner.Draw;
            else if (Winner.Name == HomeTeam.Name)
                return Shared.DTO.Response.Winner.Home;
            else
                return Shared.DTO.Response.Winner.Away;
        }
    }
}
