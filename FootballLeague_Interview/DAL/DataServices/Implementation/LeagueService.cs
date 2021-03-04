using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.DAL.Entities;
using FootballLeague_Interview.Shared.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Implementation
{
    public class LeagueService : ServiceBase, ILeagueService
    {
        public LeagueService(FootballLeagueDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> AddAsync(PostLeagueRequest postLeagueRequest)
        {
            var newLeague = DomesticLeague.FromRequest(postLeagueRequest);
            var initialTeamsAsRequests = (postLeagueRequest.InitialTeams ?? Enumerable.Empty<string>())
                                            .Select(it => new PostTeamRequest
                                            {
                                                LeagueName = postLeagueRequest.LeagueName,
                                                Name = it
                                            });
            newLeague.Teams = new List<Team>();
            foreach (var postTeamRequest in initialTeamsAsRequests) 
            {
                var newTeam = Team.FromRequest(postTeamRequest);
                newLeague.Teams.Add(newTeam);
            }

            _dbContext.Leagues.Add(newLeague);
            await _dbContext.SaveChangesAsync();

            return "todo: generate URL";
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DomesticLeague>> FindAsync(FindLeagueParams findTeamParams)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(DomesticLeague toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
