// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents an <see cref="Exception"/> that occured in an <see cref="IHaveIBeenPwnedClient"/>
/// </summary>
public class HaveIBeenPwnedClientException
  : Exception
{
  /// <summary>
  /// Initializes a new instance of <see cref="HaveIBeenPwnedClientException"/>
  /// </summary>
  public HaveIBeenPwnedClientException()
    : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of <see cref="HaveIBeenPwnedClientException"/>
  /// with the specified error message
  /// </summary>
  /// <param name="message">
  /// The message that describes the error
  /// </param>
  public HaveIBeenPwnedClientException(string message)
    : base(message)
  {
  }

  /// <summary>
  /// Initializes a new instance of <see cref="HaveIBeenPwnedClientException"/>
  /// with the specified error message and inner <see cref="Exception"/>
  /// </summary>
  /// <param name="message">
  /// The message that describes the error
  /// </param>
  /// <param name="innerException">
  /// The <see cref="Exception"/> or the error
  /// </param>
  public HaveIBeenPwnedClientException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
