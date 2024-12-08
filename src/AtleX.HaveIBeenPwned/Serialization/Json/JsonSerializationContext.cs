// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

#if NET6_0_OR_GREATER

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AtleX.HaveIBeenPwned.Serialization.Json;

#if NET8_0_OR_GREATER
[JsonSourceGenerationOptions(defaults: JsonSerializerDefaults.Web, GenerationMode = JsonSourceGenerationMode.Default)]
#else
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]
#endif

[JsonSerializable(typeof(Breach))]
[JsonSerializable(typeof(IEnumerable<Breach>))]

[JsonSerializable(typeof(Paste))]
[JsonSerializable(typeof(IEnumerable<Paste>))]

[JsonSerializable(typeof(SiteBreach))]
[JsonSerializable(typeof(IEnumerable<SiteBreach>))]

[JsonSerializable(typeof(IEnumerable<DomainUser>))] // For domain breaches

[JsonSerializable(typeof(IEnumerable<SubscribedDomain>))]

[ExcludeFromCodeCoverage]
internal sealed partial class JsonSerializationContext
  : JsonSerializerContext;
#endif