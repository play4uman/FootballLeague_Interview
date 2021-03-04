using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Response
{
    public class LeagueDTO
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public IEnumerable<string> Teams { get; set; }
    }
}
