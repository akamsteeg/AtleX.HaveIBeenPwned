# AtleX.HaveIBeenPwned

AtleX.HaveIBeenPwned is a client library for [HaveIBeenPwned.com](https://haveibeenpwned.com/). It supports 
finding breaches an account was in, which pastes contained the user's email address, getting the breaches a
domain's mail addresses were in and to check whether a password was in a breach or not.

The AtleX.HaveIBeenPwned library is highly optimized for ease of use, great performance and low resource usage. It is 
thread safe, simple to plug into ASP.net Core(+)'s dependency injection system and [usable on many platforms](https://www.nuget.org/packages/AtleX.HaveIBeenPwned#supportedframeworks-body-tab).

:warning: For some actions, like getting the pastes and breaches for an account, the [HaveIBeenPwned.com API](https://haveibeenpwned.com/) 
requires a API key. These API keys are available on the [HaveIBeenPwned.com website](https://haveibeenpwned.com/API/Key).

## Trust

Leaked data can be a sensitive subject. Just like the [HaveIBeenPwned.com website and API](https://haveibeenpwned.com/), this 
library is carefully designed to be trustworthy and require the least amount of data possible to work. There is no collection
of data. All published packages have reproducible builds, meaning that a build from a certain source version will always
result in the same binaries. Additionally, not a single part of the library is obfuscated. Anyone can easily inspect binaries
in a tool like [ILSpy](https://github.com/icsharpcode/ILSpy) or use a debugger to step through and inspect everything.

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

    // Get the subscribed domains for an API key
    var domains = await client.GetSubscribedDomainsAsync)(); // Requires an API key

    // Get breached users from a domain the API key is subscribed to
    var breachedUsersOfDomain = await client.GetBreachedDomainUsersAsync("example.com"); // Requires an API key

    // Get pastes for an email address
    var pastes = await client.GetPastesAsync("test@example.com"); // Requires an API key

    // Verify whether is password is in Pwned Passwords or not
    var isPwned = await client.IsPwnedPasswordAsync("1234");
}
```

All async methods have overrides with `CancellationToken` support.

## Dependency injection

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