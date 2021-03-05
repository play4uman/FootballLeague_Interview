using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class UpdateLeagueRequest
    {
        [Required]
        [StringLength(450)]
        public string Country { get; set; }
        public string[] RemoveTeams { get; set; }
        public string[] AddTeams { get; set; }
    }
}
