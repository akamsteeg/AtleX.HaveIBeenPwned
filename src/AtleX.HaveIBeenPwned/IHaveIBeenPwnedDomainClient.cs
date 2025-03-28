// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a client for the domain search functionality of the <see
/// href="https://haveibeenpwned.com/">HaveIBeenPwned</see> service
/// </summary>
/// <remarks>
/// Some operations require an API key
/// </remarks>
public interface IHaveIBeenPwnedDomainClient
{
  /// <summary>
  /// Get all the breached users in a domain with the breaches they appeared in
  /// </summary>
  /// <remarks>
  /// This operation requires an API key
  /// </remarks>
  /// <param name="domain">
  /// The domain to get the breached users for
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// breached <see cref="DomainUser"/> of the domain
  /// </returns>
  Task<IEnumerable<DomainUser>> GetBreachedDomainUsersAsync(string domain);

  /// <summary>
  /// Get all the breached users in a domain with the breaches they appeared in
  /// </summary>
  /// <remarks>
  /// This operation requires an API key
  /// </remarks>
  /// <param name="domain">
  /// The domain to get the breached users for
  /// </param>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// breached <see cref="DomainUser"/> of the domain
  /// </returns>
  Task<IEnumerable<DomainUser>> GetBreachedDomainUsersAsync(string domain, CancellationToken cancellationToken);

  /// <summary>
  /// Gets the domains registered to an API key
  /// </summary>
  /// <remarks>
  /// This operation requires an API key
  /// </remarks>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of <see
  /// cref="SubscribedDomain"/> registered to the API key
  /// </returns>
  Task<IEnumerable<SubscribedDomain>> GetSubscribedDomainsAsync();

  /// <summary>
  /// Gets the domains registered to an API key
  /// </summary>
  /// <remarks>
  /// This operation requires an API key
  /// </remarks>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of <see
  /// cref="SubscribedDomain"/> registered to the API key
  /// </returns>
  Task<IEnumerable<SubscribedDomain>> GetSubscribedDomainsAsync(CancellationToken cancellationToken);
}
