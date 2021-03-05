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
