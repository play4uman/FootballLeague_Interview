using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class Team
    {
        public const string LeagueNameDelimeter = "_";
        // We assume that every team is uniquely defined by the combination of the team's name and the name of the league the team plays in.
        [Key]
        public string Id { get => $"{Name}{LeagueNameDelimeter}{DomesticLeagueName}"; private set => this.Id = value;  }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey(nameof(DomesticLeague))]
        public string DomesticLeagueName { get; set; }
        public DomesticLeague DomesticLeague { get; set; }

        public ICollection<Result> HomeResults { get; set; }
        public ICollection<Result> AwayResults { get; set; }
    }
}
