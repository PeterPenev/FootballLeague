using FootballLeague.DAL.Models;
using System.Threading.Tasks;

namespace FootballLeague.DAL.Contracts
{
    public interface ITeamRepository : IRepositoryBase<Team>
    {
        Task<Team> GetTeamByNameAsync(string name);
    }
}
