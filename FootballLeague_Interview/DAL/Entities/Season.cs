using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class Season
    {
        public const string StartEndOfSeasonDelimiter = "-";
        [Key]
        public string FullName { get => $"{YearStartOfSeason}{StartEndOfSeasonDelimiter}{YearEndOfSeason}"; set => this.FullName = value; }

        [Required]
        [Range(1850, 4000)]
        public int YearStartOfSeason { get; set; }
        [Required]
        [Range(1850, 4000)]
        public int YearEndOfSeason { get; set; }
    }
}
