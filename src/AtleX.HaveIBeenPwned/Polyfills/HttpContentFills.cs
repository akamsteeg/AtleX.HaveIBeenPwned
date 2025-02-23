// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

namespace AtleX.HaveIBeenPwned.Polyfills;

/// <summary>
/// Represents polyfills for <see cref="System.Net.Http.HttpContent"/>
/// </summary>
internal static class HttpContentFills
{
#if !NET6_0_OR_GREATER
  /// <inheritDoc  cref="System.Net.Http.HttpContent.ReadAsStreamAsync()" />
  public static async System.Threading.Tasks.Task<System.IO.Stream> ReadAsStreamAsync(this System.Net.Http.HttpContent content, System.Threading.CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var result = await content.ReadAsStreamAsync();

    return result;
  }

  /// <inheritDoc  cref="System.Net.Http.HttpContent.ReadAsStringAsync()" />
  public static async System.Threading.Tasks.Task<string> ReadAsStringAsync(this System.Net.Http.HttpContent content, System.Threading.CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var result = await content.ReadAsStringAsync();

    return result;
  }
#endif
}