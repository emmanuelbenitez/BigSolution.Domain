using System.Linq;

namespace BigSolution.Infra.Domain
{
    public interface IRepository<TAggregate>
        where TAggregate : IAggregateRoot
    {
        IQueryable<TAggregate> Entities { get; }

        void Add(TAggregate entity);

        void Update(TAggregate entity);

        void Delete(TAggregate entity);
    }
}