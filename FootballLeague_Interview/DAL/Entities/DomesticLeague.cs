﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.Entities
{
    public class DomesticLeague
    {
        [Required]
        [Key]
        public string Name { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}