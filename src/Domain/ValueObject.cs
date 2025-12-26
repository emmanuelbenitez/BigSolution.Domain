#region Copyright & License

// Copyright © 2020 - 2025 Emmanuel Benitez
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

namespace BigSolution.Domain;

/// <summary>
/// Represents a base class for value objects, providing equality comparison based on the values of its attributes.
/// </summary>
/// <typeparam name="T">
/// The type of the value object that inherits from this class. This ensures that equality checks are type-safe and specific to the derived type.
/// </typeparam>
/// <remarks>
/// Value objects are immutable and are compared based on their attribute values rather than their references.
/// This class provides a consistent implementation of equality operators and methods for derived value objects.
/// </remarks>
public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    #region Operators

    /// <summary>
    /// Determines whether two specified <see cref="ValueObject{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">
    /// The first <see cref="ValueObject{T}"/> to compare, or <c>null</c>.
    /// </param>
    /// <param name="right">
    /// The second <see cref="ValueObject{T}"/> to compare, or <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the two <see cref="ValueObject{T}"/> instances are equal; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This operator uses the <see cref="object.Equals(object?)"/> method to determine equality.
    /// </remarks>
    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two specified <see cref="ValueObject{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">
    /// The first <see cref="ValueObject{T}"/> to compare, or <c>null</c>.
    /// </param>
    /// <param name="right">
    /// The second <see cref="ValueObject{T}"/> to compare, or <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the two <see cref="ValueObject{T}"/> instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This operator uses the negation of the equality operator (<see cref="op_Equality"/>) to determine inequality.
    /// </remarks>
    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
    {
        return !(left == right);
    }

    #endregion

    #region IEquatable<T> Members

    /// <summary>
    /// Determines whether the current <see cref="ValueObject{T}"/> is equal to another object of the same type.
    /// </summary>
    /// <param name="other">
    /// The other <see cref="ValueObject{T}"/> to compare with the current object.
    /// </param>
    /// <returns>
    /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method compares the attributes of the current object and the <paramref name="other"/> object
    /// using the sequence of attributes returned by <see cref="GetAttributesToIncludeInEqualityCheck"/>.
    /// </remarks>
    public bool Equals(T? other)
    {
        if (other == null) return false;
        return GetAttributesToIncludeInEqualityCheck()
            .SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());
    }

    #endregion

    #region Base Class Member Overrides

    /// <summary>
    /// Determines whether the current <see cref="ValueObject{T}"/> is equal to a specified object.
    /// </summary>
    /// <param name="other">
    /// The object to compare with the current <see cref="ValueObject{T}"/>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the specified object is equal to the current <see cref="ValueObject{T}"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method first checks for reference equality. If the objects are not the same reference, it verifies that the
    /// specified object is of the same type as the current object and then delegates the equality check to the
    /// <see cref="Equals(T?)"/> method.
    /// </remarks>
    public override bool Equals(object? other)
    {
        return ReferenceEquals(this, other) || other?.GetType() == GetType() && Equals((T?)other);
    }

    /// <summary>
    /// Serves as the default hash function for the <see cref="ValueObject{T}"/> class.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="ValueObject{T}"/> instance, which is computed based on the attributes
    /// included in the equality check.
    /// </returns>
    /// <remarks>
    /// The hash code is calculated by aggregating the hash codes of the attributes returned by
    /// <see cref="GetAttributesToIncludeInEqualityCheck"/>. This ensures consistency between the equality
    /// and hash code implementations.
    /// </remarks>
    public override int GetHashCode()
    {
        return GetAttributesToIncludeInEqualityCheck().Aggregate(17, (hashCode, attribute) => hashCode * 31 + attribute.GetHashCode());
    }

    #endregion

    /// <summary>
    /// Retrieves the sequence of attributes that should be included in the equality check for the current <see cref="ValueObject{T}"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of objects representing the attributes to be used in equality comparisons.
    /// </returns>
    /// <remarks>
    /// Derived classes must override this method to specify which attributes should be considered when determining equality.
    /// The returned sequence is used in both <see cref="Equals(object?)"/> and <see cref="GetHashCode"/> implementations.
    /// </remarks>
    protected abstract IEnumerable<object> GetAttributesToIncludeInEqualityCheck();
}
