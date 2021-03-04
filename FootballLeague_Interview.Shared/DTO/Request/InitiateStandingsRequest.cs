using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class InitiateStandingsRequest
    {
        public string LeagueName { get; set; }
        public string Season { get; set; }
    }
}
