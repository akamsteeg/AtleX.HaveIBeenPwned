﻿// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        logger.LogInformation("Is '{currentPassword}' pwned? {statusIdentifier}", currentPassword, statusIdentifier);

        await Task.Delay(DelayBetweenRequests, stoppingToken);
      }
    }
  }
}
