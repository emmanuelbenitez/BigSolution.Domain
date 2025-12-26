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

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace BigSolution.Domain;

/// <summary>
/// Defines a repository for managing aggregate roots of type <typeparamref name="TAggregate"/>.
/// </summary>
/// <typeparam name="TAggregate">
/// The type of the aggregate root managed by the repository. Must implement <see cref="IAggregateRoot"/>.
/// </typeparam>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[UsedImplicitly]
public interface IRepository<TAggregate>
    where TAggregate : IAggregateRoot
{
    /// <summary>
    /// Gets the queryable collection of aggregate root entities managed by the repository.
    /// </summary>
    /// <value>
    /// A queryable collection of <typeparamref name="TAggregate"/> entities.
    /// </value>
    IQueryable<TAggregate> Entities { get; }

    /// <summary>
    /// Adds the specified aggregate root entity to the repository.
    /// </summary>
    /// <param name="entity">
    /// The aggregate root entity to add. Must not be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="entity"/> is <c>null</c>.
    /// </exception>
    void Add(TAggregate entity);

    /// <summary>
    /// Removes the specified aggregate root entity from the repository.
    /// </summary>
    /// <param name="entity">
    /// The aggregate root entity to remove. Must not be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="entity"/> is <c>null</c>.
    /// </exception>
    void Delete(TAggregate entity);

    /// <summary>
    /// Updates the specified aggregate root entity in the repository.
    /// </summary>
    /// <param name="entity">
    /// The aggregate root entity to update. Must not be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="entity"/> is <c>null</c>.
    /// </exception>
    void Update(TAggregate entity);
}
