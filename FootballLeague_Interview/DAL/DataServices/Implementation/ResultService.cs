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
            var result = await ValidateAndGenerateResultAsync(toAdd);
            var resultAsDto = result.ToDto();


            await _standingsService.UpdateMatchAsync(resultAsDto);
            return "todo: generate URL";
        }

        private async Task<Result> ValidateAndGenerateResultAsync(PostResultRequest toAdd)
        {
            var season = Season.FromISOString(toAdd.Season);
            var standing = await _dbContext.Standings
                            .Include(s => s.ResultsDuringTheSeason)
                            .Include(s => s.League)
                                .ThenInclude(l => l.Teams)
                            .SingleOrDefaultAsync(s => s.SeasonId == season.FullName && s.LeagueId == toAdd.LeagueName);
            bool standingExists = standing != null;
            if (!standingExists)
                throw new ArgumentException("Can't add the given result as there is no such standing that corresponds to this combination of year/league");

            var homeTeamEntity = standing.League.Teams.FirstOrDefault(
                    t => t.Name.Equals(toAdd.HomeTeamName, StringComparison.OrdinalIgnoreCase));
            var awayTeamEntity = standing.League.Teams.FirstOrDefault(
                    t => t.Name.Equals(toAdd.AwayTeamName, StringComparison.OrdinalIgnoreCase));

            if (homeTeamEntity == null || awayTeamEntity == null)
                throw new ArgumentException("One of the teams, part of this result, does not exist");

            var entityToAdd = Result.FromRequest(toAdd);
            entityToAdd.HomeTeam = homeTeamEntity;
            entityToAdd.AwayTeam = awayTeamEntity;
            return entityToAdd;
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
