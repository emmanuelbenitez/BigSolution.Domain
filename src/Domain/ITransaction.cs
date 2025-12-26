#region Copyright & License

// Copyright © 2020 - 2022 Emmanuel Benitez
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

namespace BigSolution.Domain;

/// <summary>
/// Represents a transaction that can be committed or rolled back.
/// </summary>
/// <remarks>
/// This interface provides methods for managing transactions, including synchronous and asynchronous
/// operations for committing or rolling back changes. Implementations of this interface should ensure
/// proper resource management by implementing the <see cref="IDisposable"/> interface.
/// </remarks>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface ITransaction : IDisposable
{
    /// <summary>
    /// Commits the transaction.
    /// </summary>
    /// <remarks>
    /// This method finalizes the transaction, making all changes made during the transaction permanent.
    /// Implementations should ensure that any necessary resource cleanup is performed after committing.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the transaction is in an invalid state for committing.
    /// </exception>
    void Commit();

    /// <summary>
    /// Asynchronously commits the transaction.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to cancel the commit operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation of committing the transaction.
    /// </returns>
    /// <remarks>
    /// This method finalizes the transaction, making all changes made during the transaction permanent.
    /// Implementations should ensure proper resource cleanup after committing. If the transaction is in
    /// an invalid state, an exception should be thrown.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the transaction is in an invalid state for committing.
    /// </exception>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the transaction.
    /// </summary>
    /// <remarks>
    /// This method reverts all changes made during the transaction. Implementations should ensure
    /// proper cleanup and resource management after rolling back. If the transaction is in an invalid
    /// state, an exception should be thrown.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the transaction is in an invalid state for rolling back.
    /// </exception>
    void Rollback();

    /// <summary>
    /// Asynchronously rolls back the transaction.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to cancel the rollback operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation of rolling back the transaction.
    /// </returns>
    /// <remarks>
    /// This method should be implemented to ensure that any changes made during the transaction
    /// are reverted. Implementations should handle any necessary cleanup and resource management.
    /// </remarks>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
