using AtleX.HaveIBeenPwned.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Tests.Mocks
{
  public class DisposableFakeClient
    : IDisposable, IHaveIBeenPwnedClient
  {
    public bool IsDisposed
    {
      get;
      private set;
    }

    public void Dispose()
    {
      this.IsDisposed = true;
    }

    public Task<IEnumerable<Breach>> GetBreachesAsync(string account)
    {
      return Task.FromResult(Enumerable.Empty<Breach>());
    }

    public Task<IEnumerable<Breach>> GetBreachesAsync(string account, CancellationToken cancellationToken)
    {
      return Task.FromResult(Enumerable.Empty<Breach>());
    }

    public Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes)
    {
      return Task.FromResult(Enumerable.Empty<Breach>());
    }

    public Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes, CancellationToken cancellationToken)
    {
      return Task.FromResult(Enumerable.Empty<Breach>());
    }

    public Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress)
    {
      return Task.FromResult(Enumerable.Empty<Paste>());
    }

    public Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress, CancellationToken cancellationToken)
    {
      return Task.FromResult(Enumerable.Empty<Paste>());
    }

    public Task<bool> IsPwnedPasswordAsync(string password)
    {
      return Task.FromResult(false);
    }

    public Task<bool> IsPwnedPasswordAsync(string password, CancellationToken cancellationToken)
    {
      return Task.FromResult(false);
    }
  }
}
