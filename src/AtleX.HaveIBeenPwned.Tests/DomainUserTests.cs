using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;
public class DomainUserTests
{
  [Fact]
  public void ToString_ReturnsValueOfNameProperty()
  {
    const string alias = "example";

    var du = new DomainUser()
    {
      Alias = alias,
    };

    Assert.Equal(alias, du.Alias);
  }

  [Fact]
  public void Equals_WithNullObject_ReturnsFalse()
  {
    var du = new DomainUser();

    Assert.False(du.Equals((object)null));
  }

  [Fact]
  public void Equals_WithWrongTypeObject_ReturnsFalse()
  {
    var du = new DomainUser();

    Assert.False(du.Equals(string.Empty));
  }

  [Fact]
  public void Equals_WithSameTypeObjectButDifferentValues_ReturnsFalse()
  {
    var du = new DomainUser()
    {
      Alias = "FIRST",
    };

    var other = new DomainUser()
    {
      Alias = "SECOND",
    };

    Assert.False(du.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeObjectAndSameValues_ReturnsTrue()
  {
    var du = new DomainUser()
    {
      Alias = "VALUE",
    };

    var other = new DomainUser()
    {
      Alias = "VALUE",
    };

    Assert.True(du.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeButDifferentValues_ReturnsFalse()
  {
    var du = new DomainUser()
    {
      Alias = "FIRST",
    };

    var other = new DomainUser()
    {
      Alias = "SECOND",
    };

    Assert.False(du.Equals(other));
  }

  [Fact]
  public void Equals_WithSameTypeAndSameValues_ReturnsTrue()
  {
    var du = new DomainUser()
    {
      Alias = "VALUE",
    };

    var other = new DomainUser()
    {
      Alias = "VALUE",
    };

    Assert.True(du.Equals(other));
  }

  [Fact]
  public void GetHashCode_ReturnsDifferentHashCodesForDifferentValues()
  {
    var du = new DomainUser()
    {
      Alias = "FIRST",
    };

    var other = new DomainUser()
    {
      Alias = "SECOND",
    };

    Assert.NotEqual(du.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void GetHashCode_ReturnsSameHashCodesForSameValues()
  {
    var du = new DomainUser()
    {
      Alias = "VALUE",
    };

    var other = new DomainUser()
    {
      Alias = "VALUE",
    };

    Assert.Equal(du.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void EqualsOperater_WithNull_IsFalse()
  {
    var du = new DomainUser()
    {
      Alias = "FIRST",
    };

    DomainUser other = null;

    Assert.False(du == other);
  }

  [Theory]
  [InlineData("domainuser", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithSameType_IsExpected(string alias, bool expected)
  {
    var du = new DomainUser()
    {
      Alias = "domainuser",
    };

    var other = new DomainUser()
    {
      Alias = alias
    };

    Assert.Equal(expected, du == other);
  }

  [Theory]
  [InlineData("domainuser", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithObject_IsExpected(string alias, bool expected)
  {
    var du = new DomainUser()
    {
      Alias = "domainuser",
    };

    var other = new DomainUser()
    {
      Alias = alias
    };

    Assert.Equal(expected, du == (object)other);
  }

  [Fact]
  public void EqualsOperater_WithObjectOfDifferentType_IsFalsed()
  {
    var du = new DomainUser()
    {
      Alias = "domainuser",
    };

    var other = new Paste();

    Assert.False(du == other);
  }
}
