// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Reflection;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

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
    var job = Job.Default;

    var memoryDiagnoser = new MemoryDiagnoser(new MemoryDiagnoserConfig(displayGenColumns: false));
    //var disassemblyDiagnoser = new DisassemblyDiagnoser(new DisassemblyDiagnoserConfig());
    //var jitStatsDiagnoser = new JitStatsDiagnoser();

    var config = DefaultConfig.Instance
      .AddDiagnoser(memoryDiagnoser)
      //.AddDiagnoser(disassemblyDiagnoser)
      //.AddDiagnoser(jitStatsDiagnoser)
      .AddColumn(StatisticColumn.P95, StatisticColumn.OperationsPerSecond)
      .HideColumns(Column.Job, Column.Error, Column.StdDev, Column.RatioSD)
      .AddJob(
        job.WithRuntime(CoreRuntime.Core80),
        job.WithRuntime(CoreRuntime.Core10_0).AsBaseline()
      );

    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
    {
      config.AddJob(job.WithRuntime(ClrRuntime.Net481));
    }

    config.SummaryStyle = SummaryStyle.Default
      .WithRatioStyle(RatioStyle.Percentage);

    config.AddAnalyser(EnvironmentAnalyser.Default);
    config.AddValidator(JitOptimizationsValidator.FailOnError); // Fail when any of the referenced assemblies are not optimized

    config.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

    return config;
  }
}
