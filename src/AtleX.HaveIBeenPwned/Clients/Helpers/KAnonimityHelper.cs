using Pitcher;
using SwissArmyKnife;
using System.Security.Cryptography;
using System.Text;

namespace AtleX.HaveIBeenPwned.Clients.Helpers
{
  /// <summary>
  /// Represents a helper for K-Anonimity functionality
  /// </summary>
  public static class KAnonimityHelper
  {
    /// <summary>
    /// Gets SHA1 hash for the specified password
    /// </summary>
    /// <param name="password">
    /// The password to get the SHA1 hash from
    /// </param>
    /// <returns>
    /// The SHA1 hash of the specified password
    /// </returns>
    public static string GetHashForPassword(string password)
    {
      Throw.ArgumentNull.When(password.IsNullOrWhiteSpace(), nameof(password));

      using (var sha1 = new SHA1Managed())
      {
        var passwordRaw = Encoding.UTF8.GetBytes(password);

        var hash = sha1.ComputeHash(passwordRaw);

        var kAnonimityHashPart = new StringBuilder(40); // SHA1 hash is 40 characters long
        foreach (var currentByte in hash)
        {
          kAnonimityHashPart.Append(currentByte.ToString("X2"));
        }

        var result = kAnonimityHashPart.ToString();

        return result;
      }
    }
  }
}
