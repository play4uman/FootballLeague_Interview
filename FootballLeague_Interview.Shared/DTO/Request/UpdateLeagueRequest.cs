using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class UpdateLeagueRequest
    {
        public string Country { get; set; }
        public string[] RemoveTeams { get; set; }
        public string[] AddTeams { get; set; }
    }
}
