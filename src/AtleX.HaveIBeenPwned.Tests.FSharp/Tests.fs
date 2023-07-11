module Tests

open System
open Xunit
open AtleX.HaveIBeenPwned

[<Fact>]
let ``My test`` () =
    let s = HaveIBeenPwnedClientSettings();
    s.ApiKey <- Guid.Empty.ToString();
    s.ApplicationName <- "Unit tests fsharp";

    let c = new HaveIBeenPwnedClient(s);

    let results = c.GetAllBreachesAsync() |> Async.AwaitTask;

    Assert.NotNull(results);
