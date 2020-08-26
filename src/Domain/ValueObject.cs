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
