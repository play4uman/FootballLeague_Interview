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
    public class ResultController : ControllerBase
    {
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        private readonly IResultService _resultService;

        [HttpPost("add")]
        public async Task<ActionResult> PostResult(PostResultRequest postResultRequest)
        {
            {
                try
                {
                    var result = await _resultService.AddAsync(postResultRequest);

                    return Ok(result);
                }
                catch (ArgumentException aEx)
                {
                    return BadRequest(aEx.Message);
                }
            }
        }
    }
}
