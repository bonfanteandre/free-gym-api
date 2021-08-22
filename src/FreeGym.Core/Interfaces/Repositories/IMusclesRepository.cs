using FreeGym.Core.Entities;
using FreeGym.Core.Results;
using System.Threading.Tasks;

namespace FreeGym.Core.Interfaces.Repositories
{
    public interface IMusclesRepository
    {
        Task InsertAsync(Muscle muscle);
        void Update(Muscle muscle);
        void Remove(Muscle muscle);
        Task<Muscle> FindAsync(int id);
        Task<PagedResult<Muscle>> GetPagedAsync(int skip, int take, string search);
    }
}
