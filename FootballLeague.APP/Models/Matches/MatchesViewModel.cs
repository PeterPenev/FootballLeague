using FootballLeague.BLL.DTOs;
using System.Collections.Generic;

namespace FootballLeague.APP.Models.Matches
{
    public class MatchesViewModel
    {
        public IEnumerable<MatchDTO> Matches { get; set; } = new List<MatchDTO>();
    }
}
