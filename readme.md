# AtleX.HaveIBeenPwned

AtleX.HaveIBeenPwned is a client library for the [HaveIBeenPwned.com website](https://haveibeenpwned.com/). It supports 
finding breaches an account was in, which pastes contained the user's email address and to check whether the password
was in a breach or not.

[![Build status](https://interastra.visualstudio.com/OSS%20-%20CI/_apis/build/status/AtleX.HaveIBeenPwned%20CI?branchName=master)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) [![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/interastra/OSS%20-%20CI/11.svg?maxAge=3600)](https://dev.azure.com/interastra/OSS%20-%20CI/_build?definitionId=11&_a=summary) 


# Platform support

| .NET Framework     |     .NET Core      |
|:------------------:|:------------------:|
| :heavy_check_mark: | :heavy_check_mark: |


# Installation

AtleX.HaveIBeenPwned is available as NuGet package: [![NuGet](https://img.shields.io/nuget/v/AtleX.HaveIBeenPwned.svg?maxAge=3600)](https://www.nuget.org/packages/AtleX.HaveIBeenPwned/)

```
install-package AtleX.HaveIBeenPwned
```

# Examples

```csharp

// Get the breaches for an account
using (var client = new HaveIBeenPwnedClient())
{
  var breaches = await client.GetBreachesAsync("test@example.com");
}

// Get breaches for an account, including unverified breaches
using (var client = new HaveIBeenPwnedClient())
{
  var breaches = await client.GetBreachesAsync("test@example.com", BreachMode.IncludeUnverified);
}

// Get pastes for an email address
using (var client = new HaveIBeenPwnedClient())
{
  var pastes = await client.GetPastesAsync("test@example.com");
}

// Verify whether is password is in Pwned Passwords or not
using (var client = new HaveIBeenPwnedClient())
{
  var isPwned = await client.IsPwnedPasswordAsync("1234");
}

// Override the timeout
var settings = new HttpHaveIBeenPwnedClientSettings()
{
	TimeOut = TimeSpan.FromSeconds(30); // Use a 30 seconds timeout
};

using (var httpHibpClient = new HttpHaveIBeenPwnedClientSettings(settings))
using (var client = new c(httpHibpClient))
{
  // Do something
}

// Override client (mock for unit testing for example):
using (var client = new HaveIBeenPwnedClient(new MockServiceClient()))
{
  // Do something
}
```

# License

AtleX.HaveIBeenPwned uses the MIT license, see the LICENSE file.
