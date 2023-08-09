namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a <see cref="HaveIBeenPwnedClientException"/> for when the specified API key
/// does not have access to the requested domain in the system
/// </summary>
public sealed class DomainForbiddenException
  : HaveIBeenPwnedClientException
{

  /// <summary>
  /// Initializes a new instance of <see cref="DomainForbiddenException"/>
  /// </summary>
  public DomainForbiddenException()
    : base("Access to the domain is forbidden with the specified API key")
  {
  }
}
