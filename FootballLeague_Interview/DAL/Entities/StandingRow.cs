using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class StandingRow
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TeamId { get; set; }
        public Team Team { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Played { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsScored { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsConceded { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Wins { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Draws { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Losses { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Points { get; set; }

        public static StandingRow EmptyRow(Team team)
        {
            return new StandingRow
            {
                Id = Guid.NewGuid(),
                Team = team,
                Played = 0,
                Wins = 0,
                Draws = 0,
                Losses = 0,
                GoalsScored = 0,
                GoalsConceded = 0,
                Points = 0,
            };
        }
    }
}
