# AtleX.HaveIBeenPwned

AtleX.HaveIBeenPwned is a client library for the [HaveIBeenPwned.com website](https://haveibeenpwned.com/). It supports 
finding breaches an account was in, which pastes contained the user's email address and to check whether a password
was in a breach or not.

:warning: For some actions, like getting the pastes and breaches for an account, the [HaveIBeenPwned.com API](https://haveibeenpwned.com/) 
requires a API key. These API keys are available on the [HaveIBeenPwned.com website](https://haveibeenpwned.com/API/Key).

[![Build status](https://interastra.visualstudio.com/OSS%20-%20CI/_apis/build/status/AtleX.HaveIBeenPwned%20CI?branchName=master)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) [![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/interastra/OSS%20-%20CI/11.svg?maxAge=3600)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) 


# Platform support

| .NET (4.7.2+)      |  .NET Core (2.0+)  |
|:------------------:|:------------------:|
| :heavy_check_mark: | :heavy_check_mark: |


# Installation

AtleX.HaveIBeenPwned is available as NuGet package: [![NuGet](https://img.shields.io/nuget/v/AtleX.HaveIBeenPwned.svg?maxAge=3600)](https://www.nuget.org/packages/AtleX.HaveIBeenPwned/)

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

    // Get the breaches for an account. This returns a collection of breaches with their 
    // name. Use the response from GetAllBreachesAsync() to find the corresponding details 
    // by name
    var breaches = await client.GetBreachesAsync("test@example.com"); //Rquires an API key

    // Get breaches for an account, excluding unverified breaches
    var breaches = await client.GetBreachesAsync("test@example.com", BreachMode.ExcludeUnverified); //Rquires an API key

    // Get pastes for an email address
    var pastes = await client.GetPastesAsync("test@example.com"); //Rquires an API key

    // Verify whether is password is in Pwned Passwords or not
    var isPwned = await client.IsPwnedPasswordAsync("1234");
}
```

All async methods have overrides with `CancellationToken` support.

# License

AtleX.HaveIBeenPwned uses the MIT license, see the LICENSE file.
