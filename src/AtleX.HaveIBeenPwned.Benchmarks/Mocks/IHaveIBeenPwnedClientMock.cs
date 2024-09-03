// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;

namespace AtleX.HaveIBeenPwned.Benchmarks.Mocks;

public class IHaveIBeenPwnedClientMock
  : IHaveIBeenPwnedClient
{
  private static readonly IEnumerable<Breach> breaches = new Faker<Breach>().Generate(10);

  private static readonly SiteBreach latestBreach = new Faker<SiteBreach>().Generate();

  private static readonly IEnumerable<SiteBreach> siteBreaches = new Faker<SiteBreach>().Generate(30);

  private static readonly IEnumerable<Paste> pastes = new Faker<Paste>().Generate(30);

  private static readonly IEnumerable<DomainUser> domainUsers = new Faker<DomainUser>().Generate(30);

  private static readonly IEnumerable<SubscribedDomain> subscribedDomains = new Faker<SubscribedDomain>().Generate(5);

  public Task<IEnumerable<SiteBreach>> GetAllBreachesAsync()
  {
    return Task.FromResult(siteBreaches);
  }
  public Task<IEnumerable<SiteBreach>> GetAllBreachesAsync(CancellationToken cancellationToken)
  {
    return Task.FromResult(siteBreaches);
  }

  public Task<SiteBreach> GetLatestBreachAsync()
  {
    return Task.FromResult(latestBreach);
  }

  public Task<SiteBreach> GetLatestBreachAsync(CancellationToken cancellationToken)
  {
    return Task.FromResult(latestBreach);
  }

  public Task<IEnumerable<DomainUser>> GetBreachedDomainUsersAsync(string domain)
  {
    return Task.FromResult(domainUsers);
  }

  public Task<IEnumerable<DomainUser>> GetBreachedDomainUsersAsync(string domain, CancellationToken cancellationToken)
  {
    return Task.FromResult(domainUsers);
  }

  public Task<IEnumerable<Breach>> GetBreachesAsync(string account)
  {
    return Task.FromResult(breaches);
  }

  public Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes)
  {
    return Task.FromResult(breaches);
  }

  public Task<IEnumerable<Breach>> GetBreachesAsync(string account, CancellationToken cancellationToken)
  {
    return Task.FromResult(breaches);
  }

  public Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes, CancellationToken cancellationToken)
  {
    return Task.FromResult(breaches);
  }

  public Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress)
  {
    return Task.FromResult(pastes);
  }

  public Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress, CancellationToken cancellationToken)
  {
    return Task.FromResult(pastes);
  }

  public Task<bool> IsPwnedPasswordAsync(string password)
  {
    return Task.FromResult(true);
  }

  public Task<bool> IsPwnedPasswordAsync(string password, CancellationToken cancellationToken)
  {
    return Task.FromResult(true);
  }

  public Task<IEnumerable<SubscribedDomain>> GetSubscribedDomainsAsync()
  {
    return Task.FromResult(subscribedDomains);
  }

  public Task<IEnumerable<SubscribedDomain>> GetSubscribedDomainsAsync(CancellationToken cancellationToken)
  {
    return Task.FromResult(subscribedDomains);
  }
}
