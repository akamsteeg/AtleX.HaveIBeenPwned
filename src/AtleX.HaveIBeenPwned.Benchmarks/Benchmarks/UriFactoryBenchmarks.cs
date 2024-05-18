// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using BenchmarkDotNet.Attributes;
using System;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks;

public class UriFactoryBenchmarks
{
  private const string Account = "mail@example.com";

  private const string Domain = "example.com";

  [Benchmark]
  public Uri GetAllBreachesUri()
  {
    return UriFactory.GetAllBreachesUri();
  }

  [Benchmark]
  [Arguments(BreachMode.All)]
  [Arguments(BreachMode.ExcludeUnverified)]
  public Uri GetBreachesForAccountUri(BreachMode breachMode)
  {
    return UriFactory.GetBreachesForAccountUri(Account, breachMode);
  }

  [Benchmark]
  public Uri GetPasteAccountUri()
  {
    return UriFactory.GetPasteAccountUri(Account);
  }

  [Benchmark]
  public Uri GetPwnedPasswordUri()
  {
    return UriFactory.GetPwnedPasswordUri("5BAA6");
  }

  [Benchmark]
  public Uri GetBreachedDomainUsersUri()
  {
    return UriFactory.GetBreachedDomainUsersUri(Domain);
  }

  [Benchmark]
  public Uri GetSubscribedDomainsUri()
  {
    return UriFactory.GetSubscribedDomainsUri();
  }
}
