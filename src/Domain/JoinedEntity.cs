#region Copyright & License

// Copyright © 2020 - 2021 Emmanuel Benitez
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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BigSolution.Domain
{
    public abstract class JoinedEntity<TOwnerEntity, TId, TJoinedEntity, TJoinEntity> : Entity<TId>
        where TJoinEntity : class, IJoinEntity<TJoinedEntity>, IJoinEntity<TOwnerEntity>, new()
        where TJoinedEntity : class, IEntity
        where TOwnerEntity : JoinedEntity<TOwnerEntity, TId, TJoinedEntity, TJoinEntity>
    {
        protected JoinedEntity()
            : this(default) { }

        protected JoinedEntity(TId id)
            : base(id)
        {
            _collectionFacadeLazy = new Lazy<JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity>>(CreateCollectionFacade);
        }

        protected JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity> CollectionFacade => _collectionFacadeLazy.Value;

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        protected TOwnerEntity OwnerEntity => (TOwnerEntity) this;

        private JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity> CreateCollectionFacade()
        {
            return new JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity>(OwnerEntity, _joinEntities);
        }

        private readonly Lazy<JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity>> _collectionFacadeLazy;
        private readonly List<TJoinEntity> _joinEntities = new List<TJoinEntity>();
    }
}
