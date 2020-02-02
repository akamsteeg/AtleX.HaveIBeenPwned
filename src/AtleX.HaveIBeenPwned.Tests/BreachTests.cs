using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class BreachTests
  {
    [Fact]
    public void ToString_ReturnsValueOfNameProperty()
    {
      const string name = "BREACH_NAME";

      var b = new Breach()
      {
        Name = name,
      };

      Assert.Equal(name, b.Name);
    }
  }
}
