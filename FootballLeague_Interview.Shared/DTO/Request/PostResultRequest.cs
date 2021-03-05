using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class PostResultRequest
    {
        public string Season { get; set; }
        [Required]
        [StringLength(450)]
        public string LeagueName { get; set; }
        [Required]
        [StringLength(450)]
        public string HomeTeamName { get; set; }
        [Required]
        [StringLength(450)]
        public string AwayTeamName { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsScoredByHomeTeam { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsScoredByAwayTeam { get; set; }
    }
}
