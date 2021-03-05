using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class Season
    {
        public const string StartEndOfSeasonDelimiter = "-";

        private string fullName = null;
        [Key]
        public string FullName
        {
            get
            {
                if (fullName == null)
                    fullName = $"{YearStartOfSeason}{StartEndOfSeasonDelimiter}{YearEndOfSeason}";
                return fullName;
            }
            set
            {
                fullName = value;
            }
        }

        [Required]
        [Range(1850, 4000)]
        public int YearStartOfSeason { get; set; }
        [Required]
        [Range(1850, 4000)]
        public int YearEndOfSeason { get; set; }

        
        public static Season FromISOString(string isoSeasonName)
        {
            var yearRange = ExtractStartEndYears(isoSeasonName);
            return new Season
            {
                YearStartOfSeason = yearRange.seasonStartYear,
                YearEndOfSeason = yearRange.seasonEndYear
            };
        }

        // ex. seasonName = 2018/2019
        private static (int seasonStartYear, int seasonEndYear) ExtractStartEndYears(string seasonName)
        {
            var regex = new Regex(@"(\d+)/(\d+)");
            var match = regex.Match(seasonName);
            return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }
    }
}
