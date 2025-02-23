// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

#if NET8_0_OR_GREATER

using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace AtleX.HaveIBeenPwned.Serialization.Json;

/// <summary>
/// Extension methods for <see cref="JsonSerializerOptions"/>
/// </summary>
internal static class JsonSerializerOptionsExtensions
{
  /// <inheritdoc cref="System.Text.Json.JsonSerializerOptions.GetTypeInfo(Type)"/>
  public static JsonTypeInfo<T> GetTypeInfo<T>(this JsonSerializerOptions options)
    => (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));
}
#endif