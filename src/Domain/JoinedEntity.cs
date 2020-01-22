using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BigSolution.Infra.Domain
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
