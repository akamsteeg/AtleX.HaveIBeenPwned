// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

#if NET6_0_OR_GREATER

using System;
using System.Collections.Generic;
using AtleX.HaveIBeenPwned.Serialization.Json;
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
  [InlineData(typeof(IEnumerable<DomainUser>))]
  [InlineData(typeof(IEnumerable<SubscribedDomain>))]
  public void Context_SupportsType(Type type)
  {
    var c = new JsonSerializationContext();

    var ti = c.GetTypeInfo(type);

    Assert.NotNull(ti);
  }
}
#endif