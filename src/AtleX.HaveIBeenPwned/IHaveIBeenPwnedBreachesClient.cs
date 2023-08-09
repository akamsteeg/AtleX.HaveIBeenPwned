using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned;
/// <summary>
/// Represents a client for the breaches functionality of the <see
/// href="https://haveibeenpwned.com/">HaveIBeenPwned</see> service
/// </summary>
public interface IHaveIBeenPwnedBreachesClient
{
  /// <summary>
  /// Get all site breaches in the system
  /// </summary>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="SiteBreach"/> in the system
  /// </returns>
  Task<IEnumerable<SiteBreach>> GetAllBreachesAsync();

  /// <summary>
  /// Get all site breaches in the system
  /// </summary>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="SiteBreach"/> in the system
  /// </returns>
  Task<IEnumerable<SiteBreach>> GetAllBreachesAsync(CancellationToken cancellationToken);

  /// <summary>
  /// Get the breaches for an account
  /// </summary>
  /// <param name="account">
  /// The account to get the breaches for
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="Breach"/> the account was found in
  /// </returns>
  Task<IEnumerable<Breach>> GetBreachesAsync(string account);

  /// <summary>
  /// Get the breaches for an account
  /// </summary>
  /// <param name="account">
  /// The account to get the breaches for
  /// </param>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="Breach"/> the account was found in
  /// </returns>
  Task<IEnumerable<Breach>> GetBreachesAsync(string account, CancellationToken cancellationToken);

  /// <summary>
  /// Get the breaches for an account
  /// </summary>
  /// <param name="account">
  /// The account to get the breaches for
  /// </param>
  /// <param name="modes">
  /// The <see cref="BreachMode"/> flags to specify extra breaches to get
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="Breach"/> the account was found in
  /// </returns>
  Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes);

  /// <summary>
  /// Get the breaches for an account
  /// </summary>
  /// <param name="account">
  /// The account to get the breaches for
  /// </param>
  /// <param name="modes">
  /// The <see cref="BreachMode"/> flags to specify extra breaches to get
  /// </param>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="Breach"/> the account was found in
  /// </returns>
  Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes, CancellationToken cancellationToken);

  /// <summary>
  /// Get the latest breach added to the system
  /// </summary>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the latest
  /// <see cref="Breach"/> added to the system
  /// </returns>
  Task<SiteBreach?> GetLatestBreachAsync();

  /// <summary>
  /// Get the latest breach added to the system
  /// </summary>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the latest
  /// <see cref="Breach"/> added to the system
  /// </returns>
  Task<SiteBreach?> GetLatestBreachAsync(CancellationToken cancellationToken);
}