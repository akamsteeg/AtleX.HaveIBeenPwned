#if NET6_0_OR_GREATER

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AtleX.HaveIBeenPwned.Serialization.Json;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]

[JsonSerializable(typeof(Breach))]
[JsonSerializable(typeof(IEnumerable<Breach>))]

[JsonSerializable(typeof(Paste))]
[JsonSerializable(typeof(IEnumerable<Paste>))]

[JsonSerializable(typeof(SiteBreach))]
[JsonSerializable(typeof(IEnumerable<SiteBreach>))]

[ExcludeFromCodeCoverage]
internal sealed partial class JsonSerializationContext
  : JsonSerializerContext
{
}

#endif