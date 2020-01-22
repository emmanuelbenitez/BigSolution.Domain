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
