using FootballLeague.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.DAL.Repositories
{
    public class RepositoryBase<TModel> : IRepositoryBase<TModel>
        where TModel : class
    {
        private readonly FootballLeagueContext footballLeagueContext;

        public RepositoryBase(FootballLeagueContext footballLeagueContext)
        {
            this.footballLeagueContext = footballLeagueContext;
        }

        public async Task AddAsync(TModel entity)
        {
            this.footballLeagueContext.ChangeTracker.Clear();

            await this.footballLeagueContext.AddAsync(entity);

            await this.footballLeagueContext.SaveChangesAsync();
        }

        public async Task Remove(TModel entity)
        {
            this.footballLeagueContext.ChangeTracker.Clear();

            this.footballLeagueContext.Remove(entity);

            await this.footballLeagueContext.SaveChangesAsync();
        }        

        public async Task Update(TModel entity)
        {
            this.footballLeagueContext.ChangeTracker.Clear();

            this.footballLeagueContext.Update(entity);

            await this.footballLeagueContext.SaveChangesAsync();
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            return await this.footballLeagueContext.Set<TModel>().FindAsync(id);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await this.footballLeagueContext.Set<TModel>().ToListAsync();
        }
    }
}
