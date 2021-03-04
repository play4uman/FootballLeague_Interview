using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class Team
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DomesticLeague DomesticLeague { get; set; }
    }
}
