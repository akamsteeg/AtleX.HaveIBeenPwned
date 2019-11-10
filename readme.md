# AtleX.HaveIBeenPwned

AtleX.HaveIBeenPwned is a client library for the [HaveIBeenPwned.com website](https://haveibeenpwned.com/). It supports 
finding breaches an account was in, which pastes contained the user's email address and to check whether the password
was in a breach or not.

[![Build status](https://interastra.visualstudio.com/OSS%20-%20CI/_apis/build/status/AtleX.HaveIBeenPwned%20CI?branchName=master)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) [![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/interastra/OSS%20-%20CI/11.svg?maxAge=3600)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) 


# Platform support

| .NET (4.6.1+)      |  .NET Core (2.0+)  |
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
    ApiKey = "APIKEY", // Get one from https://haveibeenpwned.com/API/Key. Not necessary for only checking pwned passwords
    ApplicationName = "TheNameOfYourApplication",
};
using (var client = new HaveIBeenPwnedClient(settings))
{
    // Get all breaches in the system with their details
    var breaches = await client.GetAllBreachesAsync();

    // Get the breaches for an account. This returns a collection of breaches with their 
    // name. Use the response from GetAllBreachesAsync() to find the corresponding details 
    // by name
    var breaches = await client.GetBreachesAsync("test@example.com");

    // Get breaches for an account, excluding unverified breaches
    var breaches = await client.GetBreachesAsync("test@example.com", BreachMode.ExcludeUnverified);

    // Get pastes for an email address
    var pastes = await client.GetPastesAsync("test@example.com");

    // Verify whether is password is in Pwned Passwords or not
    var isPwned = await client.IsPwnedPasswordAsync("1234");
}
```

# License

AtleX.HaveIBeenPwned uses the MIT license, see the LICENSE file.
