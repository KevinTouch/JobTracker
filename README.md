# JobTracker

JobTracker is a focused ASP.NET Core 10 Web API for keeping a local record of job applications. It uses Entity Framework Core with SQLite, so it runs without external services or accounts.

## Features

- Create, list, view, update, and delete applications
- Status validation: `Wishlist`, `Applied`, `Interview`, `Offer`, `Rejected`, or `Withdrawn`
- Required-field and URL validation with standard `400` responses
- SQLite persistence with an initial EF Core migration
- Swagger UI and OpenAPI documentation available in development
- Filtering by status/company with page-size limits
- Follow-up and interview dates, location, work arrangement, salary, and contact tracking

## Run locally

Requirements: .NET 10 SDK.

```powershell
dotnet restore JobTracker.slnx
dotnet run --project src/JobTracker.Api --launch-profile https
```

The API applies pending migrations at startup and stores data in `jobtracker.db` in the API working directory. The default HTTPS profile exposes `https://localhost:7112` and `http://localhost:5133`; HTTP requests redirect to HTTPS. In development, open `/swagger` to explore and execute requests interactively.

## API examples

Create an application:

```http
POST /api/JobApplications
Content-Type: application/json

{
  "company": "Acme Corp",
  "jobTitle": "Backend Engineer",
  "status": "Applied",
  "applicationDate": "2026-09-11",
  "jobUrl": "https://example.com/jobs/123",
  "notes": "Referred by a former teammate"
}
```

Available endpoints:

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/JobApplications` | List applications, newest first |
| GET | `/api/JobApplications/{id}` | Get one application |
| POST | `/api/JobApplications` | Create an application |
| PUT | `/api/JobApplications/{id}` | Replace an application |
| DELETE | `/api/JobApplications/{id}` | Delete an application |

List requests support optional `status`, `company`, `page`, and `pageSize` query parameters. For example: `/api/JobApplications?status=Interview&page=1&pageSize=10`.

Applications can also track `location`, `workArrangement`, `salaryRange`, `contactName`, `interviewDate`, and `nextActionDate`. Follow-ups on or before today appear in the dashboard's due counter.

## Test

```powershell
dotnet test JobTracker.slnx
```

The test project includes controller tests and HTTP-level integration tests using an isolated EF Core in-memory database.
