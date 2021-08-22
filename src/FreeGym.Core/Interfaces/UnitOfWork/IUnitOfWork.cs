using System.Threading.Tasks;

namespace FreeGym.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
