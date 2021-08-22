using FreeGym.Core.Entities;
using FreeGym.Core.Interfaces.Repositories;
using FreeGym.Core.Results;
using FreeGym.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FreeGym.Data.Repositories
{
    public class MusclesRepository : IMusclesRepository
    {
        private readonly FreeGymContext _context;

        public MusclesRepository(FreeGymContext context)
        {
            _context = context;
        }

        public async Task<Muscle> FindAsync(int id)
        {
            return await _context.Muscles.FindAsync(id);
        }

        public async Task<PagedResult<Muscle>> GetPagedAsync(int skip, int take, string search)
        {
            var musclesQuery = _context.Muscles.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                musclesQuery = musclesQuery.Where(m => m.Name.ToLower().Contains(search.ToLower()));
            }

            var pagedMuscles = await musclesQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = musclesQuery.Count();

            return new PagedResult<Muscle>(pagedMuscles, total);
        }

        public async Task InsertAsync(Muscle muscle)
        {
            await _context.Muscles.AddAsync(muscle);
        }

        public void Remove(Muscle muscle)
        {
            _context.Muscles.Remove(muscle);
        }

        public void Update(Muscle muscle)
        {
            _context.Muscles.Update(muscle);
        }
    }
}
