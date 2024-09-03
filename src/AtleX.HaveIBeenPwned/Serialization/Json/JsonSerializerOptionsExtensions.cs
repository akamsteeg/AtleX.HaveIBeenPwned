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
  /// <summary>
  /// Gets the <see cref="JsonTypeInfo"/> contract metadata resolved by the
  /// current <see cref="JsonSerializerOptions"/> instance.
  /// </summary>
  /// <typeparam name="T">
  /// The type to get the contract metadata for.
  /// </typeparam>
  /// <returns>
  /// The contract metadata resolved for <typeparamref name="T"/>.
  /// </returns>
  /// <exception cref="ArgumentException">
  /// <typeparamref name="T"/> is not valid for serialization.
  /// </exception>
  /// <remarks>
  /// <para>
  /// Returned metadata can be downcast to <see cref="JsonTypeInfo{T}"/> and
  /// used with the relevant <see cref="JsonSerializer"/> overloads.
  ///</para>
  ///<para>
  /// If the <see cref="JsonSerializerOptions"/> instance is locked for
  /// modification, the method will return a cached instance for the metadata.
  /// </para>
  /// </remarks>
  public static JsonTypeInfo GetTypeInfo<T>(this JsonSerializerOptions options)
    => options.GetTypeInfo(typeof(T));
}
#endif