// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using Pitcher;

namespace AtleX.HaveIBeenPwned.Helpers;

/// <summary>
/// A helper to calculate hash codes
/// </summary>
/// <remarks>
/// See
/// <see href="https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode/263416#263416"/>
/// for the algorithm used
/// </remarks>
internal static class HashCodeHelper
{
  /// <summary>
  /// Calculate the hash code for the specified value
  /// </summary>
  /// <typeparam name="T1">
  /// The <see cref="System.Type"/> of the parameter
  /// </typeparam>
  /// <returns>
  /// A hash code for the specified value
  /// </returns>
  public static int GetHashCode<T1>(T1 value1)
    where T1 : notnull
  {
    Throw.ArgumentNull.WhenNull(value1, nameof(value1));

    var result = Constants.HashCode.InitialPrimeNumber;
    result = (result * Constants.HashCode.MultiplierPrimeNumber) + value1.GetHashCode();

    return result;
  }

  /// <summary>
  /// Calculate the hash code for the specified values
  /// </summary>
  /// <typeparam name="T1">
  /// The <see cref="System.Type"/> of the parameters
  /// </typeparam>
  /// <returns>
  /// A hash code for the specified values
  /// </returns>
  public static int GetHashCode<T1>(T1 value1, T1 value2)
    where T1 : notnull
  {
    Throw.ArgumentNull.WhenNull(value1, nameof(value1));
    Throw.ArgumentNull.WhenNull(value2, nameof(value2));

    var result = Constants.HashCode.InitialPrimeNumber;
    result = (result * Constants.HashCode.MultiplierPrimeNumber) + value1.GetHashCode();
    result = (result * Constants.HashCode.MultiplierPrimeNumber) + value2.GetHashCode();

    return result;
  }
}
