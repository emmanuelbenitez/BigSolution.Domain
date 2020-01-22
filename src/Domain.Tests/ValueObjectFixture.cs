using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace BigSolution.Infra.Domain
{
    public class ValueObjectFixture
    {
        [Theory]
        [MemberData(nameof(NotSameValueObject))]
        public void EqualsFailed(FakeValueObject valueObject, FakeValueObject valueObjectToCompare)
        {
            valueObject.Equals(valueObjectToCompare).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(NotSameValueObjectAsObject))]
        public void EqualsObjectFailed(FakeValueObject valueObject, object objectToCompare)
        {
            valueObject.Equals(objectToCompare).Should().BeFalse();
        }

        [Fact]
        public void EqualsObjectSucceeds()
        {
            new FakeValueObject(0, string.Empty).Equals((object) new FakeValueObject(0, string.Empty)).Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(SameValueObject))]
        public void EqualsSucceeds(FakeValueObject valueObject, FakeValueObject valueObjectToCompare)
        {
            valueObject.Equals(valueObjectToCompare).Should().BeTrue();
        }

        [Fact]
        public void GetHashCodeSucceeds()
        {
            new FakeValueObject(10, null).GetHashCode().Should().BeGreaterOrEqualTo(0);
        }

        [Theory]
        [MemberData(nameof(NotSameValueObject))]
        public void OperatorEqualFailed(FakeValueObject valueObject, FakeValueObject valueObjectToCompare)
        {
            (valueObject == valueObjectToCompare).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(SameValueObject))]
        public void OperatorEqualSucceeds(FakeValueObject valueObject, FakeValueObject valueObjectToCompare)
        {
            (valueObject == valueObjectToCompare).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(SameValueObject))]
        public void OperatorNotEqualFailed(FakeValueObject valueObject, FakeValueObject valueObjectToCompare)
        {
            (valueObject != valueObjectToCompare).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(NotSameValueObject))]
        public void OperatorNotEqualSucceeds(FakeValueObject valueObject, FakeValueObject valueObjectToCompare)
        {
            (valueObject != valueObjectToCompare).Should().BeTrue();
        }

        public static IEnumerable<object[]> SameValueObject
        {
            [UsedImplicitly]
            get
            {
                var valueObject = new FakeValueObject(0, string.Empty);
                yield return new object[] { valueObject, valueObject };
                yield return new object[] { valueObject, new FakeValueObject(0, string.Empty) };
            }
        }

        public static IEnumerable<object[]> NotSameValueObjectAsObject
        {
            [UsedImplicitly]
            get
            {
                yield return new object[] { new FakeValueObject(0, string.Empty), null };
                yield return new object[] { new FakeValueObject(0, string.Empty), string.Empty };
                foreach (var testCase in NotSameValueObject) yield return testCase;
            }
        }

        public static IEnumerable<object[]> NotSameValueObject
        {
            get
            {
                yield return new object[] { new FakeValueObject(0, string.Empty), null };
                yield return new object[] { new FakeValueObject(0, string.Empty), new FakeValueObject(1, string.Empty) };
                yield return new object[] { new FakeValueObject(0, string.Empty), new FakeValueObject(0, "a") };
                yield return new object[] { new FakeValueObject(0, string.Empty), new FakeValueObject(1, "a") };
            }
        }

        public sealed class FakeValueObject : ValueObject<FakeValueObject>
        {
            public FakeValueObject(int intValue, string stringValue)
            {
                IntValue = intValue;
                StringValue = stringValue;
            }

            #region Base Class Member Overrides

            protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
            {
                yield return IntValue;
                yield return StringValue;
            }

            #endregion

            [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
            public int IntValue { get; }

            [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
            public string StringValue { get; }
        }
    }
}
