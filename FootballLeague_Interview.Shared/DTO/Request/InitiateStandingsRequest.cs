using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class InitiateStandingsRequest
    {
        [Required]
        [StringLength(450)]
        public string LeagueName { get; set; }
        [Required]
        [RegularExpression(@"(\d+)/(\d+)")]
        public string Season { get; set; }
    }
}
