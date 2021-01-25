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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#if HAVE_HASHCODE
using System;
#endif

namespace BigSolution.Domain
{
    public abstract class Entity<TId> : IEntity
    {
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

        protected Entity()
            : this(default) { }

        protected Entity(TId id)
        {
            Id = id;
        }

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

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public TId Id { get; private set; }

        private bool IsNew => Equals(Id, default(TId));
    }
}
