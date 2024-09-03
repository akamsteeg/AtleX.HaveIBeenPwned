// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

#if NET8_0_OR_GREATER

using AtleX.HaveIBeenPwned.Serialization.Json;
using System;
using System.Text.Json.Serialization.Metadata;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Serialization.Json;
public class JsonSerializerOptionsExtensionsTests
{
  [Fact]
  public void GetTypeInfoT_Succeeds()
  {
    var o = JsonSerializerOptionsFactory.Create();

    var typeInfo = o.GetTypeInfo<Breach>();

    Assert.NotNull(typeInfo);
    Assert.IsType<JsonTypeInfo<Breach>>(typeInfo);
  }

  [Fact]
  public void GetTypeInfoT_WithNotSupportedType_ThrowsNotSupportedException()
  {
    var o = JsonSerializerOptionsFactory.Create();

    Assert.Throws<NotSupportedException>(() =>  o.GetTypeInfo<FactAttribute>());
  }
}
#endif