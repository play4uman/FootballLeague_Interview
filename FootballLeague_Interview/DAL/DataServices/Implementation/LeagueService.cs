using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.DAL.Entities;
using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<LeagueDTO>> FindAsync(FindLeagueParams findLeagueParams)
        {
            IQueryable<DomesticLeague> leaguesQuery = _dbContext.Leagues
                                                        .Include(l => l.Teams);
            if (findLeagueParams.Country != null)
                leaguesQuery = leaguesQuery.Where(t => t.Country.Equals(findLeagueParams.Country, StringComparison.OrdinalIgnoreCase));

            if (findLeagueParams.LeagueNames != null)
                leaguesQuery = leaguesQuery.Where(t => findLeagueParams.LeagueNames.Contains(t.Name));


            return (await leaguesQuery.ToListAsync())
                        .Select(t => t.ToDto());
        }

        public Task<string> UpdateAsync(DomesticLeague toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
