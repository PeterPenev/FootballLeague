using FootballLeague.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.DAL.Contracts
{
    public interface IMatchRepository : IRepositoryBase<Match>
    {
        Task <IEnumerable<Match>> GetAllMatchesByTeamIdAsync(int id);

        Task<Match> GetMatchByTeamId1AndTeamId2Async(int id1, int id2);
    }
}
