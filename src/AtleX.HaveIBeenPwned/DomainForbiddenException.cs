namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a <see cref="HaveIBeenPwnedClientException"/> for when the specified API key
/// does not have access to the requested domain in the system
/// </summary>
#pragma warning disable RCS1194 // Implement exception constructors.
// We really want to avoid inadvertently including domains in exception
// messages etc. The exception messages might end up in logging or shown
// to end users. We don't want to leak private or internal domains that way
public sealed class DomainForbiddenException
#pragma warning restore RCS1194 // Implement exception constructors.
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
