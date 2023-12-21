using FootballLeague.BLL.DTOs;
using FootballLeague.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.BLL.Contracts
{
    public interface IMatchService
    {
        Task<IEnumerable<Match>> GetAllMatchesAsync();

        Task<Match> CreateMatchAsync(Match match);

        Task<Match> UpdateMatchAsync(Match match);

        Task<Match> DeleteMatchAsync(Match match);

        Task<IEnumerable<Match>> GetMatchesByTeamId(int teamId);

        Task<IEnumerable<MatchDTO>> GetAllMatchesDTOAsync();
    }
}
