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

        public async Task<IEnumerable<ResultDTO>> FindAsync(FindResultParams findTeamParams)
        {
            IQueryable<Result> resultsQuery = _dbContext.Results
                                        .Include(r => r.HomeTeam)
                                        .Include(r => r.AwayTeam);

            if (findTeamParams.Season != null)
                resultsQuery = resultsQuery.Where(r => r.SeasonId == findTeamParams.Season);

            if (findTeamParams.LeagueName != null)
                resultsQuery = resultsQuery.Where(r => r.LeagueId == findTeamParams.LeagueName);

            if (findTeamParams.HomeTeamName != null)
                resultsQuery = resultsQuery.Where(r => r.HomeTeam.Name == findTeamParams.HomeTeamName);

            if (findTeamParams.AwayTeamName != null)
                resultsQuery = resultsQuery.Where(r => r.AwayTeam.Name == findTeamParams.AwayTeamName);

            return (await resultsQuery.ToArrayAsync()).Select(r => r.ToDto());
        }

        public async Task<(string, ResultDTO)> AddAsync(PostResultRequest toAdd)
        {
            var result = await ValidateAndGenerateResultAsync(toAdd);
            var resultAsDto = result.ToDto();


            await _standingsService.UpdateMatchAsync(resultAsDto, false);
            return ("todo: generate URL", resultAsDto);
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
                    t => t.Name.Equals(toAdd.HomeTeamName));
            var awayTeamEntity = standing.League.Teams.FirstOrDefault(
                    t => t.Name.Equals(toAdd.AwayTeamName));

            if (homeTeamEntity == null || awayTeamEntity == null)
                throw new ArgumentException("One of the teams, part of this result, does not exist");

            var entityToAdd = Result.FromRequest(toAdd);
            entityToAdd.HomeTeam = homeTeamEntity;
            entityToAdd.AwayTeam = awayTeamEntity;
            return entityToAdd;
        }

        public async Task DeleteAsync(DeleteResultRequest deleteResultRequest)
        {
            var homeTeamName = deleteResultRequest.HomeTeamName;
            var awayTeamName = deleteResultRequest.AwayTeamName;

            var standingsEntity = await _dbContext.Standings
                .Include(s => s.ResultsDuringTheSeason)
                    .ThenInclude(r => r.HomeTeam)
                .Include(s => s.ResultsDuringTheSeason)
                    .ThenInclude(r => r.AwayTeam)
                .SingleOrDefaultAsync(r => r.SeasonId.Equals(deleteResultRequest.Season) 
                    && r.LeagueId.Equals(deleteResultRequest.LeagueName));

            if (standingsEntity == null)
                throw new ArgumentException($"There are no recorded results for league {deleteResultRequest.LeagueName} " +
                    $"at season {deleteResultRequest.Season}");

            var resultEntity = standingsEntity.ResultsDuringTheSeason
                .FirstOrDefault(r => r.HomeTeam.Name.Equals(homeTeamName) &&
                                        r.AwayTeam.Name.Equals(awayTeamName));
            if (resultEntity == null)
                throw new ArgumentException($"No match between {homeTeamName} and {awayTeamName}" +
                    $" has been played that season");

            _dbContext.Results.Remove(resultEntity);

            await _dbContext.SaveChangesAsync();

            if (deleteResultRequest.RollbackStandings)
                await _standingsService.UpdateMatchAsync(resultEntity.ToDto(), true);
        }
    }
}
