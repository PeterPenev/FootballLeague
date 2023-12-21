using FootballLeague.DAL.Contracts;
using FootballLeague.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.DAL.Repositories
{
    public class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        private readonly FootballLeagueContext footballLeagueContext;

        public TeamRepository(FootballLeagueContext footballLeagueContext)
            : base(footballLeagueContext)
        {
            this.footballLeagueContext = footballLeagueContext;
        }

        public async Task<Team> GetTeamByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception($"Team name {name} is null or empty!");
            }

            var team = await this.footballLeagueContext.Teams.Where(n => n.Name == name).FirstOrDefaultAsync();

            return team;
        }
    }
}
