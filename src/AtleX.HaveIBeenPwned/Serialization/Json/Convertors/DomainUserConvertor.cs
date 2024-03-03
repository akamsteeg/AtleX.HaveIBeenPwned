using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AtleX.HaveIBeenPwned.Serialization.Json.Convertors;

/// <summary>
/// Converts a JSON to an <see cref="IEnumerable{T}"/> of <see cref="DomainUser"/>
/// </summary>
internal sealed class DomainUserConvertor : JsonConverter<IEnumerable<DomainUser>>
{
  /// <inheritDoc />
  public override IEnumerable<DomainUser>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    List<DomainUser> result = [];

    var alias = string.Empty;
    List<string> breaches = [];

    while (reader.Read())
    {
      switch (reader.TokenType)
      {
        case JsonTokenType.PropertyName: // Alias of the breached user, typically the name part of an email address
          {
            alias = reader.GetString();
            break;
          }
        case JsonTokenType.StartArray: // The breaches a user is in are stored in an array of strings
          {
            breaches = [];
            break;
          }
        case JsonTokenType.String: // The values in the array of breaches
          {
            var value = reader.GetString();

            breaches!.Add(value!);
            break;
          }
        case JsonTokenType.EndArray: // End of the array with breached users. Next is a new user or the end of the JSON document
          {
            var newDomainUser = new DomainUser()
            {
              Alias = alias,
              Breaches = breaches,
            };

            result.Add(newDomainUser);

            break;
          }
      }
    }

    return result;
  }

  /// <inheritDoc />
  public override void Write(Utf8JsonWriter writer, IEnumerable<DomainUser> value, JsonSerializerOptions options)
    => throw new NotImplementedException();
}
