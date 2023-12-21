using FootballLeague.APP.Models.Teams;
using FootballLeague.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FootballLeague.APP.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService teamService;

        public TeamsController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new TeamsViewModel();

            viewModel.Teams = await this.teamService.GetAllTeamsDTOAsync();

            return View(viewModel);
        }        
    }
}
