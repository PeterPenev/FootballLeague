using FootballLeague.DAL.Contracts;
using FootballLeague.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.DAL.Repositories
{
    public class MatchRepository : RepositoryBase<Match>, IMatchRepository
    {
        private readonly FootballLeagueContext footballLeagueContext;

        public MatchRepository(FootballLeagueContext footballLeagueContext)
            : base(footballLeagueContext)
        {
            this.footballLeagueContext = footballLeagueContext;
        }

        public async Task<IEnumerable<Match>> GetAllMatchesByTeamIdAsync(int id)
        {
            var matches = await this.footballLeagueContext.Matches
                                                    .Where(m => m.Team1Id == id || m.Team2Id == id)
                                                    .ToListAsync();

            return matches;
        }

        public async Task<Match> GetMatchByTeamId1AndTeamId2Async(int id1, int id2)
        {
            var match = await this.footballLeagueContext.Matches
                                                        .Include(t=>t.Team1)
                                                        .Include(t=>t.Team2)
                                                        .Where(m => m.Team1Id == id1 && m.Team2Id == id2)
                                                        .FirstOrDefaultAsync();

            return match;
        }
    }
}
