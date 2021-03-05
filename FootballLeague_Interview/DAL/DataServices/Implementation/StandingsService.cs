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

        public const int PointsForWin = 3;
        public const int PointsForDraw = 1;
        public const int PointsForLoss = 0;

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
        public async Task DeleteAsync((string leagueName, string season) deleteParams)
        {
            var seasonId = Season.FromISOString(deleteParams.season).FullName;
            var standingsEntity = await _dbContext.Standings.FindAsync(seasonId, deleteParams.leagueName);
            if (standingsEntity == null)
                throw new ArgumentException("Standings for this league/season does not exist");

            _dbContext.Standings.Remove(standingsEntity);
            await _dbContext.SaveChangesAsync();
        }

        // Use this method when there's an error with a given row and you want to change the row manually.
        public async Task<string> UpdateAsync(StandingsRowDTO toUpdate)
        {
            var seasonId = Season.FromISOString(toUpdate.Season).FullName;
            var standingsEntity = await _dbContext.Standings
                .Include(s => s.StandingRows)
                    .ThenInclude(r => r.Team)
                .SingleOrDefaultAsync(r => r.SeasonId == seasonId && r.LeagueId == toUpdate.LeagueName);
            if (standingsEntity == null)
                throw new ArgumentException("No standings for this row exist");

            var rowEntity = standingsEntity.StandingRows.FirstOrDefault(r => r.Team.Name.Equals(toUpdate.TeamName));
            if (rowEntity == null)
                throw new ArgumentException("No row with such a team exists");

            var id = rowEntity.Id;
            var newRow = StandingRow.FromDto(toUpdate);
            _dbContext.Entry(rowEntity).CurrentValues.SetValues(newRow);
            rowEntity.Id = id;

            await _dbContext.SaveChangesAsync();
            return "todo: generate URL";
        }

        // Use this method when a result has been added and we want to update the rows for the two participating teams automatically

        public async Task<string> UpdateMatchAsync(ResultDTO resultDTO, bool rollback)
        {
            var seasonId = Season.FromISOString(resultDTO.Season).FullName;
            var standingsEntity = await _dbContext.Standings
                                            .Include(s => s.StandingRows)
                                            .SingleOrDefaultAsync(s => s.SeasonId == seasonId && s.LeagueId == resultDTO.LeagueName);
            var points = GetPoints(resultDTO.Winner);
            UpdateSingleRowAfterAMatch(resultDTO.HomeTeamName, resultDTO.GoalsScoredByHomeTeam, resultDTO.GoalsScoredByAwayTeam, points.homeTeamPoints, standingsEntity, rollback);
            UpdateSingleRowAfterAMatch(resultDTO.AwayTeamName, resultDTO.GoalsScoredByAwayTeam, resultDTO.GoalsScoredByHomeTeam, points.awayTeamPoints, standingsEntity, rollback);

            await _dbContext.SaveChangesAsync();
            return "todo: generate URL";
        }

        private void UpdateSingleRowAfterAMatch(string teamName, int goalsScored, int goalsConceded, int pointsToAdd, 
            Standings standings, bool rollback)
        {
            var teamId = Team.GetIdFromNameAndLeague(teamName, standings.LeagueId);
            var rowEntity = standings.StandingRows.FirstOrDefault(r => r.TeamId == teamId);
            if (rowEntity == null)
                throw new ArgumentException($"Cannot update row in standings as the team {teamName} does not have an associated row." +
                    $"Did you forget to initiate the standings?");

            rowEntity.GoalsScored += !rollback ? goalsScored : -goalsScored; // if we want to rollback a result just substract the desired values
            rowEntity.GoalsConceded += !rollback ? goalsConceded : -goalsConceded;
            rowEntity.Points += !rollback ? pointsToAdd : -pointsToAdd;

            bool wasDraw = pointsToAdd == PointsForDraw;
            bool wasWinner = pointsToAdd == PointsForWin;
            bool wasLoser = pointsToAdd == PointsForLoss;

            int addToMatches = !rollback ? 1 : -1;

            rowEntity.Wins += wasWinner ? addToMatches : 0;
            rowEntity.Draws += wasDraw ? addToMatches : 0;
            rowEntity.Losses += wasLoser ? addToMatches : 0;

            rowEntity.Played += addToMatches;
        }

        private (int homeTeamPoints, int awayTeamPoints) GetPoints(Winner matchWinner)
        {
            int pointsForHomeTeam;
            int pointsForAwayTeam;
            if (matchWinner == Winner.Draw)
            {
                pointsForHomeTeam = PointsForDraw;
                pointsForAwayTeam = PointsForDraw;
            }
            else
            {
                pointsForHomeTeam = matchWinner == Winner.Home ? PointsForWin : PointsForLoss;
                pointsForAwayTeam = matchWinner == Winner.Away ? PointsForWin : PointsForLoss;
            }

            return (pointsForHomeTeam, pointsForAwayTeam);
        }
    }
}
