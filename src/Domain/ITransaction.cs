using System;
using System.Threading.Tasks;

namespace BigSolution.Infra.Domain
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();

        Task CommitAsync();

        Task RollbackAsync();
    }
}