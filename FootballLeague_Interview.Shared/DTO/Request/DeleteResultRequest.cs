using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class DeleteResultRequest
    {
        public string Season { get; set; }
        public string LeagueName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public bool RollbackStandings { get; set; }
    }
}
