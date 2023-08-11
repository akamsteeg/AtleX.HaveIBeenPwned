#if NET6_0_OR_GREATER

using AtleX.HaveIBeenPwned.Serialization.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Serialization.Json;

public class JsonSerializationContextTests
{
  [Theory]
  [InlineData(typeof(Breach))]
  [InlineData(typeof(IEnumerable<Breach>))]
  [InlineData(typeof(Paste))]
  [InlineData(typeof(IEnumerable<Paste>))]
  [InlineData(typeof(SiteBreach))]
  [InlineData(typeof(IEnumerable<SiteBreach>))]
  [InlineData(typeof(Dictionary<string, IEnumerable<string>>))]
  public void Context_SupportsType(Type type)
  {
    var c = new JsonSerializationContext();

    var ti = c.GetTypeInfo(type);

    Assert.NotNull(ti);
  }
}
#endif