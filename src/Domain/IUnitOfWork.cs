using System.Threading.Tasks;

namespace BigSolution.Infra.Domain
{
    public interface IUnitOfWork
    {
        void Save();

        ITransaction BeginTransaction();

        Task SaveAsync();
    }
}