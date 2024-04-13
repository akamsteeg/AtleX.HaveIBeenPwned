using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Validators;
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

    config.AddValidator(JitOptimizationsValidator.FailOnError); // Fail when any of the referenced assemblies are not optimized

    config.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

    return config;
  }
}
