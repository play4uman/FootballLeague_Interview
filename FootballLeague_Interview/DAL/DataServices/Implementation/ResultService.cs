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
    public class ResultService : ServiceBase, IResultService
    {
        public ResultService(FootballLeagueDbContext dbContext, IStandingsService standingsService) : base(dbContext)
        {
            _standingsService = standingsService;
        }

        private readonly IStandingsService _standingsService;

        public Task<IEnumerable<ResultDTO>> FindAsync(FindResultParams findTeamParams)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddAsync(PostResultRequest toAdd)
        {
            var season = Season.FromISOString(toAdd.Season);
            var standing = _dbContext.Standings
                            .Include(s => s.ResultsDuringTheSeason)
                            .SingleOrDefault(s => s.SeasonId == toAdd.Season && s.LeagueId == toAdd.LeagueName);
            bool standingExists = standing != null;
            if (!standingExists)
                throw new ArgumentException("Can't add the given result as there is no such standing that corresponds to this combination of year/league");

            var entityToAdd = Result.FromRequest(toAdd);
            standing.ResultsDuringTheSeason.Add(entityToAdd);

            await _dbContext.SaveChangesAsync();
            return "todo: generate URL";
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }



        public Task<string> UpdateAsync(ResultDTO toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
