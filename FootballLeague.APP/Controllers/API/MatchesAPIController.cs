using FootballLeague.BLL.Contracts;
using FootballLeague.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FootballLeague.APP.Controllers.API
{
    [Route("api/")]
    [ApiController]
    public class MatchesAPIController : Controller
    {
        private readonly IMatchService matchService;

        public MatchesAPIController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpGet]
        [Route("GetAllMatches")]
        public async Task<IActionResult> GetAllMatches()
        {
            var matches = await this.matchService.GetAllMatchesAsync();

            return Ok(matches);
        }

        [HttpPost]
        [Route("AddMatch")]
        public async Task<IActionResult> AddMatch([FromBody] Match match)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var createdMatch = await this.matchService.CreateMatchAsync(match);

            return Ok(createdMatch);
        }

        [HttpPut]
        [Route("UpdateMatch")]
        public async Task<IActionResult> UpdateMatch([FromBody] Match match)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var updatedMatch = await this.matchService.UpdateMatchAsync(match);            

            return Ok(updatedMatch);
        }

        [HttpDelete]
        [Route("DeleteMatch")]
        public async Task<IActionResult> DeleteMatch([FromBody] Match match)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var deletedMatch = await this.matchService.DeleteMatchAsync(match);
            
            return Ok(deletedMatch);
        }
    }
}
