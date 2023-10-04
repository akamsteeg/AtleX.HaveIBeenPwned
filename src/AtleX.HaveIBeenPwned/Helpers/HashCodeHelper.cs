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
  /// Gets the prime number to start the calculation of the hash code with
  /// </summary>
  private const int primeInitial = 17;

  /// <summary>
  /// Gets the prime number to multiply a previously calculated value with
  /// </summary>
  private const int primeMultiplier = 23;

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

    var result = primeInitial;
    result = (result * primeMultiplier) + value1.GetHashCode();

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

    var result = primeInitial;
    result = (result * primeMultiplier) + value1.GetHashCode();
    result = (result * primeMultiplier) + value2.GetHashCode();

    return result;
  }
}
