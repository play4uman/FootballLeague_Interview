using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class PostLeagueRequest
    {
        public string LeagueName { get; set; }
        public string[] InitialTeams { get; set; }
    }
}
