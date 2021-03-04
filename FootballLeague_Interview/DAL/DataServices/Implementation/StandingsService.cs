using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.DAL.Entities;
using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL.DataServices.Implementation
{
    public class StandingsService : ServiceBase, IStandingsService
    {
        public StandingsService(FootballLeagueDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> InitiateAsync(InitiateStandingsRequest initiateStandingsRequest)
        {
            var newSeason = Season.FromISOString(initiateStandingsRequest.Season);

            bool standingsAlreadyExist = await _dbContext.Standings.FindAsync(newSeason.FullName, initiateStandingsRequest.LeagueName) != null;
            if (standingsAlreadyExist)
                throw new ArgumentException("Standings for this league/season already exists");

            var existingLeague = await _dbContext.Leagues
                                .Include(l => l.Teams)
                                .SingleOrDefaultAsync(l => l.Name == initiateStandingsRequest.LeagueName);
            bool leagueAlreadyExists = existingLeague != null;
            if (!leagueAlreadyExists)
                throw new ArgumentException("Cannot instantiate standings for the given League as it doesn't exist");

            var existingSeason = await _dbContext.Seasons.FindAsync(newSeason.FullName);
            var seasonToUse = existingSeason ?? newSeason;

            var newStandings = new Standings
            {
                League = existingLeague,
                Season = seasonToUse,
                StandingRows = existingLeague.Teams.Select(t => StandingRow.EmptyRow(t)).ToArray()
            };

            _dbContext.Standings.Add(newStandings);
            await _dbContext.SaveChangesAsync();

            return "todo: generate URL";
        }
        public async Task<IEnumerable<StandingsDTO>> FindAsync(FindStandingsParams findTeamParams)
        {
            IQueryable<Standings> standingsQuery = _dbContext.Standings
                                                    .Include(s => s.League)
                                                    .Include(s => s.StandingRows)
                                                        .ThenInclude(r => r.Team);
            
            if (findTeamParams.LeagueName != null)
                standingsQuery = standingsQuery.Where(s => s.LeagueId == findTeamParams.LeagueName);

            if (findTeamParams.Season != null)
            {
                var toSearch = Season.FromISOString(findTeamParams.Season);
                standingsQuery = standingsQuery.Where(s => s.SeasonId == toSearch.FullName);
            }

            return (await standingsQuery.ToListAsync())
                .Select(s => s.ToDto());
        }
        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(StandingsRowDTO toUpdate)
        {
            throw new NotImplementedException();
        }


    }
}
