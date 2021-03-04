using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.FindParameters
{
    public class FindTeamParams
    {
        public string League { get; set; }
        public string[] TeamNames { get; set; }
    }
}
