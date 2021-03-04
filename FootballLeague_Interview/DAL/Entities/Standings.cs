using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class Standings
    {
        [Key]
        public string OfSeason { get; set; }
        public ICollection<StandingRow> StandingRows { get; set; }
    }
}
