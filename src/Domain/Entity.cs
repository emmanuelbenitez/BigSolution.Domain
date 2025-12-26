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

using JetBrains.Annotations;

namespace BigSolution.Domain;

public abstract class Entity<TId>(TId id) : IEntity
{
    #region Operators

    /// <summary>
    /// Determines whether two <see cref="Entity{TId}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Entity{TId}"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Entity{TId}"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="Entity{TId}"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Two <see cref="Entity{TId}"/> instances are considered equal if they have the same identifier and neither of them is new.
    /// </remarks>
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="Entity{TId}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Entity{TId}"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Entity{TId}"/> instance to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two <see cref="Entity{TId}"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class with default values.
    /// </summary>
    protected Entity()
        : this(default!) { }

    #region Base Class Member Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Entity{TId}"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="Entity{TId}"/> instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified object is equal to the current <see cref="Entity{TId}"/> instance; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Two <see cref="Entity{TId}"/> instances are considered equal if they have the same identifier and neither of them is new.
    /// </remarks>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) ||
            obj is Entity<TId> entity && !(entity.IsNew && IsNew) &&
            EqualityComparer<TId>.Default.Equals(Id, entity.Id);
    }

    /// <summary>
    /// Serves as the default hash function for the <see cref="Entity{TId}"/> type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="Entity{TId}"/> instance.
    /// </returns>
    /// <remarks>
    /// The hash code is computed based on the identifier of the entity.
    /// </remarks>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    #endregion

    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    /// <remarks>
    /// The <see cref="Id"/> property is used to uniquely identify an instance of the entity.
    /// It is initialized during the construction of the entity and cannot be modified afterwards.
    /// </remarks>
    [UsedImplicitly]
    public TId Id { get; } = id;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Entity{TId}"/> instance is new.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the current <see cref="Entity{TId}"/> instance is new; otherwise, <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// An entity is considered new if its identifier is equal to the default value of the type <typeparamref name="TId"/>.
    /// </remarks>
    private bool IsNew => Equals(Id, default(TId));
}
