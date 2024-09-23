// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Polyfills;

/// <summary>
/// Represents polyfills for <see cref="HttpContent"/>
/// </summary>
internal static class HttpContentFills
{
#if !NET6_0_OR_GREATER
  /// <inheritDoc  cref="HttpContent.ReadAsStreamAsync()" />
  public static async Task<Stream> ReadAsStreamAsync(this HttpContent content, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var result = await content.ReadAsStreamAsync();

    return result;
  }

  /// <inheritDoc  cref="HttpContent.ReadAsStringAsync()" />
  public static async Task<string> ReadAsStringAsync(this HttpContent content, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var result = await content.ReadAsStringAsync();

    return result;
  }
#endif
}