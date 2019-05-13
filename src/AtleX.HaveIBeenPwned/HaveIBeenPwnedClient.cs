using AtleX.HaveIBeenPwned.Clients.Http;
using Pitcher;
using SwissArmyKnife;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents the default <see cref=" IHaveIBeenPwnedClient"/> in the system
  /// </summary>
  public sealed class HaveIBeenPwnedClient
    : Disposable, IHaveIBeenPwnedClient
  {
    /// <summary>
    /// Gets the <see cref="IHaveIBeenPwnedClient"/> to use
    /// </summary>
    private readonly IHaveIBeenPwnedClient _serviceClient;

    /// <summary>
    /// Gets whether the <see cref="IHaveIBeenPwnedClient"/> must be disposed or not
    /// </summary>
    /// <remarks>
    /// When we create the <see cref="IHaveIBeenPwnedClient"/> ourselves, it must
    /// be disposed. When it was injected via the constructor, we must not
    /// dispose it. After all, the <see cref="IHaveIBeenPwnedClient"/> can be
    /// used by the callee too and it would be rude and error-prone to dispose it
    /// in that case.
    /// </remarks>
    private readonly bool _enableClientDisposing;

    /// <summary>
    /// Initializes a new instance of <see cref="HaveIBeenPwnedClient"/>
    /// </summary>
    public HaveIBeenPwnedClient()
      : this(CreateDefaultServiceClient())
    {
      this._enableClientDisposing = true;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HaveIBeenPwnedClient"/> with the
    /// specified <see cref="IHaveIBeenPwnedClient"/>
    /// </summary>
    /// <param name="serviceClient">
    /// The <see cref="IHaveIBeenPwnedClient"/> to use
    /// </param>
    public HaveIBeenPwnedClient(IHaveIBeenPwnedClient serviceClient)
    {
      this._serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));

      this._enableClientDisposing = false;
    }

    /// <summary>
    /// Get the breaches for an account
    /// </summary>
    /// <param name="account">
    /// The account to get the breaches for
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account)
    {
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));

      var result = await this._serviceClient.GetBreachesAsync(account, BreachMode.Default)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the breaches for an account
    /// </summary>
    /// <param name="account">
    /// The account to get the breaches for
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));

      var result = await this._serviceClient.GetBreachesAsync(account, BreachMode.Default, cancellationToken)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the breaches for an account
    /// </summary>
    /// <param name="account">
    /// The account to get the breaches for
    /// </param>
    /// <param name="modes">
    /// The <see cref="BreachMode"/> flags to specify extra breaches to get
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes)
    {
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));
      this.ThrowIfDisposed();

      var result = await this._serviceClient
        .GetBreachesAsync(account, modes)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the breaches for an account
    /// </summary>
    /// <param name="account">
    /// The account to get the breaches for
    /// </param>
    /// <param name="modes">
    /// The <see cref="BreachMode"/> flags to specify extra breaches to get
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));

      this.ThrowIfDisposed();

      var result = await this._serviceClient
        .GetBreachesAsync(account, modes, cancellationToken)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the pastes for an email address
    /// </summary>
    /// <param name="emailAddress">
    /// The email address to get the pastes for
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Paste"/> the email address was found in
    /// </returns>
    public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress)
    {
      Throw.ArgumentNull.When(emailAddress.IsNullOrWhiteSpace(), nameof(emailAddress));

      this.ThrowIfDisposed();

      var result = await this._serviceClient
        .GetPastesAsync(emailAddress)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the pastes for an email address
    /// </summary>
    /// <param name="emailAddress">
    /// The email address to get the pastes for
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Paste"/> the email address was found in
    /// </returns>
    public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(emailAddress.IsNullOrWhiteSpace(), nameof(emailAddress));

      this.ThrowIfDisposed();

      var result = await this._serviceClient
        .GetPastesAsync(emailAddress, cancellationToken)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Gets whether the specified password is found in a password list
    /// </summary>
    /// <param name="password">
    /// The password to check
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with a <see cref="bool"/>
    /// indicating whether the password was found or not
    /// </returns>
    public async Task<bool> IsPwnedPasswordAsync(string password)
    {
      Throw.ArgumentNull.When(password.IsNullOrWhiteSpace(), nameof(password));

      this.ThrowIfDisposed();

      var result = await this._serviceClient
        .IsPwnedPasswordAsync(password)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Gets whether the specified password is found in a password list
    /// </summary>
    /// <param name="password">
    /// The password to check
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with a <see cref="bool"/>
    /// indicating whether the password was found or not
    /// </returns>
    public async Task<bool> IsPwnedPasswordAsync(string password, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(password.IsNullOrWhiteSpace(), nameof(password));

      this.ThrowIfDisposed();

      var result = await this._serviceClient
        .IsPwnedPasswordAsync(password, cancellationToken)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or
    /// resetting unmanaged resources
    /// </summary>
    /// <param name="disposing">
    /// True when disposing, false when finalizing
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this._enableClientDisposing && this._serviceClient is IDisposable disposableServiceClient)
        {
          disposableServiceClient.Dispose();
        }
      }
    }

    /// <summary>
    /// Create the default <see cref="IHaveIBeenPwnedClient"/> to use
    /// </summary>
    /// <returns>
    /// A new <see cref="IHaveIBeenPwnedClient"/>
    /// </returns>
    private static IHaveIBeenPwnedClient CreateDefaultServiceClient()
    {
      var clientSettings = HttpHaveIBeenPwnedClientSettings.Default;
      var result = new HttpHaveIBeenPwnedClient(clientSettings);

      return result;
    }
  }
}
