// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// <para>
/// Represents a client for the <see href="https://haveibeenpwned.com/">HaveIBeenPwned</see> service
/// </para>
/// <para>
/// See <see cref="IHaveIBeenPwnedBreachesClient"/>, <see cref="IHaveIBeenPwnedPastesClient"/>, <see
/// cref="IHaveIBeenPwnedPasswordClient"/> or <see cref="IHaveIBeenPwnedDomainClient"/> for more
/// specialized interfaces implemented by this interface.
/// </para>
/// </summary>
public interface IHaveIBeenPwnedClient
  : IHaveIBeenPwnedBreachesClient,
  IHaveIBeenPwnedPastesClient,
  IHaveIBeenPwnedPasswordClient,
  IHaveIBeenPwnedDomainClient
{
}
