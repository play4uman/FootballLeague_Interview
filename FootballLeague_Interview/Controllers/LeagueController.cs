using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.Shared.DTO.Request;
using FootballLeague_Interview.Shared.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController : ControllerBase
    {
        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        private readonly ILeagueService _leagueService;

        [HttpGet]
        public async Task<ActionResult<LeagueDTO>> GetLeagues(string country, [FromQuery] string[] leagueNames)
        {
            try
            {
                var result = await _leagueService.FindAsync(new FindLeagueParams
                {
                    Country = country,
                    LeagueNames = leagueNames.Any() ? leagueNames : null
                });

                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                return NotFound(aEx.Message);
            }
        }


        [HttpPost("create")]
        public async Task<ActionResult<string>> PostLeague(PostLeagueRequest postLeagueRequest)
        {
            try
            {
                var result = await _leagueService.AddAsync(postLeagueRequest);

                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }


        [HttpPost("update/{league}")]
        public async Task<ActionResult> UpdateTeam([FromQuery] string league, [FromBody] UpdateLeagueRequest updateLeagueRequest)
        {
            try
            {
                var result = await _leagueService.UpdateAsync((league, updateLeagueRequest));
                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }

        [HttpDelete("{league}")]
        public async Task<ActionResult> DeleteTeam(string leagueId)
        {
            try
            {
                await _leagueService.DeleteAsync(leagueId);
                return Ok();
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }
    }
}
