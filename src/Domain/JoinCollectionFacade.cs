using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BigSolution.Infra.Domain
{
    public class JoinCollectionFacade<TEntity, TOwnerEntity, TJoinEntity>
        : ICollection<TEntity>
        where TJoinEntity : class, IJoinEntity<TEntity>, IJoinEntity<TOwnerEntity>, new()
        //where TEntity : class//, IEntity
        where TOwnerEntity : class, IEntity
    {
        private readonly ICollection<TJoinEntity> _collection;
        private readonly TOwnerEntity _ownerEntity;

        public JoinCollectionFacade(
            TOwnerEntity ownerEntity,
            ICollection<TJoinEntity> collection)
        {
            _ownerEntity = ownerEntity;
            _collection = collection;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _collection.Select(e => ((IJoinEntity<TEntity>) e).Navigation).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TEntity item)
        {
            var entity = new TJoinEntity();
            ((IJoinEntity<TEntity>) entity).Navigation = item;
            ((IJoinEntity<TOwnerEntity>) entity).Navigation = _ownerEntity;
            _collection.Add(entity);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(TEntity item)
        {
            return _collection.Any(e => Equals(item, e));
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            this.ToList().CopyTo(array, arrayIndex);
        }

        public bool Remove(TEntity item)
        {
            return _collection.Remove(
                _collection.FirstOrDefault(e => Equals(item, e)));
        }

        public int Count
            => _collection.Count;

        public bool IsReadOnly
            => _collection.IsReadOnly;

        private static bool Equals(TEntity item, TJoinEntity e)
        {
            return Equals(((IJoinEntity<TEntity>) e).Navigation, item);
        }
    }
}