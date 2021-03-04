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
    public class StandingsController : ControllerBase
    {
        public StandingsController(IStandingsService standingsService)
        {
            _standingsService = standingsService;
        }

        private readonly IStandingsService _standingsService;

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

        [HttpPost("initiate")]
        public async Task<ActionResult> InitiateStandings(InitiateStandingsRequest initiateStandingsRequest)
        {
            try
            {
                var result = await _standingsService.InitiateAsync(initiateStandingsRequest);

                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
        }


    }
}
