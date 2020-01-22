using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace BigSolution.Infra.Domain
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [UsedImplicitly]
    public interface IRepository<TAggregate>
        where TAggregate : IAggregateRoot
    {
        IQueryable<TAggregate> Entities { get; }

        void Add(TAggregate entity);

        void Update(TAggregate entity);

        void Delete(TAggregate entity);
    }
}
