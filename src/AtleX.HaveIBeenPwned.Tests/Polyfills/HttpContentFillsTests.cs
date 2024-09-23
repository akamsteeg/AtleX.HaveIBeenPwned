// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

#if !NET6_0_OR_GREATER

using AtleX.HaveIBeenPwned.Polyfills;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Polyfills;
public class HttpContentFillsTests
{
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public async Task ReadAsStreamAsync_WithCancellationToken(bool isCanceled)
  {
    using var cts = new CancellationTokenSource();
    using var content = new StreamContent(Stream.Null);

    if (isCanceled)
    {
      cts.Cancel();

      await Assert.ThrowsAsync<OperationCanceledException>(async () => await content.ReadAsStreamAsync(cts.Token));
    }
    else
    {
      var result = await content.ReadAsStreamAsync(cts.Token);

      Assert.NotNull(result);
    }
  }

  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public async Task ReadAsStringmAsync_WithCancellationToken(bool isCanceled)
  {
    using var cts = new CancellationTokenSource();
    using var content = new StreamContent(Stream.Null);

    if (isCanceled)
    {
      cts.Cancel();

      await Assert.ThrowsAsync<OperationCanceledException>(async () => await content.ReadAsStringAsync(cts.Token));
    }
    else
    {
      var result = await content.ReadAsStringAsync(cts.Token);

      Assert.NotNull(result);
    }
  }
}
#endif