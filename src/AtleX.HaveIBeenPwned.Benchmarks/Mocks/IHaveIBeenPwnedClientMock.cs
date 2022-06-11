using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GenFu;

namespace AtleX.HaveIBeenPwned.Benchmarks.Mocks;

public class IHaveIBeenPwnedClientMock
  : IHaveIBeenPwnedClient
{
  private static readonly IEnumerable<Breach> breaches = A.ListOf<Breach>(30);

  private static readonly IEnumerable<SiteBreach> siteBreaches = A.ListOf<SiteBreach>(30);

  private static readonly IEnumerable<Paste> pastes = A.ListOf<Paste>(30);

  public Task<IEnumerable<SiteBreach>> GetAllBreachesAsync()
  {
    return Task.FromResult(siteBreaches);
  }
  public Task<IEnumerable<SiteBreach>> GetAllBreachesAsync(CancellationToken cancellationToken)
  {
    return Task.FromResult(siteBreaches);
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
}
