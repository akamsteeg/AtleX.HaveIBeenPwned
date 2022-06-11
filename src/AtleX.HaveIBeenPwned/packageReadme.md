﻿# AtleX.HaveIBeenPwned

AtleX.HaveIBeenPwned is a client library for the [HaveIBeenPwned.com website](https://haveibeenpwned.com/). It supports 
finding breaches an account was in, which pastes contained the user's email address and to check whether a password
was in a breach or not.

:warning: For some actions, like getting the pastes and breaches for an account, the [HaveIBeenPwned.com API](https://haveibeenpwned.com/) 
requires a API key. These API keys are available on the [HaveIBeenPwned.com website](https://haveibeenpwned.com/API/Key).

# Platform support

| .NET (4.7.2+)      |  .NET Core (2.0+)  |
|:------------------:|:------------------:|
| :heavy_check_mark: | :heavy_check_mark: |

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

    // Get the breaches for an account. This returns a collection of breaches with their 
    // name. Use the response from GetAllBreachesAsync() to find the corresponding details 
    // by name
    var breaches = await client.GetBreachesAsync("test@example.com"); // Requires an API key

    // Get breaches for an account, excluding unverified breaches
    var breaches = await client.GetBreachesAsync("test@example.com", BreachMode.ExcludeUnverified); // Requires an API key

    // Get pastes for an email address
    var pastes = await client.GetPastesAsync("test@example.com"); // Requires an API key

    // Verify whether is password is in Pwned Passwords or not
    var isPwned = await client.IsPwnedPasswordAsync("1234");
}
```

All async methods have overrides with `CancellationToken` support.