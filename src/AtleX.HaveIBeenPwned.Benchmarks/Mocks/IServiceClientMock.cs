using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AtleX.HaveIBeenPwned.Data;
using GenFu;

namespace AtleX.HaveIBeenPwned.Benchmarks.Mocks
{
  public class IServiceClientMock
    : IServiceClient
  {
    private static readonly IEnumerable<Breach> breaches = A.ListOf<Breach>(30);

    private static readonly IEnumerable<Paste> pastes = A.ListOf<Paste>(30);

    public void Dispose()
    {
      // NOP
    }

    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account)
    {
      return breaches;
    }

    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes)
    {
      return breaches;
    }

    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, CancellationToken cancellationToken)
    {
      return breaches;
    }

    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes, CancellationToken cancellationToken)
    {
      return breaches;
    }

    public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress)
    {
      return pastes;
    }

    public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress, CancellationToken cancellationToken)
    {
      return pastes;
    }

    public async Task<bool> IsPwnedPasswordAsync(string password)
    {
      return await Task.FromResult(true);
    }

    public async Task<bool> IsPwnedPasswordAsync(string password, CancellationToken cancellationToken)
    {
      return await Task.FromResult(true);
    }
  }
}
