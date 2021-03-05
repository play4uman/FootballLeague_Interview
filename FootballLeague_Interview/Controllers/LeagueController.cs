﻿using FootballLeague_Interview.DAL.DataServices.Abstractions;
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

        /// <summary>
        /// Retrieves a list of League objects
        /// </summary>
        /// <param name="country">Filter Leagues based on country name</param>
        /// <param name="leagueNames">Find only Leagues whose names are contained in this list</param>
        /// <response code="200">Query is valid. The resulting collection may be empty though</response>
        /// <response code="400">Cannot find league with the given parameters</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueDTO>>> GetLeagues(string country, [FromQuery] string[] leagueNames)
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

        /// <summary>
        /// Create a new League object
        /// </summary>
        /// <param name="postLeagueRequest">Request used for creating the League object</param>
        /// <response code="200">Query is valid. The resulting collection may be empty though</response>
        /// <response code="400">Cannot find league with the given parameters</response>


        [HttpPost("create")]
        public async Task<ActionResult> PostLeague(PostLeagueRequest postLeagueRequest)
        {
            try
            {
                var result = await _leagueService.AddAsync(postLeagueRequest);

                return Created(result.url, result.createdDto);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }


        [HttpPost("update/{name}")]
        public async Task<ActionResult> UpdateTeam([FromQuery] string name, [FromBody] UpdateLeagueRequest updateLeagueRequest)
        {
            try
            {
                var result = await _leagueService.UpdateAsync((name, updateLeagueRequest));
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
