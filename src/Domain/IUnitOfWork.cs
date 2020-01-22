using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BigSolution.Infra.Domain
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IUnitOfWork
    {
        void Save();

        ITransaction BeginTransaction();

        Task SaveAsync();
    }
}
