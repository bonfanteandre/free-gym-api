using FreeGym.Core.Entities;
using FreeGym.Core.Interfaces.Repositories;
using FreeGym.Core.Interfaces.UnitOfWork;
using FreeGym.Core.Results;
using FreeGym.Core.Validators;
using System.Threading.Tasks;

namespace FreeGym.Core.Services
{
    public class MusclesService : ServiceBase<Muscle>
    {
        private readonly IMusclesRepository _musclesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MusclesService(IMusclesRepository musclesRepository, IUnitOfWork unitOfWork)
        {
            _musclesRepository = musclesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task InsertAsync(Muscle muscle)
        {
            if (!ExecuteValidation(new MuscleValidator(), muscle))
            {
                return;
            }

            await _musclesRepository.InsertAsync(muscle);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(int id, Muscle muscle)
        {
            var muscleToUpdate = await _musclesRepository.FindAsync(id);

            if (muscle == null)
            {
                AddError("Músuclo não encontrado");
                return;
            }

            muscleToUpdate.Name = muscle.Name;

            if (!ExecuteValidation(new MuscleValidator(), muscle))
            {
                return;
            }

            _musclesRepository.Update(muscleToUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task Remove(int id)
        {
            var muscle = await _musclesRepository.FindAsync(id);

            if (muscle == null)
            {
                AddError("Músuclo não encontrado");
                return;
            }

            _musclesRepository.Remove(muscle);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task<Muscle> FindAsync(int id)
        {
            return await _musclesRepository.FindAsync(id);
        }
        
        public async Task<PagedResult<Muscle>> GetPagedAsync(int skip, int take, string search)
        {
            return await _musclesRepository.GetPagedAsync(skip, take, search);
        }
    }
}
