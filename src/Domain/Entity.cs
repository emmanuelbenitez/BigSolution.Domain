using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BigSolution.Infra.Domain
{
    public abstract class Entity<TId> : IEntity
    {
        protected Entity()
            : this(default)
        {
        }

        protected Entity(TId id)
        {
            Id = id;
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public TId Id { get; private set; }

        private bool IsNew => Equals(Id, default(TId));

        #region Operators

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        #endregion

        #region Base Class Member Overrides

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) ||
                   obj is Entity<TId> entity && !(entity.IsNew && IsNew) &&
                   EqualityComparer<TId>.Default.Equals(Id, entity.Id);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
#if !HAVE_HASHCODE
            return Id.GetHashCode();
#endif
#if HAVE_HASHCODE
            return HashCode.Combine(Id);
#endif
        }

        #endregion
    }
}