using FootballLeague.BLL.Contracts;
using FootballLeague.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FootballLeague.APP.Controllers.API
{
    [Route("api/")]
    [ApiController]
    public class TeamsAPIController : Controller
    {
        private readonly ITeamService teamService;

        public TeamsAPIController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet]
        [Route("GetAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await this.teamService.GetAllTeamsDTOAsync();

            return Ok(teams);
        }

        [HttpPost]
        [Route("AddTeam")]
        public async Task<IActionResult> AddTeam([FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var createdTeam = await this.teamService.CreateTeamAsync(team.Name);

            if (createdTeam == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return Ok(createdTeam);
        }

        [HttpPut]
        [Route("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam([FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var updatedTeam = await this.teamService.UpdateTeamAsync(team);

            return Ok(updatedTeam);
        }

        [HttpDelete]
        [Route("DeleteTeam")]
        public async Task<IActionResult> DeleteTeam([FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var deletedTeam = await this.teamService.DeleteTeamAsync(team);

            return Ok(deletedTeam);
        }
    }
}
