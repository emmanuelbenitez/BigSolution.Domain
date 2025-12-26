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

/// <summary>
/// Represents a unit of work that encapsulates a set of operations to be performed as a single transaction.
/// </summary>
/// <remarks>
/// This interface provides methods to manage transactions and persist changes to the underlying data store.
/// </remarks>
[UsedImplicitly]
public interface IUnitOfWork
{
    /// <summary>
    /// Begins a new transaction for the current unit of work.
    /// </summary>
    /// <remarks>
    /// This method creates and returns an instance of <see cref="ITransaction"/>, which can be used to 
    /// commit or roll back the changes made within the scope of the transaction. Ensure proper disposal 
    /// of the returned <see cref="ITransaction"/> instance to release resources.
    /// </remarks>
    /// <returns>
    /// An instance of <see cref="ITransaction"/> representing the newly started transaction.
    /// </returns>
    ITransaction BeginTransaction();

    /// <summary>
    /// Persists all changes made within the current unit of work to the underlying data store.
    /// </summary>
    /// <remarks>
    /// This method ensures that all changes tracked by the unit of work are saved to the data store. 
    /// Use this method to commit changes in a synchronous manner.
    /// </remarks>
    void Save();

    /// <summary>
    /// Asynchronously persists all changes made within the current unit of work to the underlying data store.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to cancel the save operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous save operation.
    /// </returns>
    /// <remarks>
    /// This method ensures that all changes tracked by the unit of work are saved to the data store. 
    /// Use this method when performing asynchronous operations to avoid blocking the calling thread.
    /// </remarks>
    Task SaveAsync(CancellationToken cancellationToken = default);
}
