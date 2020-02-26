using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BigSolution.Infra.Domain
{
    public class JoinCollectionFacade<TEntity, TOwnerEntity, TJoinEntity>
        : ICollection<TJoinEntity>
        where TJoinEntity : class, IJoinEntity<TEntity>, IJoinEntity<TOwnerEntity>, new()
        where TEntity : class
        where TOwnerEntity : class, IEntity
    {
        private static bool Equals(TEntity item, TJoinEntity e)
        {
            return Equals(((IJoinEntity<TEntity>) e).Navigation, item);
        }

        public JoinCollectionFacade(
            TOwnerEntity ownerEntity,
            ICollection<TJoinEntity> collection)
        {
            Requires.NotNull(ownerEntity, nameof(ownerEntity));
            Requires.NotNull(collection, nameof(collection));

            _ownerEntity = ownerEntity;
            _collection = collection;
        }

        #region ICollection<TJoinEntity> Members

        public IEnumerator<TJoinEntity> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [ExcludeFromCodeCoverage]
        bool ICollection<TJoinEntity>.Remove(TJoinEntity item)
        {
            return _collection.Remove(item);
        }

        public int Count
            => _collection.Count;

        public bool IsReadOnly
            => _collection.IsReadOnly;

        [ExcludeFromCodeCoverage]
        void ICollection<TJoinEntity>.Add(TJoinEntity item)
        {
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        [ExcludeFromCodeCoverage]
        bool ICollection<TJoinEntity>.Contains(TJoinEntity item)
        {
            return _collection.Contains(item);
        }

        [ExcludeFromCodeCoverage]
        void ICollection<TJoinEntity>.CopyTo(TJoinEntity[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        #endregion

        public IEnumerable<TEntity> Entities => _collection.Select(e => ((IJoinEntity<TEntity>) e).Navigation);

        public void Add(TEntity item)
        {
            var entity = new TJoinEntity();
            ((IJoinEntity<TEntity>) entity).Navigation = item;
            ((IJoinEntity<TOwnerEntity>) entity).Navigation = _ownerEntity;
            _collection.Add(entity);
        }

        public bool Contains(TEntity item)
        {
            return _collection.Any(e => Equals(item, e));
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            _collection.Select(entity => ((IJoinEntity<TEntity>) entity).Navigation).ToList()
                .CopyTo(array, arrayIndex);
        }

        public bool Remove(TEntity item)
        {
            return _collection.Remove(
                _collection.FirstOrDefault(e => Equals(item, e)));
        }

        private readonly ICollection<TJoinEntity> _collection;
        private readonly TOwnerEntity _ownerEntity;
    }
}
