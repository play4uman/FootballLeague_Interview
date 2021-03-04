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
    }
}
