using FreeGym.Core.Interfaces.UnitOfWork;
using FreeGym.Data.Context;
using System.Threading.Tasks;

namespace FreeGym.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FreeGymContext _context;

        public UnitOfWork(FreeGymContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
