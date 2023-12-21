using FootballLeague.APP.Models.Matches;
using FootballLeague.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FootballLeague.APP.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchService matchService;

        public MatchesController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new MatchesViewModel();

            viewModel.Matches = await this.matchService.GetAllMatchesDTOAsync();

            return View(viewModel);
        }
    }
}
