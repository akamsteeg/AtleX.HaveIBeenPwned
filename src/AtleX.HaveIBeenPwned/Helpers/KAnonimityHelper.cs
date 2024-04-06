using Pitcher;
using System;
using System.Buffers;
using System.Security.Cryptography;
using System.Text;

#pragma warning disable CA5350 // Do Not Use Weak Cryptographic Algorithms

// We know SHA1 is a weak algorithm but that's just the way K-Anonimity
// works. And it's not used as a hashing algorithm that must be
// cryptographically secure.

namespace AtleX.HaveIBeenPwned.Helpers;

/// <summary>
/// Represents a helper for K-Anonimity functionality
/// </summary>
internal static class KAnonimityHelper
{
#if NET6_0_OR_GREATER

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

    var maxByteCount = Encoding.UTF8.GetByteCount(password); // UTF8 has at most 4 bytes per character but this will give us an accurate number

    var hashBuffer = ArrayPool<byte>.Shared.Rent(maxByteCount);

    var numberOfEncodedBytes = Encoding.UTF8.GetBytes(password, hashBuffer);

    var usedPartOfBuffer = hashBuffer.AsSpan()[..numberOfEncodedBytes];

    var hash = SHA1.HashData(usedPartOfBuffer);

    var kAnonimityHash = Convert.ToHexString(hash).AsSpan();

    var result = (
      kAnonimityHash[..Constants.KAnonimity.PartLength].ToString(),
       kAnonimityHash.Slice(Constants.KAnonimity.PartLength, Constants.KAnonimity.RemainderLength).ToString()
       );

    ArrayPool<byte>.Shared.Return(hashBuffer);

    return result;
  }

#else
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

    var kAnonimityHashPart = new StringBuilder(Constants.KAnonimity.TotalLength);
    foreach (var currentByte in hash)
    {
      kAnonimityHashPart.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "{0:X2}", currentByte);
    }

    var result = (
      kAnonimityHashPart.ToString(0, Constants.KAnonimity.PartLength),
      kAnonimityHashPart.ToString(Constants.KAnonimity.PartLength, Constants.KAnonimity.RemainderLength)
      );

    return result;
  }

  /// <summary>
  /// Gets the array of <see cref="byte"/> with the full SHA1 hash of the
  /// specified <see cref="string"/>
  /// </summary>
  /// <param name="password">
  /// The <see cref="string"/> to get the SHA1 hash from
  /// </param>
  /// <returns>
  /// The SHA1 hash of the specified <see cref="string"/>
  /// </returns>
  private static byte[] GetSHA1HashForPassword(string password)
  {
    var passwordRaw = Encoding.UTF8.GetBytes(password);

    using var sha1 = SHA1.Create();
    var result = sha1.ComputeHash(passwordRaw);

    return result;
  }
#endif
}

#pragma warning restore CA5350 // Do Not Use Weak Cryptographic Algorithms