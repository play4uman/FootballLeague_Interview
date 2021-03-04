using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.FindParameters
{
    public class FindLeagueParams
    {
        public string Country { get; set; } 
        public string[] LeagueNames { get; set; }
    }
}
