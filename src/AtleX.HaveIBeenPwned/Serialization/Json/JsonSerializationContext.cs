#if NET6_0_OR_GREATER

using System.Text.Json.Serialization;

namespace AtleX.HaveIBeenPwned.Serialization.Json;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]

[JsonSerializable(typeof(Breach))]
[JsonSerializable(typeof(BreachMode))]
[JsonSerializable(typeof(Paste))]
[JsonSerializable(typeof(SiteBreach))]
internal sealed partial class JsonSerializationContext
  : JsonSerializerContext
{
}

#endif