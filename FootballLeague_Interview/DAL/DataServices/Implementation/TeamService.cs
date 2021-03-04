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
    public class TeamService : ServiceBase, ITeamService
    {
        public TeamService(FootballLeagueDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<TeamDTO>> FindAsync(FindTeamParams findTeamParams)
        {
            IQueryable<Team> teamsQuery = _dbContext.Teams;
            if (findTeamParams.League != null)
                teamsQuery = teamsQuery.Where(t => t.DomesticLeagueName.Equals(findTeamParams.League, StringComparison.OrdinalIgnoreCase));

            if (findTeamParams.TeamNames != null)
                teamsQuery = teamsQuery.Where(t => findTeamParams.TeamNames.Contains(t.Name));

            
            return (await teamsQuery.ToListAsync())
                        .Select(t => t.ToDto());
        }

        public async Task<string> AddAsync(PostTeamRequest postTeamRequest)
        {
            var toAddEntity = Team.FromRequest(postTeamRequest);
            bool alreadyExists = (await _dbContext.Teams.FindAsync(toAddEntity.Id)) != null;
            if (alreadyExists)
                throw new ArgumentException($"Team with name {toAddEntity.Name} in league {toAddEntity.DomesticLeagueName} already exists");

            _dbContext.Teams.Add(toAddEntity);
            await _dbContext.SaveChangesAsync();
            return "todo: URL";
        }

        public async Task<string> UpdateAsync(Team toUpdate)
        {
            var existingEntity = await _dbContext.Teams.FindAsync(toUpdate.Id);
            bool alreadyExists = existingEntity != null;
            if (!alreadyExists)
                throw new ArgumentException($"Team with name {toUpdate.Name} in league {toUpdate.DomesticLeagueName} does not exist");

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(toUpdate);
            await _dbContext.SaveChangesAsync();
            return "todo: URL";
        }

        public async Task DeleteAsync(object toDeleteId)
        {
            var existingEntity = await _dbContext.Teams.FindAsync(toDeleteId);
            var alreadyExists = existingEntity != null;
            if(!alreadyExists)
                throw new ArgumentException($"Team with Id {toDeleteId} does not exist");

            _dbContext.Teams.Remove(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
