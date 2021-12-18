namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a client for the HaveIBeenPwned service
/// </summary>
public interface IHaveIBeenPwnedClient
  : IHaveIBeenPwnedBreachesClient, IHaveIBeenPwnedPastesClient, IHaveIBeenPwnedPasswordClient
{
}
