using Pitcher;
using System.Security.Cryptography;
using System.Text;

namespace AtleX.HaveIBeenPwned.Helpers
{
  /// <summary>
  /// Represents a helper for K-Anonimity functionality
  /// </summary>
  internal static class KAnonimityHelper
  {
    /// <summary>
    /// Gets the length of the KAnonimity part
    /// to send to the HaveIBeenPwned API
    /// </summary>
    private const int KAnonimityPartLength = 5;

    /// <summary>
    /// Gets the length of the remainder of the
    /// hash that serves as the suffix of the
    /// KAnonimity system
    /// </summary>
    private const int KAnonimitySuffixLength = 35;

    /// <summary>
    /// Gets SHA1 hash for the specified password
    /// </summary>
    /// <param name="password">
    /// The password to get the SHA1 hash from
    /// </param>
    /// <returns>
    /// The SHA1 hash of the specified password
    /// </returns>
    public static (string kAnonimityPart, string kAnonimitySuffix) GetKAnonimityPartsForPassword(string password)
    {
      Throw.ArgumentNull.WhenNull(password, nameof(password));

      using (var sha1 = new SHA1Managed())
      {
        var passwordRaw = Encoding.UTF8.GetBytes(password);

        var hash = sha1.ComputeHash(passwordRaw);

        var kAnonimityHashPart = new StringBuilder(40); // SHA1 hash is 40 characters long
        foreach (var currentByte in hash)
        {
          kAnonimityHashPart.Append(currentByte.ToString("X2"));
        }

        var result = (
          kAnonimityHashPart.ToString(0, KAnonimityPartLength),
          kAnonimityHashPart.ToString(KAnonimityPartLength, KAnonimitySuffixLength)
          );

        return result;
      }
    }
  }
}
