// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AtleX.HaveIBeenPwned.TelemetryTest.Services;
internal class PasswordCheckService(IHaveIBeenPwnedClient hibpClient, ILogger<PasswordCheckService> logger)
    : BackgroundService
{
  private static readonly TimeSpan DelayBetweenRequests = TimeSpan.FromMilliseconds(1500);

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    string[] passwordsToCheck =
    [
      "DonaldDuck",
      "DaisyDuck",
      "HueyDuck",
      "DeweyDuck",
      "LouieDuck",
      "ScroogeMcDuck"
    ];

    while (!stoppingToken.IsCancellationRequested)
    {
      foreach (var currentPassword in passwordsToCheck)
      {
        var isPwned = await hibpClient.IsPwnedPasswordAsync(currentPassword, stoppingToken);

        var statusIdentifier = (isPwned) ? "Yes" : "No";

        logger.LogInformation("Is '{CurrentPassword}' pwned? {StatusIdentifier}", currentPassword, statusIdentifier);

        await Task.Delay(DelayBetweenRequests, stoppingToken);
      }
    }
  }
}
