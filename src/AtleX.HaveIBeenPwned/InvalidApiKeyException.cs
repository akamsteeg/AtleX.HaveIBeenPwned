namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a <see cref="HaveIBeenPwnedClientException"/> for an unknown or expired API key
/// </summary>
#pragma warning disable RCS1194 // Implement exception constructors.
// We really want to avoid inadvertently including API keys in exception
// messages etc. The exception messages might end up in logging or shown
// to end users. We don't want to leak an API key that way
public sealed class InvalidApiKeyException
#pragma warning restore RCS1194 // Implement exception constructors.
  : HaveIBeenPwnedClientException
{
  /// <summary>
  /// Initializes a new instance of <see cref="InvalidApiKeyException"/>
  /// </summary>
  public InvalidApiKeyException()
    : base("The API key is unknown or expired") // We hard-code the message so we don't end up with private API keys in logging systems etc.
  {
  }
}
