using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System.Reflection;

namespace AtleX.HaveIBeenPwned.Benchmarks
{
  public static class Program
  {
    private static void Main(string[] args)
    {
      var config = GetConfig();
      BenchmarkSwitcher
        .FromAssembly(Assembly.GetExecutingAssembly())
        .Run(args, config);
    }

    private static IConfig GetConfig()
    {
      var config = ManualConfig.Create(DefaultConfig.Instance);

      config
        .AddDiagnoser(BenchmarkDotNet.Diagnosers.MemoryDiagnoser.Default);

      config.AddJob(
        Job.Default.WithToolchain(CsProjCoreToolchain.NetCoreApp60).AsBaseline(),
        Job.Default.WithToolchain(CsProjCoreToolchain.NetCoreApp31),
        Job.Default.WithToolchain(CsProjClassicNetToolchain.Net48)
        );

      return config;
    }
  }
}
