using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.Shared.DTO.Request;
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

        [HttpPost("create")]
        public async Task<ActionResult<string>> PostTeam(PostLeagueRequest postLeagueRequest)
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
    }
}
