using FootballLeague.BLL.DTOs;
using System.Collections.Generic;

namespace FootballLeague.APP.Models.Teams
{
    public class TeamsViewModel
    {
        public IEnumerable<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
    }
}
