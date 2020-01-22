using System;
using System.Collections.Generic;
using System.Linq;

namespace BigSolution.Infra.Domain
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        #region Operators

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }

        #endregion

        #region IEquatable<T> Members

        public bool Equals(T other)
        {
            if (other == null) return false;

            return GetAttributesToIncludeInEqualityCheck()
                .SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());
        }

        #endregion

        #region Base Class Member Overrides

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public override int GetHashCode()
        {
            return GetAttributesToIncludeInEqualityCheck().Aggregate(17, (current, obj) => current * 31 + (obj == null ? 0 : obj.GetHashCode()));
        }

        #endregion

        protected abstract IEnumerable<object> GetAttributesToIncludeInEqualityCheck();
    }
}
