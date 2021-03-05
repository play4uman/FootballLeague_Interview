using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
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
        
        private string id = null;
        [Key]
        public string Id 
        { 
            get
            {
                if (id == null)
                    id = GetIdFromNameAndLeague(Name, DomesticLeagueName);
                return id;
            }
            set
            {
                id = value;
            }
        }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey(nameof(DomesticLeague))]
        public string DomesticLeagueName { get; set; }
        public DomesticLeague DomesticLeague { get; set; }

        public ICollection<Result> HomeResults { get; set; }
        public ICollection<Result> AwayResults { get; set; }

        public static string GetIdFromNameAndLeague(string teamName, string leagueName)
        {
            return $"{teamName}{LeagueNameDelimeter}{leagueName}";
        }
        
        public static Team FromRequest(PostTeamRequest postTeamRequest)
        {
            return new Team
            {
                Name = postTeamRequest.Name,
                DomesticLeagueName = postTeamRequest.LeagueName
            };
        }

        public TeamDTO ToDto()
        {
            return new TeamDTO
            {
                Name = this.Name,
                LeagueName = DomesticLeagueName
            };
        }
    }
}
