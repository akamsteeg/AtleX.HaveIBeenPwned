#if NET8_0_OR_GREATER

using AtleX.HaveIBeenPwned.Serialization.Json;
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
}
#endif