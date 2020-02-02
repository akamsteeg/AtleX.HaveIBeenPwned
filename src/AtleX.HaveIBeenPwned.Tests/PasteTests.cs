using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class PasteTests
  {
    [Fact]
    public void ToString_ReturnsValueOfNameProperty()
    {
      const string title = "PASTE_TITLE";

      var p = new Paste()
      {
        Title = title,
      };

      Assert.Equal(title, p.Title);
    }
  }
}
