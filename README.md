# JobTracker

An ASP.NET Core job application tracker built with C# and .NET 10.

## Current structure

- `src/JobTracker.Api` — ASP.NET Core Web API
- `tests/JobTracker.Api.Tests` — xUnit tests
- `JobTracker.slnx` — solution file

## Run locally

```powershell
dotnet restore JobTracker.slnx
dotnet run --project src/JobTracker.Api
dotnet test JobTracker.slnx
```

The API starts with the template weather endpoint for now. The next milestone is replacing it with the job-application domain, persistence, and tests.
