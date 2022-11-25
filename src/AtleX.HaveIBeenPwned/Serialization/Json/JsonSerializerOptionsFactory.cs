using System.Text.Json;

namespace AtleX.HaveIBeenPwned.Serialization.Json;

/// <summary>
/// Represents a factory for <see cref="JsonSerializerOptions"/>
/// </summary>
internal static class JsonSerializerOptionsFactory
{
  /// <summary>
  /// Creates a new instance of <see cref="JsonSerializerOptions"/>
  /// </summary>
  /// <returns>
  /// The created <see cref="JsonSerializerOptions"/>
  /// </returns>
  public static JsonSerializerOptions Create()
  {
    var result = new JsonSerializerOptions();

#if NET6_0_OR_GREATER
    result.AddContext<JsonSerializationContext>();
#endif

    return result;
  }
}
