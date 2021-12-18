using Pitcher;

namespace AtleX.HaveIBeenPwned.Helpers;

/// <summary>
/// A helper to calculate hash codes
/// </summary>
/// <remarks>
/// See
/// https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode/263416#263416
/// for the algorithm used
/// </remarks>
internal static class HashCodeHelper
{
  /// <summary>
  /// Calculate the hash code for the specified value
  /// </summary>
  /// <returns>
  /// A hash code for the specified value
  /// </returns>
  public static int GetHashCode<T1>(T1 value1)
    where T1 : notnull
  {
    Throw.ArgumentNull.WhenNull(value1, nameof(value1));

    var result = 17;
    result = result * 23 + value1.GetHashCode();

    return result;
  }

  /// <summary>
  /// Calculate the hash code for the specified values
  /// </summary>
  /// <returns>
  /// A hash code for the specified values
  /// </returns>
  public static int GetHashCode<T1, T2>(T1 value1, T2 value2)
    where T1 : notnull
    where T2 : notnull
  {
    Throw.ArgumentNull.WhenNull(value1, nameof(value1));
    Throw.ArgumentNull.WhenNull(value2, nameof(value2));

    var result = 17;
    result = result * 23 + value1.GetHashCode();
    result = result * 23 + value2.GetHashCode();

    return result;
  }
}
