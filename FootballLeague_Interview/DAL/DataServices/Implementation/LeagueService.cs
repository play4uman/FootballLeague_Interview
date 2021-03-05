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

        public async Task<IEnumerable<LeagueDTO>> FindAsync(FindLeagueParams findLeagueParams)
        {
            IQueryable<DomesticLeague> leaguesQuery = _dbContext.Leagues
                                                        .Include(l => l.Teams);
            if (findLeagueParams.Country != null)
                leaguesQuery = leaguesQuery.Where(t => t.Country.Equals(findLeagueParams.Country));

            if (findLeagueParams.LeagueNames != null)
                leaguesQuery = leaguesQuery.Where(t => findLeagueParams.LeagueNames.Contains(t.Name));


            return (await leaguesQuery.ToListAsync())
                        .Select(t => t.ToDto());
        }

        public async Task<(string, LeagueDTO)> AddAsync(PostLeagueRequest postLeagueRequest)
        {
            var leagueEntity = DomesticLeague.FromRequest(postLeagueRequest);
            var initialTeamsAsRequests = (postLeagueRequest.InitialTeams ?? Enumerable.Empty<string>())
                                            .Select(it => new PostTeamRequest
                                            {
                                                LeagueName = postLeagueRequest.LeagueName,
                                                Name = it
                                            });
            leagueEntity.Teams = new List<Team>();
            foreach (var postTeamRequest in initialTeamsAsRequests) 
            {
                var newTeam = Team.FromRequest(postTeamRequest);
                leagueEntity.Teams.Add(newTeam);
            }

            _dbContext.Leagues.Add(leagueEntity);
            await _dbContext.SaveChangesAsync();

            return ("todo: generate URL", leagueEntity.ToDto());
        }

        public async Task DeleteAsync(string id)
        {
            var leagueEntity = await _dbContext.Leagues.FindAsync(id);
            if (leagueEntity == null)
                throw new ArgumentException($"No league named {id} exists");

            _dbContext.Remove(leagueEntity);
            await _dbContext.SaveChangesAsync();
        }



        public async Task<string> UpdateAsync((string leagueName, UpdateLeagueRequest updateLeagueRequest) updateArgs)
        {
            (string leagueName, UpdateLeagueRequest updateLeagueRequest) = updateArgs;
            var leagueEntity = await _dbContext.Leagues
                                .Include(l => l.Teams)                
                                .SingleOrDefaultAsync(l => l.Name.Equals(leagueName));
            if (leagueEntity == null)
                throw new ArgumentException("No such league exists");

            if (updateLeagueRequest.Country != null)
                updateLeagueRequest.Country = updateLeagueRequest.Country;

            if (updateLeagueRequest.AddTeams != null)
                await AddOrRemoveteams(leagueEntity, updateLeagueRequest.AddTeams, true);

            if (updateLeagueRequest.RemoveTeams != null)
                await AddOrRemoveteams(leagueEntity, updateLeagueRequest.RemoveTeams, false);

            await _dbContext.SaveChangesAsync();
            return "todo: generate URL";
        }

        private async Task AddOrRemoveteams(DomesticLeague leagueEntity, string[] teamNames, bool add)
        {
            string addedOrRemovedExceptionMessage = add ? "Added" : "Removed";
            
            bool shouldAlreadyExist = !add; // if we want to add a new team to a league it shouldn't already exist there and vice versa
            foreach (var teamToAddName in teamNames)
            {
                bool teamAlreadyExistsInLeague = leagueEntity.Teams.Any(t => t.Name == teamToAddName);
                if (teamAlreadyExistsInLeague && !shouldAlreadyExist)
                    throw new ArgumentException($"The team {teamToAddName} already exists in league {leagueEntity.Name}");

                var teamEntity = await _dbContext.Teams
                        .SingleOrDefaultAsync(t => t.Name == teamToAddName  && t.DomesticLeagueName == leagueEntity.Name);
                
                if (teamEntity == null) // the team in the request doesn't exist in the database
                {
                    if (shouldAlreadyExist) // if it's requested to remove a team that doesn't exist - throw error
                    { 
                        throw new ArgumentException($"The team {teamToAddName} listed in {addedOrRemovedExceptionMessage} teams does not exist");
                    }
                    else // we need to add the requested team to the database
                    {
                        teamEntity = new Team
                        {
                            Id = Team.GetIdFromNameAndLeague(teamToAddName, leagueEntity.Name),
                            Name = teamToAddName
                        };
                    }  
                }

                if (add)
                    leagueEntity.Teams.Add(teamEntity);
                else
                    leagueEntity.Teams.Remove(teamEntity);
            }
        }
    }
}
