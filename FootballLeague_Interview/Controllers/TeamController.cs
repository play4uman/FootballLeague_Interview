using FootballLeague_Interview.DAL.DataServices;
using FootballLeague_Interview.DAL.DataServices.Abstractions;
using FootballLeague_Interview.DAL.DataServices.FindParameters;
using FootballLeague_Interview.DAL.Entities;
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
    public class TeamController : ControllerBase
    {
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        private readonly ITeamService _teamService;

        /// <summary>
        /// Retrieves a list of Team objects
        /// </summary>
        /// <param name="league">Filter Teams based on League name</param>
        /// <param name="teamNames">Find only Teams whose names are contained in this list</param>
        /// <response code="200">Query is valid. The resulting collection may be empty though</response>
        /// <response code="400">Cannot find Teams with the given parameters</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams(string league, [FromQuery]string[] teamNames)
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

        /// <summary>
        /// Create a new Team object
        /// </summary>
        /// <param name="postTeamRequest">Request used for creating the League object</param>
        /// <response code="201">Team has been created</response>
        /// <response code="400">Cannot create Team from the given request</response>

        [HttpPost("create")]
        public async Task<ActionResult> PostTeam(PostTeamRequest postTeamRequest)
        {
            try
            {
                var result = await _teamService.AddAsync(postTeamRequest);

                return Created(result.url, result.createdDto);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }

        // It doesn't really make sense to update a team since all of it's fields are part of its PK. If a mistake has been made, delete 
        // the dirty Team using the DELETE endpoint and insert a new one.  


        /// <summary>
        /// Deletes an existing Team object
        /// </summary>
        /// <param name="teamName">The name of the Team object to delete</param>
        /// <param name="leagueName">The name of the League in which the Team to delete plays in</param>
        /// <response code="200">The Team has been deleted</response>
        /// <response code="400">Cannot delete Team with the given parameters</response>

        [HttpDelete]
        public async Task<ActionResult> DeleteTeam(
            [Required] string teamName,
            [Required] string leagueName)
        {
            try
            {
                await _teamService.DeleteAsync((teamName, leagueName));
                return Ok();
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }
    }
}
