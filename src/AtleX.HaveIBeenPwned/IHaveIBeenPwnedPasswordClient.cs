// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a client for the leaked passwords functionality of the
/// <see href="https://haveibeenpwned.com/">HaveIBeenPwned</see> service
/// </summary>
/// <remarks>
/// The pwned passwords API uses k-anonimity so no passwords are send
/// to the HaveIBeenPwned.com API. See also
/// <see href="https://haveibeenpwned.com/API/v3#PwnedPasswords"/>.
/// </remarks>
public interface IHaveIBeenPwnedPasswordClient
{
  /// <summary>
  /// Gets whether the specified password is found in a password list
  /// </summary>
  /// <param name="password">
  /// The password to check
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with a <see cref="bool"/>
  /// indicating whether the password was found or not
  /// </returns>
  /// <remarks>
  /// The pwned passwords API uses k-anonimity so no passwords are send
  /// to the HaveIBeenPwned.com API. See also
  /// <see href="https://haveibeenpwned.com/API/v3#PwnedPasswords"/>.
  /// </remarks>
  Task<bool> IsPwnedPasswordAsync(string password);

  /// <summary>
  /// Gets whether the specified password is found in a password list
  /// </summary>
  /// <param name="password">
  /// The password to check
  /// </param>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with a <see cref="bool"/>
  /// indicating whether the password was found or not
  /// </returns>
  /// <remarks>
  /// The pwned passwords API uses k-anonimity so no passwords are send
  /// to the HaveIBeenPwned.com API. See also
  /// <see href="https://haveibeenpwned.com/API/v3#PwnedPasswords"/>.
  /// </remarks>
  Task<bool> IsPwnedPasswordAsync(string password, CancellationToken cancellationToken);
}
