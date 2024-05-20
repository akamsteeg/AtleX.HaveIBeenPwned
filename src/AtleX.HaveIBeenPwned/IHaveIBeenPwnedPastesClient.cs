// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a client for the pastes functionality of the <see
/// href="https://haveibeenpwned.com/">HaveIBeenPwned</see> service
/// </summary>
/// <remarks>
/// Some operations require an API key
/// </remarks>
public interface IHaveIBeenPwnedPastesClient
{
  /// <summary>
  /// Get the pastes for an email address
  /// </summary>
  /// <remarks>
  /// This operation requires an API key
  /// </remarks>
  /// <param name="emailAddress">
  /// The email address to get the pastes for
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="Paste"/> the email address was found in
  /// </returns>
  Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress);

  /// <summary>
  /// Get the pastes for an email address
  /// </summary>
  /// <remarks>
  /// This operation requires an API key
  /// </remarks>
  /// <param name="emailAddress">
  /// The email address to get the pastes for
  /// </param>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="Paste"/> the email address was found in
  /// </returns>
  Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress, CancellationToken cancellationToken);
}
