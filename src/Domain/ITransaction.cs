using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BigSolution.Infra.Domain
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();

        Task CommitAsync();

        Task RollbackAsync();
    }
}
