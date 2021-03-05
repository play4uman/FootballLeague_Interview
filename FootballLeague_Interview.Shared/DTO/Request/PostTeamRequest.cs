using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Request
{
    public class PostTeamRequest
    {
        [Required]
        [StringLength(450)]
        public string Name { get; set; }
        [Required]
        [StringLength(450)]
        public string LeagueName { get; set; }
    }
}
