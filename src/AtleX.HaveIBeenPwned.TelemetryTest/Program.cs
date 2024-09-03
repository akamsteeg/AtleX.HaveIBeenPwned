// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned;
using AtleX.HaveIBeenPwned.Telemetry;
using AtleX.HaveIBeenPwned.TelemetryTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("AtleX.HaveIBeenPwned.TelemetryTest");

var builder = new HostBuilder();

builder
  .ConfigureAppConfiguration(configBuilder =>
  {
    configBuilder
        .AddJsonFile("appsettings.json", optional: false);
  })
  .ConfigureLogging(logging =>
  {
    logging.ClearProviders();
    logging.AddConsole();
  })
  .ConfigureServices(s =>
  {
    s.AddHostedService<PasswordCheckService>();

    // Add the AtleX.HaveIBeenPwned stuff
    s.AddSingleton(_ => new HaveIBeenPwnedClientSettings()
    {
      ApplicationName = "AtleX.HaveIBeenPwned.TelemetryTest"
    })
    .AddScoped<IHaveIBeenPwnedClient, HaveIBeenPwnedClient>() // You can also use one of the more specialized interfaces
    .AddHttpClient<HaveIBeenPwnedClient>();

    // Enable telemetry
    var resourceBuilder = ResourceBuilder.CreateDefault().AddService("AtleX.HaveIBeenPwned.TelemetryTest",
        "AtleX.HaveIBeenPwned.TelemetryTest");

    s.AddOpenTelemetry()
        .WithMetrics(m =>
        {
          m
            .SetResourceBuilder(resourceBuilder)
            .AddMeter(TelemetryNames.RootName)
            .AddConsoleExporter(c => c.Targets = OpenTelemetry.Exporter.ConsoleExporterOutputTargets.Console);
        })
        .WithTracing(t =>
        {
          t
            .SetResourceBuilder(resourceBuilder)
            .AddSource(TelemetryNames.RootName)
            .AddConsoleExporter(c => c.Targets = OpenTelemetry.Exporter.ConsoleExporterOutputTargets.Console);
        });
  });

var host = builder.Build();

await host.RunAsync();
