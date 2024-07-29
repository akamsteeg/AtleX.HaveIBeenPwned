// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using System.Reflection;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;
public class AssemblyTests
{
  [Fact]
  public void IsAssemblyStrongNamed()
  {
    const string expectedPublicKeyToken = "dd937fdd82a3e1d3";

    var fullNameOfAssembly = typeof(HaveIBeenPwnedClient).Assembly.FullName;

    var publicKeyOffset = fullNameOfAssembly.IndexOf("PublicKeyToken=");

    Assert.NotEqual(-1, publicKeyOffset);

    var publicKeyTokenOfAssembly = fullNameOfAssembly.Substring(publicKeyOffset + "PublicKeyToken=".Length);

    Assert.Equal(expectedPublicKeyToken, publicKeyTokenOfAssembly);
  }

  [Fact]
  public void HasIncludedFrameworkVersionInProductAttribute()
  {
    const string productStartsWith = "AtleX.HaveIBeenPwned (net"; // First part of the product. Must be "<assembly name> (net*" to cover unified .NET versions and .NET Standard

    var productAttribute = typeof(HaveIBeenPwnedClient).Assembly.GetCustomAttribute(typeof(AssemblyProductAttribute)) as AssemblyProductAttribute;

    Assert.NotNull(productAttribute);
    Assert.StartsWith(productStartsWith, productAttribute.Product);
  }
}
