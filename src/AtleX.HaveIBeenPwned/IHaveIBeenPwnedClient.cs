namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a client for the <see
/// href="https://haveibeenpwned.com/">HaveIBeenPwned</see> service
/// </summary>
public interface IHaveIBeenPwnedClient
  : IHaveIBeenPwnedBreachesClient,
  IHaveIBeenPwnedPastesClient,
  IHaveIBeenPwnedPasswordClient,
  IHaveIBeenPwnedDomainClient
{
}
