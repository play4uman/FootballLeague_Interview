using FootballLeague_Interview.DAL.DataServices;
using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.DAL.Entities;
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
    public class TeamsController : ControllerBase
    {
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        private readonly ITeamService _teamService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams(string league, [FromQuery] string[] teamNames)
        {
            try
            {
                var result = await _teamService.FindAsync(new FindTeamParams
                {
                    League = league,
                    TeamNames = teamNames.Any() ? teamNames : null
                });

                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                return NotFound(aEx.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> PostTeam(PostTeamRequest postTeamRequest)
        {
            try
            {
                var result = await _teamService.AddAsync(postTeamRequest);

                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }
    }
}
