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

    (string, string) result;

    var encoding = Encoding.UTF8;
    var arrayPool = ArrayPool<byte>.Shared;

    var maxByteCount = encoding.GetByteCount(password); // UTF8 has at most 4 bytes per character but this will give us an accurate number

    var characterBuffer = arrayPool.Rent(maxByteCount); // PERF: Rent a buffer. It is at least the same size as the number of bytes of the password
    var hashBuffer = arrayPool.Rent(20); // PERF: Use a rented buffer to store the SHA1 hash. A SHA1 hash is always 20 bytes.

    try
    {
      var numberOfEncodedBytes = encoding.GetBytes(password, characterBuffer);

      var usedPartOfCharacterBuffer = characterBuffer.AsSpan()[..numberOfEncodedBytes]; // The rented buffer can be larger than the minimal size. We must only work on the part of the bufer that's used      

      var numberOfHashBytes = SHA1.HashData(usedPartOfCharacterBuffer, hashBuffer);

      var usedPartOfHashBuffer = hashBuffer.AsSpan()[..numberOfHashBytes];

      var kAnonimityHash = Convert.ToHexString(usedPartOfHashBuffer);

      var kAnonimityHashSpan = kAnonimityHash.AsSpan();

      result = (
        kAnonimityHashSpan[..Constants.KAnonimity.PartLength].ToString(),
         kAnonimityHashSpan.Slice(Constants.KAnonimity.PartLength, Constants.KAnonimity.RemainderLength).ToString()
         );
    }
    finally
    {
      // Make sure return the rented buffers

      arrayPool.Return(characterBuffer);
      arrayPool.Return(hashBuffer);
    }

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