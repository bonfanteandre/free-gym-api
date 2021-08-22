using FreeGym.Core.Entities;
using FreeGym.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeGym.Core.Interfaces.Repositories
{
    public interface IExercisesRepository
    {
        Task InsertAsync(Exercise exercise);
        void Update(Exercise exercise);
        void Remove(Exercise exercise);
        Task<Exercise> FindAsync(int id);
        Task<PagedResult<Exercise>> GetPagedAsync(int skip, int take, string search);
    }
}
