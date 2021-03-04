using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague_Interview.Shared.DTO.Response
{
    public enum Winner
    {
        Draw,
        Home,
        Away
    }
    public class ResultDTO
    {
        public string Season { get; set; }
        public string LeagueName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int GoalsScoredByHomeTeam { get; set; }
        public int GoalsScoredByAwayTeam { get; set; }
        public Winner Winner { get; set; }
    }
}
