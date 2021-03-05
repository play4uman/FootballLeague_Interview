using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class DeleteResultRequest
    {
        [Required]
        [RegularExpression(@"(\d+)/(\d+)")]
        public string Season { get; set; }
        [Required]
        [StringLength(450)]
        public string LeagueName { get; set; }
        [Required]
        [StringLength(450)]
        public string HomeTeamName { get; set; }
        [Required]
        [StringLength(450)]
        public string AwayTeamName { get; set; }
        public bool RollbackStandings { get; set; }
    }
}
