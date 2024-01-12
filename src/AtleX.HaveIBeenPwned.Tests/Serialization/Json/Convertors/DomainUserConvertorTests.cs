using AtleX.HaveIBeenPwned.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Serialization.Json.Convertors;
public class DomainUserConvertorTests
{
  [Fact]
  public void Read()
  {
    var json = @"
        {
          ""user"": [
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
    Assert.Equal("user", firstUser.Alias);
    Assert.NotEmpty(firstUser.Breaches);
    Assert.Equal(2, firstUser.Breaches.Count());
  }
}
