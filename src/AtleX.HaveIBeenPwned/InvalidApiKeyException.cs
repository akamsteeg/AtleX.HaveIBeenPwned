using System;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents a <see cref="HaveIBeenPwnedClientException"/> for an unknown or expired API key
  /// </summary>
  [Serializable]
  public sealed class InvalidApiKeyException
    : HaveIBeenPwnedClientException
  {
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidApiKeyException"/>
    /// </summary>
    public InvalidApiKeyException()
      : base("The API key used is unknown or expired") // We hard-code the message so we don't end up with private API keys in logging systems etc.
    {
    }
  }
}
