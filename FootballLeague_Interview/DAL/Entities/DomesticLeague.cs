using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
using System;
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

        [Required]
        public string Country { get; set; }
        public ICollection<Team> Teams { get; set; }

        public static DomesticLeague FromRequest(PostLeagueRequest postLeagueRequest)
        {
            return new DomesticLeague
            {
                Name = postLeagueRequest.LeagueName
            };
        }

        public LeagueDTO ToDto()
        {
            return new LeagueDTO
            {
                Name = this.Name,
                Country = this.Country,
                Teams = Teams.Select(t => t.Name)
            };
        }
    }
}
