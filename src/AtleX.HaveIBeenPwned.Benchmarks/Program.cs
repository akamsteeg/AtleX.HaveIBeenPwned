using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System;
using System.Reflection;

namespace AtleX.HaveIBeenPwned.Benchmarks;

public static class Program
{
  public static void Main(string[] args)
  {
    var config = GetConfig();
    BenchmarkSwitcher
      .FromAssembly(Assembly.GetExecutingAssembly())
      .Run(args, config);
  }

  private static ManualConfig GetConfig()
  {
    var config = ManualConfig.Create(DefaultConfig.Instance)
      .AddJob(
        Job.Default.WithToolchain(CsProjCoreToolchain.NetCoreApp80).AsBaseline(),
        Job.Default.WithToolchain(CsProjCoreToolchain.NetCoreApp60)
      )
      .AddDiagnoser(MemoryDiagnoser.Default);

    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
    {
      config.AddJob(Job.Default.WithToolchain(CsProjClassicNetToolchain.Net481));
    }

    config.SummaryStyle = SummaryStyle.Default
      .WithRatioStyle(RatioStyle.Percentage);

    return config;
  }
}
