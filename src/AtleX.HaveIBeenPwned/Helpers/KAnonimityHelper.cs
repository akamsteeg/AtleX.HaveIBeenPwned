using Pitcher;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace AtleX.HaveIBeenPwned.Helpers;

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

#if NET6_0_OR_GREATER
    var kAnonimityHash = Convert.ToHexString(hash).AsSpan();

    var result = (
       kAnonimityHash[..KAnonimityPartLength].ToString(),
        kAnonimityHash.Slice(KAnonimityPartLength, KAnonimityRemainderLength).ToString()
      );
#else
    var kAnonimityHashPart = new StringBuilder(KAnonimityPartLength + KAnonimityRemainderLength); // SHA1 hash is 40 characters long
    foreach (var currentByte in hash)
    {
      kAnonimityHashPart.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", currentByte);
    }

    var result = (
      kAnonimityHashPart.ToString(0, KAnonimityPartLength),
      kAnonimityHashPart.ToString(KAnonimityPartLength, KAnonimityRemainderLength)
      );
#endif

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
    var passwordRaw = Encoding.UTF8.GetBytes(dataToHash);

#pragma warning disable CA5350 // Do Not Use Weak Cryptographic Algorithms

    // We know SHA1 is a weak algorithm but that's just the way K-Anonimity
    // works. And it's not used as a hashing algorithm that must be
    // cryptographically secure.

#if NET6_0_OR_GREATER
    var result = SHA1.HashData(passwordRaw);
#elif NETSTANDARD2_0_OR_GREATER
    using var sha1 = SHA1.Create();
    var result = sha1.ComputeHash(passwordRaw);
#endif

#pragma warning restore CA5350 // Do Not Use Weak Cryptographic Algorithms

    return result;
  }
}
