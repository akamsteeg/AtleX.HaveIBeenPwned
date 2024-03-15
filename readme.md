# AtleX.HaveIBeenPwned

AtleX.HaveIBeenPwned is a client library for the [HaveIBeenPwned.com website](https://haveibeenpwned.com/). It supports 
finding breaches an account was in, which pastes contained the user's email address and to check whether a password
was in a breach or not.

The AtleX.HaveIBeenPwned library is highly optimized for ease of use, great performance and low resource usage. It is 
thread safe, simple to plug into ASP.net Core(+)'s dependency injection system and usable on many platforms.

:warning: For some actions, like getting the pastes and breaches for an account, the [HaveIBeenPwned.com API](https://haveibeenpwned.com/) 
requires a API key. These API keys are available on the [HaveIBeenPwned.com website](https://haveibeenpwned.com/API/Key).

[![Build status](https://interastra.visualstudio.com/OSS%20-%20CI/_apis/build/status/AtleX.HaveIBeenPwned%20CI?branchName=master)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) [![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/interastra/OSS%20-%20CI/11.svg?maxAge=3600)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) 


## Trust

Leaked data can be a sensitive subject. Just like the [HaveIBeenPwned.com website and API](https://haveibeenpwned.com/), this 
library is carefully designed to be trustworthy and require the least amount of data possible to work. There is no collection
of data. All published packages have reproducible builds, meaning that a build from a certain source version will always
result in the same binaries. Additionally, not a single part of the library is obfuscated. Anyone can easily inspect binaries
in a tool like [ILSpy](https://github.com/icsharpcode/ILSpy) or use a debugger to step through and inspect everything.

# Platform support

|  .NET (5.0+)       |  .NET Core (2.0+)  | .NET Standard (2.0+)    | .NET Framework (4.7.2+) |
|:------------------:|:------------------:|:-----------------------:|:-----------------------:|
| :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark:      | :heavy_check_mark:      |

A full overview of explicitly and implicitly [supported frameworks is available on NuGet.org](https://www.nuget.org/packages/AtleX.HaveIBeenPwned/#supportedframeworks-body-tab).


# Installation

[![NuGet](https://img.shields.io/nuget/v/AtleX.HaveIBeenPwned.svg?cacheSeconds=3600)](https://www.nuget.org/packages/AtleX.HaveIBeenPwned/) [![Downloads from NuGet](https://img.shields.io/nuget/dt/atlex.haveibeenpwned?cacheSeconds=3600)](https://www.nuget.org/stats/packages/AtleX.HaveIBeenPwned?groupby=Version)

AtleX.HaveIBeenPwned is available as a NuGet package: 

```
install-package AtleX.HaveIBeenPwned
```

# Examples

```csharp

// Create the client
var settings = new HaveIBeenPwnedClientSettings()
{
    ApiKey = "APIKEY", // Get one from https://haveibeenpwned.com/API/Key. Necessary for getting the breaches and pastes an account is in.
    ApplicationName = "TheNameOfYourApplication",
};
using (var client = new HaveIBeenPwnedClient(settings))
{
    // Get all breaches in the system with their details
    var breaches = await client.GetAllBreachesAsync();

    // Get the latest breach in the system
    var latestBreach = await client.GetLatestBreachAsync();

    // Get the breaches for an account. This returns a collection of breaches with their 
    // name. Use the response from GetAllBreachesAsync() to find the corresponding details 
    // by name
    var breaches = await client.GetBreachesAsync("test@example.com"); // Requires an API key

    // Get the breached users of a domain.
    var breachedUsers = await client.GetBreachedDomainUsersAsync("example.com"); // Requires an API key

    // Get breaches for an account, excluding unverified breaches
    var breaches = await client.GetBreachesAsync("test@example.com", BreachMode.ExcludeUnverified); // Requires an API key

    // Get pastes for an email address
    var pastes = await client.GetPastesAsync("test@example.com"); // Requires an API key

    // Verify whether is password is in Pwned Passwords or not
    var isPwned = await client.IsPwnedPasswordAsync("1234");
}
```

All async methods have overrides with `CancellationToken` support.

## Depedency injection

Usage with the dependency injection system of ASP.net Core/.NET Core or higher is fully supported.

```csharp
builder.Services
  .AddSingleton(_ => new HaveIBeenPwnedClientSettings() 
    { 
      ApplicationName = "YourAppName", 
      ApiKey = "YourApiKey" // Only necessary for methods thar require an API key. Can be left blank otherwise
    })
  .AddScoped<IHaveIBeenPwnedClient, HaveIBeenPwnedClient>() // You can also use one of the more specialized interfaces
  .AddHttpClient<HaveIBeenPwnedClient>();
```

# License

AtleX.HaveIBeenPwned uses the MIT license, see the LICENSE file.
