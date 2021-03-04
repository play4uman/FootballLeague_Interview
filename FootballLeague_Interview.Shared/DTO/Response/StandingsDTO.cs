using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Response
{
    public class StandingsDTO
    {
        public string LeagueName { get; set; }
        public string Season { get; set; }
        public IEnumerable<StandingsRowDTO> Rows { get; set; }
    }
}
