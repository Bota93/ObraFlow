# Setup And Operations

All commands below assume the current directory is `backend/`. If you are at the repository root, run `cd backend` first or prefix the paths with `backend/`.

## Prerequisites

To work on ObraFlow locally, install:

- .NET 10 SDK
- Docker Desktop or another Docker runtime with Compose support
- PostgreSQL, if you want to run the database outside Docker

## Option 1: Run With Docker Compose

The simplest way to boot the full local stack is:

```bash
docker compose up --build
```

This starts:

- PostgreSQL 16 on port `5432`
- ObraFlow API on port `5000`

Useful URLs:

- API base URL: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`

## Option 2: Run The API Locally

If PostgreSQL is already available on your machine:

1. Make sure a database named `obraflowdb` exists, or change the connection string.
2. Check `src/ObraFlow.Api/appsettings.json`.
3. Run the API:

```bash
dotnet run --project src/ObraFlow.Api/ObraFlow.Api.csproj
```

Useful local URLs:

- API base URL: `http://localhost:5250`
- HTTPS API: `https://localhost:7129`
- Swagger UI: `http://localhost:5250/swagger`

## Configuration

### Local Default Connection String

`src/ObraFlow.Api/appsettings.json`:

```text
Host=localhost;Port=5432;Database=obraflowdb;Username=postgres;Password=postgres
```

### Docker Compose Connection String

`docker-compose.yml` in `backend/` overrides the connection string using:

```text
Host=db;Port=5432;Database=obraflowdb;Username=postgres;Password=postgres
```

The hostname is `db` because the API connects to the PostgreSQL container over the Docker network.

## Build

The standard build command is:

```bash
dotnet build ObraFlow.slnx
```

## Tests

Run the API integration test project with:

```bash
dotnet test tests/ObraFlow.Api.IntegrationTests/ObraFlow.Api.IntegrationTests.csproj
```

The test suite uses `CustomWebApplicationFactory` and an in-memory SQLite database for HTTP-level verification.

## Swagger

Swagger is enabled only in the Development environment. That behavior is configured in `src/ObraFlow.Api/Program.cs`.

## Notes About The Current Runtime

At this stage:

- the application boots as an ASP.NET Core API
- EF Core is configured against PostgreSQL
- `Workers`, `DailyReports`, and `Incidents` are exposed through controllers
- `Materials` is mapped in persistence but not exposed yet as an HTTP module
- HTTPS redirection is enabled in the pipeline
