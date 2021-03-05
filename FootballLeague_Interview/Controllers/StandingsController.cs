using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StandingsController : ControllerBase
    {
        public StandingsController(IStandingsService standingsService)
        {
            _standingsService = standingsService;
        }

        private readonly IStandingsService _standingsService;

        /// <summary>
        /// Retrieves a list of Standings objects
        /// </summary>
        /// <param name="leagueName">Filter Results based on the League the matches were played in</param>
        /// <param name="season">Filter Standings based on Season name. Should be in the format xxxx/xxxx(+1). E.g. 2020/2021</param>
        /// <response code="200">Query is valid. The resulting collection may be empty though</response>
        /// <response code="400">Cannot find Standings with the given parameters</response>

        [HttpGet]
        public async Task<ActionResult<StandingsDTO>> GetStandings(string leagueName, string season)
        {
            try
            {
                var result = await _standingsService.FindAsync(new FindStandingsParams
                {
                    LeagueName = leagueName,
                    Season = season
                });

                return Ok(result.First());
            }
            catch (ArgumentException aEx)
            {
                return NotFound(aEx.Message);
            }
        }

        /// <summary>
        /// Initiates a Standings table for a given combination of Season and League. All the values in this table, e.g. Points, Goals Scored,
        /// Goals Conceded, etc. are 0 for all teams. Before a Result can be POST-ed the respective Standings must be first initialized. 
        /// </summary>
        /// <param name="initiateStandingsRequest">Request used for intializing the Standings object</param>
        /// <response code="201">Query is valid. The resulting collection may be empty though</response>
        /// <response code="400">Cannot initialize Standings from the given reques</response>


        [HttpPost("initiate")]
        public async Task<ActionResult> InitiateStandings(InitiateStandingsRequest initiateStandingsRequest)
        {
            try
            {
                var result = await _standingsService.InitiateAsync(initiateStandingsRequest);

                return Created(result.url, result.createdDto);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }

        /// <summary>
        /// Updates an existing Standings object with the result from a match. 
        /// </summary>
        /// <param name="resultDTO">The name of the League object to update</param>
        /// <param name="rollback">If the update should rollback the points/goals/etc. from the Result (if already applied to a Standings object)</param>
        /// <response code="200">The Standings object has been updated</response>
        /// <response code="400">Cannot update Standings object with the given parameters</response>

        [HttpPost("update/match")]
        public async Task<ActionResult> UpdateStandingsWithMatch(ResultDTO resultDTO, [FromQuery]bool? rollback)
        {
            bool shouldRollback = rollback.HasValue && rollback.Value;
            try
            {
                var result = await _standingsService.UpdateMatchAsync(resultDTO, shouldRollback);

                return Ok(result.updatedDto);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }

        /// <summary>
        /// Updates a row from an existing Standings manually. Should be used only there was a human/server error which led to the
        /// corruption of data in one of the Standings tables.
        /// </summary>
        /// <param name="standingsRowDTO">Data with which to update the dirty row</param>
        /// <response code="200">The Standings object has been updated</response>
        /// <response code="400">Cannot update Standings object with the given parameters</response>

        [HttpPost("update")]
        public async Task<ActionResult> UpdateStandingsRow(StandingsRowDTO standingsRowDTO)
        {
            try
            {
                var result = await _standingsService.UpdateAsync(standingsRowDTO);

                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }

        /// <summary>
        /// Deletes an existing Standings object
        /// </summary>
        /// <param name="season">The deleted Standings object must be from this Season</param>
        /// <param name="league">The deleted Standings object must be from this League</param>
        /// <response code="200">The Standings object has been deleted</response>
        /// <response code="400">Cannot delete Standings object with the given parameters</response>

        [HttpDelete]
        public async Task<ActionResult> DeleteStandings(
            [Required, RegularExpression(@"(\d+)/(\d+)")] string season,
            [Required] string league)
        {
            try
            {
                await _standingsService.DeleteAsync((league, season));
                return Ok();
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }

    }
}
