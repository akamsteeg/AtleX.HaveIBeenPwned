﻿// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Serialization.Json.Convertors;
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

    result.Converters.Add(new DomainUserConvertor());

#if NET8_0_OR_GREATER
    result.TypeInfoResolverChain.Add(new JsonSerializationContext());

    result.MakeReadOnly(); // Guard against modifications
#elif NET6_0_OR_GREATER                  
    result.AddContext<JsonSerializationContext>();
#endif

    return result;
  }
}
