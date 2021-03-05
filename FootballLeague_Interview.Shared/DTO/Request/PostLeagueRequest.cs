using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class PostLeagueRequest
    {
        [Required]
        [StringLength(450)]
        public string LeagueName { get; set; }
        [Required]
        [StringLength(450)]
        public string Country { get; set; }
        public string[] InitialTeams { get; set; }
    }
}
