using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a client for the domain search functionality of the <see
/// href="https://haveibeenpwned.com/">HaveIBeenPwned</see> service
/// </summary>
public interface IHaveIBeenPwnedDomainClient
{
  /// <summary>
  /// Get all the breached users in a domain with the breaches they appeared in
  /// </summary>
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
}
