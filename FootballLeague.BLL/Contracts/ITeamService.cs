using FootballLeague.BLL.DTOs;
using FootballLeague.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.BLL.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDTO>> GetAllTeamsDTOAsync();

        Task<Team> CreateTeamAsync(string name);

        Task<Team> UpdateTeamAsync(Team team);

        Task<Team> DeleteTeamAsync(Team team);  
    }
}
