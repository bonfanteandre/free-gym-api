using FreeGym.Core.Entities;
using FreeGym.Core.Interfaces.Repositories;
using FreeGym.Core.Results;
using FreeGym.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeGym.Data.Repositories
{
    public class ExercisesRepository : IExercisesRepository
    {
        private readonly FreeGymContext _context;

        public ExercisesRepository(FreeGymContext context)
        {
            _context = context;
        }

        public Task<Exercise> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Exercise>> GetPagedAsync(int skip, int take, string search)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public void Remove(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public void Update(Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}
