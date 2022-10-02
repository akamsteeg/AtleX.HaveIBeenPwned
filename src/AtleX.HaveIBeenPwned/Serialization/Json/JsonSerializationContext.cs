#if NET6_0_OR_GREATER

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AtleX.HaveIBeenPwned.Serialization.Json;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]

[JsonSerializable(typeof(Breach))]
[JsonSerializable(typeof(Paste))]
[JsonSerializable(typeof(SiteBreach))]

[ExcludeFromCodeCoverage]
internal sealed partial class JsonSerializationContext
  : JsonSerializerContext
{
}

#endif