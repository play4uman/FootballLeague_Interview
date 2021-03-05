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

        public async Task DeleteAsync(string id)
        {
            var leagueEntity = await _dbContext.Leagues.FindAsync(id);
            if (leagueEntity == null)
                throw new ArgumentException($"No league named {id} exists");

            _dbContext.Remove(leagueEntity);
            await _dbContext.SaveChangesAsync();
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

        public async Task<string> UpdateAsync((string leagueName, UpdateLeagueRequest updateLeagueRequest) updateArgs)
        {
            (string leagueName, UpdateLeagueRequest updateLeagueRequest) = updateArgs;
            var leagueEntity = await _dbContext.Leagues
                                .Include(l => l.Teams)                
                                .SingleOrDefaultAsync(l => l.Name.Equals(leagueName, StringComparison.OrdinalIgnoreCase));
            if (leagueEntity == null)
                throw new ArgumentException("No such league exists");

            if (updateLeagueRequest.Country != null)
                updateLeagueRequest.Country = updateLeagueRequest.Country;

            if (updateLeagueRequest.AddTeams != null)
                await AddOrRemoveteams(leagueEntity, updateLeagueRequest.AddTeams, true);

            if (updateLeagueRequest.RemoveTeams != null)
                await AddOrRemoveteams(leagueEntity, updateLeagueRequest.RemoveTeams, false);

            return "todo: generate URL";
        }

        private async Task AddOrRemoveteams(DomesticLeague leagueEntity, string[] teamNames, bool add)
        {
            string addedOrRemovedExceptionMessage = add ? "Added" : "Removed";
            foreach (var teamToAddName in teamNames)
            {
                bool teamAlreadyExistsInLeague = leagueEntity.Teams.Any(t => t.Name.Equals(teamToAddName, StringComparison.OrdinalIgnoreCase));
                if (teamAlreadyExistsInLeague)
                    throw new ArgumentException($"The team {teamToAddName} already exists in league {leagueEntity.Name}");

                var teamEntity = await _dbContext.Teams.FindAsync(leagueEntity.Name, teamToAddName);
                if (teamEntity == null)
                    throw new ArgumentException($"The team {teamToAddName} listed in {addedOrRemovedExceptionMessage} teams does not exist");

                leagueEntity.Teams.Add(teamEntity);
            }
        }
    }
}
