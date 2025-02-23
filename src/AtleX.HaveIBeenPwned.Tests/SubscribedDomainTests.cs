// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;
public class SubscribedDomainTests
{
  [Fact]
  public void ToString_ReturnsValueOfDomainNameProperty()
  {
    const string domainName = "example.com";

    var sd = new SubscribedDomain()
    {
      DomainName = domainName,
    };

    Assert.Equal(domainName, sd.ToString());
  }

  [Fact]
  public void Equals_WithNullObject_ReturnsFalse()
  {
    var sd = new SubscribedDomain();

    Assert.False(sd.Equals((object)null));
  }

  [Fact]
  public void Equals_WithWrongTypeObject_ReturnsFalse()
  {
    var sd = new SubscribedDomain();

    Assert.False(sd.Equals(string.Empty));
  }

  [Fact]
  public void Equals_WithSameTypeObjectButDifferentValues_ReturnsFalse()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "FIRST",
    };

    var other = new SubscribedDomain()
    {
      DomainName = "SECOND",
    };

    Assert.False(sd.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeObjectAndSameValues_ReturnsTrue()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "VALUE",
    };

    var other = new SubscribedDomain()
    {
      DomainName = "VALUE",
    };

    Assert.True(sd.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeButDifferentValues_ReturnsFalse()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "FIRST",
    };

    var other = new SubscribedDomain()
    {
      DomainName = "SECOND",
    };

    Assert.False(sd.Equals(other));
  }

  [Fact]
  public void Equals_WithSameTypeAndSameValues_ReturnsTrue()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "VALUE",
    };

    var other = new SubscribedDomain()
    {
      DomainName = "VALUE",
    };

    Assert.True(sd.Equals(other));
  }

  [Fact]
  public void GetHashCode_ReturnsDifferentHashCodesForDifferentValues()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "FIRST",
    };

    var other = new SubscribedDomain()
    {
      DomainName = "SECOND",
    };

    Assert.NotEqual(sd.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void GetHashCode_ReturnsSameHashCodesForSameValues()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "VALUE",
    };

    var other = new SubscribedDomain()
    {
      DomainName = "VALUE",
    };

    Assert.Equal(sd.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void EqualsOperater_WithNull_IsFalse()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "FIRST",
    };

    SubscribedDomain other = null;

    Assert.False(sd == other);
  }

  [Theory]
  [InlineData("subscribeddomain", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithSameType_IsExpected(string alias, bool expected)
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "subscribeddomain",
    };

    var other = new SubscribedDomain()
    {
      DomainName = alias
    };

    Assert.Equal(expected, sd == other);
  }

  [Theory]
  [InlineData("subscribeddomain", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithObject_IsExpected(string alias, bool expected)
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "subscribeddomain",
    };

    var other = new SubscribedDomain()
    {
      DomainName = alias
    };

    Assert.Equal(expected, sd == (object)other);
  }

  [Fact]
  public void EqualsOperater_WithObjectOfDifferentType_IsFalse()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "subscribeddomain",
    };

    var other = new Paste();

    Assert.False(sd == other);
  }

  [Fact]
  public void NotEqualsOperater_WithNull_IsTrue()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "FIRST",
    };

    SubscribedDomain other = null;

    Assert.True(sd != other);
  }

  [Theory]
  [InlineData("subscribeddomain", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithSameType_IsExpected(string alias, bool expected)
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "subscribeddomain",
    };

    var other = new SubscribedDomain()
    {
      DomainName = alias
    };

    Assert.Equal(expected, sd != other);
  }

  [Theory]
  [InlineData("subscribeddomain", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithObject_IsExpected(string alias, bool expected)
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "subscribeddomain",
    };

    var other = new SubscribedDomain()
    {
      DomainName = alias
    };

    Assert.Equal(expected, sd != (object)other);
  }

  [Fact]
  public void NotEqualsOperater_WithObjectOfDifferentType_IsTrue()
  {
    var sd = new SubscribedDomain()
    {
      DomainName = "subscribeddomain",
    };

    var other = new Paste();

    Assert.True(sd != other);
  }
}
