# AtleX.HaveIBeenPwned

AtleX.HaveIBeenPwned is a client library for the [HaveIBeenPwned.com website](https://haveibeenpwned.com/). It supports 
finding breaches an account was in, which pastes contained the user's email address and to check whether the password
was in a breach or not.

Supported .NET frameworks:
* NETSTANDARD 2.0

# Installation

AtleX.HaveIBeenPwned is available [as NuGet package](https://www.nuget.org/packages/AtleX.HaveIBeenPwned/):

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
  var isPwned = await client.IsPwnedPassword("1234");
}

// Override the timeout
var settings = new ClientSettings()
{
	TimeOut = TimeSpan.FromSeconds(30); // Use a 30 seconds timeout
};
using (var client = new HaveIBeenPwnedClient(settings))
{
  // Do something
}

// Override client (mock for unit testing for example):
using (var client = new HaveIBeenPwnedClient(new ClientSettings(), new MockServiceClient()))
{
  // Do something
}
```

# License

AtleX.HaveIBeenPwned uses the MIT license, see the LICENSE file.
