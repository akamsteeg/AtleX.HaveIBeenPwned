using AtleX.HaveIBeenPwned.Serialization.Json;
using System.Text.Json;
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
}
