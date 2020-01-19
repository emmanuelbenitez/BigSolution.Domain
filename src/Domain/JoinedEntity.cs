using System;
using System.Collections.Generic;

namespace BigSolution.Infra.Domain
{
    public abstract class JoinedEntity<TOwnerEntity, TId, TJoinedEntity, TJoinEntity> : Entity<TId>
        where TJoinEntity : class, IJoinEntity<TJoinedEntity>, IJoinEntity<TOwnerEntity>, new()
        where TJoinedEntity : class, IEntity
        where TOwnerEntity : JoinedEntity<TOwnerEntity, TId, TJoinedEntity, TJoinEntity>
    {
        private readonly Lazy<JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity>> _collectionFacadeLazy;
        private readonly List<TJoinEntity> _joinEntities = new List<TJoinEntity>();

        protected JoinedEntity()
            : this(default)
        {
        }

        protected JoinedEntity(TId id)
            : base(id)
        {
            _collectionFacadeLazy = new Lazy<JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity>>(CreateCollectionFacade);
        }

        protected abstract TOwnerEntity OwnerEntity { get; }

        protected JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity> CollectionFacade => _collectionFacadeLazy.Value;

        private JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity> CreateCollectionFacade()
        {
            return new JoinCollectionFacade<TJoinedEntity, TOwnerEntity, TJoinEntity>(OwnerEntity, _joinEntities);
        }
    }
}