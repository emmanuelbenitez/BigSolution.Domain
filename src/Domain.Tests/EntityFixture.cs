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
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace BigSolution.Infra.Domain
{
    public class EntityFixture
    {
        [Theory]
        [MemberData(nameof(NotSameEntities))]
        public void EqualsFailed(FakeEntity entity, FakeEntity entityToCompare)
        {
            entity.Equals(entityToCompare).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(SameEntities))]
        public void EqualsSucceeds(FakeEntity entity, FakeEntity entityToCompare)
        {
            entity.Equals(entityToCompare).Should().BeTrue();
        }

        [Fact]
        public void GetHashCodeSucceeds()
        {
#if !HAVE_HASHCODE
             new FakeEntity(1).GetHashCode().Should().Be(1.GetHashCode())
#endif
#if HAVE_HASHCODE
            new FakeEntity(1).GetHashCode().Should().Be(HashCode.Combine(1));
#endif
        }

        [Theory]
        [MemberData(nameof(SameEntities))]
        public void OperatorNotEqualFailed(FakeEntity entity, FakeEntity entityToCompare)
        {
            (entity != entityToCompare).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(NotSameEntities))]
        public void OperatorNotEqualSucceeds(FakeEntity entity, FakeEntity entityToCompare)
        {
            (entity != entityToCompare).Should().BeTrue();
        }

        public static IEnumerable<object[]> NotSameEntities
        {
            [UsedImplicitly]
            get
            {
                yield return new object[] { new FakeEntity(), new FakeEntity() };
                yield return new object[] { new FakeEntity(0), new FakeEntity(1) };
            }
        }

        public static IEnumerable<object[]> SameEntities
        {
            [UsedImplicitly]
            get
            {
                var fakeEntity = new FakeEntity();
                yield return new object[] { fakeEntity, fakeEntity };
                yield return new object[] { new FakeEntity(1), new FakeEntity(1) };
            }
        }

        public class FakeEntity : Entity<int>
        {
            public FakeEntity() { }

            public FakeEntity(int id) : base(id) { }
        }
    }
}
