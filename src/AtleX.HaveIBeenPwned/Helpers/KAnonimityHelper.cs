using Pitcher;
using System;
using System.Globalization;
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
    private const int KAnonimityRemainderLength = 35;

    /// <summary>
    /// Gets SHA1 KAnonomity part and remainder for the specified password
    /// </summary>
    /// <param name="password">
    /// The password to get the SHA1 hash from
    /// </param>
    /// <returns>
    /// A <see cref="ValueTuple{T1, T2}"/> with the KAnonimity part of the password
    /// and the KAnonimity remainder
    /// </returns>
    public static (string kAnonimityPart, string kAnonimityRemainder) GetKAnonimityPartsForPassword(string password)
    {
      Throw.ArgumentNull.WhenNullOrEmpty(password, nameof(password));

      var hash = GetSHA1HashForPassword(password);

      var kAnonimityHashPart = new StringBuilder(40); // SHA1 hash is 40 characters long
      foreach (var currentByte in hash)
      {
        kAnonimityHashPart.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", currentByte);
      }

      var result = (
        kAnonimityHashPart.ToString(0, KAnonimityPartLength),
        kAnonimityHashPart.ToString(KAnonimityPartLength, KAnonimityRemainderLength)
        );

      return result;
    }

    /// <summary>
    /// Gets the array of <see cref="byte"/> with the full SHA1 hash of the
    /// specified <see cref="string"/>
    /// </summary>
    /// <param name="dataToHash">
    /// The <see cref="string"/> to get the SHA1 hash from
    /// </param>
    /// <returns>
    /// The SHA1 hash of the specified <see cref="string"/>
    /// </returns>
    private static byte[] GetSHA1HashForPassword(string dataToHash)
    {
      using var sha1 = SHA1Managed.Create();

      var passwordRaw = Encoding.UTF8.GetBytes(dataToHash);
      var result = sha1.ComputeHash(passwordRaw);

      return result;
    }
  }
}
