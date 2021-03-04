using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Response
{
    public class StandingsRowDTO
    {
        public string TeamName { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int Points { get; set; }
    }
}
