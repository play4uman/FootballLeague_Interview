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
    public class ResultController : ControllerBase
    {
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        private readonly IResultService _resultService;

        /// <summary>
        /// Retrieves a list of Result objects
        /// </summary>
        /// <param name="season">Filter Results based on Season name. Should be in the format xxxx/xxxx(+1). E.g. 2020/2021</param>
        /// <param name="league">Filter Results based on the League the matches were played in</param>
        /// <param name="homeTeam">Filter Results based on the Home Team relative to the match</param>
        /// <param name="awayTeam">Filter Results based on the Away Team relative to the match</param>
        /// <response code="200">Query is valid. The resulting collection may be empty though</response>
        /// <response code="400">Cannot find Results with the given parameters</response>

        [HttpGet]
        public async Task<ActionResult<ResultDTO>> GetResult(
            [RegularExpression(@"(\d+)/(\d+)")] string season, 
            string league, 
            string homeTeam, 
            string awayTeam)
        {
            try
            {
                var result = await _resultService.FindAsync(new FindResultParams
                {
                    Season = season,
                    LeagueName = league,
                    HomeTeamName = homeTeam,
                    AwayTeamName = awayTeam
                });
                return Ok(result.First());
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }

        /// <summary>
        /// Create a new Result object
        /// </summary>
        /// <param name="postResultRequest">Request used for creating the League object</param>
        /// <response code="201">Result has been created</response>
        /// <response code="400">Cannot create Result from the given request</response>

        [HttpPost("add")]
        public async Task<ActionResult> PostResult(PostResultRequest postResultRequest)
        {
            {
                try
                {
                    var result = await _resultService.AddAsync(postResultRequest);

                    return Created(result.url, result.createdDto);
                }
                catch (ArgumentException aEx)
                {
                    return BadRequest(aEx.Message);
                }
            }
        }

        // Since a Result is like a transaction it doesn't make sense to update one. If it's a product of an error, the bad result
        // should be just deleted using the DELETE endpoint and it's results on the Standings should be rolled back.


        /// <summary>
        /// Deletes an existing Result object
        /// </summary>
        /// <param name="season">The deleted Result must be from this Season</param>
        /// <param name="league">The deleted Result must be from this League</param>
        /// <param name="homeTeam">The deleted Result's Home team name should be equal to this value</param>
        /// <param name="awayTeam">The deleted Result's Away team name should be equal to this value</param>
        /// <param name="rollback">Whether the respective Standings should be rollbacked with relevance to the deleted result</param>
        /// <response code="200">The Result has been deleted</response>
        /// <response code="400">Cannot delete Result with the given parameters</response>

        [HttpDelete]
        public async Task<ActionResult> DeleteResult(
            [Required, RegularExpression(@"(\d+)/(\d+)")]string season, 
            [Required]string league, 
            [Required]string homeTeam, 
            [Required]string awayTeam, 
            bool? rollback)
        {
            try
            {
                await _resultService.DeleteAsync(new DeleteResultRequest
                {
                    Season = season,
                    LeagueName = league,
                    HomeTeamName = homeTeam,
                    AwayTeamName = awayTeam,
                    RollbackStandings = rollback.HasValue && rollback.Value
                });
                return Ok();
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }
    }
}
