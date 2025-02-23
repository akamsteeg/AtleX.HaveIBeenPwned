// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AtleX.HaveIBeenPwned.Serialization.Json;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Serialization.Json.Convertors;
public class DomainUserConvertorTests
{
  [Fact]
  public void Read_WithValidInput_Succeeds()
  {
    var json = @"
        {
          ""user1"": [
                    ""breach1"",
                    ""breach2""
                  ],
        ""user2"": [
                    ""breach3""
                  ]   
         }";

    var options = JsonSerializerOptionsFactory.Create();

    var result = JsonSerializer.Deserialize<IEnumerable<DomainUser>>(json, options);

    Assert.NotNull(result);
    Assert.NotEmpty(result);

    var firstUser = result.First();
    Assert.Equal("user1", firstUser.Alias);
    Assert.NotEmpty(firstUser.Breaches);
    Assert.Equal(2, firstUser.Breaches.Count());

    var secondUser = result.Skip(1).First();
    Assert.Equal("user2", secondUser.Alias);
    Assert.NotEmpty(secondUser.Breaches);
    Assert.Single(secondUser.Breaches);
  }

  [Fact]
  public void Write_ThrowsNotImplementedException()
  {
    IEnumerable<DomainUser> o = new[]
      {
        new DomainUser()
      };

    var options = JsonSerializerOptionsFactory.Create();

    Assert.Throws<NotImplementedException>(() => JsonSerializer.Serialize(o, options));
  }
}
