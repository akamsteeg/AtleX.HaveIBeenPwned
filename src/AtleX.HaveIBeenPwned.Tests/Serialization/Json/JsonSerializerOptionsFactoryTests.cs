// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Linq;
using System.Text.Json;
using AtleX.HaveIBeenPwned.Serialization.Json;
using AtleX.HaveIBeenPwned.Serialization.Json.Convertors;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Serialization.Json;

public class JsonSerializerOptionsFactoryTests
{
  [Fact]
  public void Create_ReturnsJsonSerializerOptions()
  {
    var o = JsonSerializerOptionsFactory.Create();

    Assert.IsType<JsonSerializerOptions>(o);
  }

  [Theory]
  [InlineData(typeof(DomainUserConvertor))]
  public void Create_AddsConvertor(Type typeOfConvertor)
  {
    var o = JsonSerializerOptionsFactory.Create();

    var convertor = o.Converters.FirstOrDefault(c => c.GetType() == typeOfConvertor);

    Assert.NotNull(convertor);
    Assert.IsType(typeOfConvertor, convertor);
  }
}
