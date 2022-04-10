using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

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

  [Fact]
  public void Equals_WithNullObject_ReturnsFalse()
  {
    var p = new Paste();

    Assert.False(p.Equals((object)null));
  }

  [Fact]
  public void Equals_WithWrongTypeObject_ReturnsFalse()
  {
    var p = new Paste();

    Assert.False(p.Equals(string.Empty));
  }

  [Fact]
  public void Equals_WithSameTypeObjectButDifferentValues_ReturnsFalse()
  {
    var p = new Paste()
    {
      Id = "FIRST",
    };

    var other = new Paste()
    {
      Id = "SECOND",
    };

    Assert.False(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeObjectAndSameValues_ReturnsTrue()
  {
    var p = new Paste()
    {
      Id = "VALUE",
    };

    var other = new Paste()
    {
      Id = "VALUE",
    };

    Assert.True(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeButDifferentValues_ReturnsFalse()
  {
    var p = new Paste()
    {
      Id = "FIRST",
    };

    var other = new Paste()
    {
      Id = "SECOND",
    };

    Assert.False(p.Equals(other));
  }

  [Fact]
  public void Equals_WithSameTypeAndSameValues_ReturnsTrue()
  {
    var p = new Paste()
    {
      Id = "VALUE",
    };

    var other = new Paste()
    {
      Id = "VALUE",
    };

    Assert.True(p.Equals(other));
  }

  [Fact]
  public void GetHashCode_ReturnsDifferentHashCodesForDifferentValues()
  {
    var p = new Paste()
    {
      Id = "FIRST",
    };

    var other = new Paste()
    {
      Id = "SECOND",
    };

    Assert.NotEqual(p.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void GetHashCode_ReturnsSameHashCodesForSameValues()
  {
    var p = new Paste()
    {
      Id = "VALUE",
    };

    var other = new Paste()
    {
      Id = "VALUE",
    };

    Assert.Equal(p.GetHashCode(), other.GetHashCode());
  }
}
