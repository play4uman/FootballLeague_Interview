using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class StandingRow
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TeamId { get; set; }
        public Team Team { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Played { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsScored { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GoalsConceived { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Wins { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Draws { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Losses { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Points { get; set; }
    }
}
