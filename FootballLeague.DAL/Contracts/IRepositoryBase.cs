using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.DAL.Contracts
{
    public interface IRepositoryBase<TModel> 
        where TModel : class
    {
        Task<TModel> GetByIdAsync(int id);

        Task<IEnumerable<TModel>> GetAllAsync();

        Task AddAsync(TModel entity);
        Task Update(TModel entity);
        Task Remove(TModel entity);
    }
}
