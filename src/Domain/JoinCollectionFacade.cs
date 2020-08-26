#region Copyright & License

// Copyright © 2020 - 2020 Emmanuel Benitez
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

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
            Requires.Argument(ownerEntity, nameof(ownerEntity))
                .IsNotNull()
                .Check();
            Requires.Argument(collection, nameof(collection))
                .IsNotNull()
                .Check();

            _ownerEntity = ownerEntity;
            _collection = collection;
        }

        #region ICollection<TJoinEntity> Members

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

        public int Count
            => _collection.Count;

        public bool IsReadOnly
            => _collection.IsReadOnly;

        [ExcludeFromCodeCoverage]
        bool ICollection<TJoinEntity>.Remove(TJoinEntity item)
        {
            return _collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TJoinEntity> GetEnumerator()
        {
            return _collection.GetEnumerator();
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
