# Setup And Operations

All commands below assume the current directory is `backend/`. If you are at the repository root, run `cd backend` first or prefix the paths with `backend/`.

## Prerequisites

To work on ObraFlow locally, install:

- .NET 10 SDK
- Docker Desktop or another Docker runtime with Compose support
- PostgreSQL, if you want to run the database outside Docker

## Public Demo

Live environment URLs:

- App: `https://obraflow.adrianalcaraz.es`
- API: `https://api-obraflow.adrianalcaraz.es`
- Swagger UI: `https://api-obraflow.adrianalcaraz.es/swagger`

## Option 1: Run With Docker Compose

The simplest way to boot the full local stack is:

1. Copy `.env.example` to `.env`.
2. Run:

```bash
docker compose up --build
```

This starts:

- PostgreSQL 16 on port `5432`
- ObraFlow API on port `5000`

The API applies EF Core migrations automatically in this Docker setup so a fresh local database can boot without manual schema steps.

Useful URLs:

- API base URL: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`

## Option 2: Run The API Locally

If PostgreSQL is already available on your machine:

1. Make sure a database named `obraflowdb` exists, or set `ConnectionStrings__DefaultConnection`.
2. Optionally export variables from `backend/.env.example`.
3. Run the API:

```bash
dotnet run --project src/ObraFlow.Api/ObraFlow.Api.csproj
```

Useful local URLs:

- API base URL: `http://localhost:5250`
- HTTPS API: `https://localhost:7129`
- Swagger UI: `http://localhost:5250/swagger`

If you run the frontend locally, the development client now tries `http://localhost:5250` first and falls back to `http://localhost:5000` when the API is only available through Docker Compose.

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

### Other Runtime Variables

Useful environment variables for local and hosted environments:

- `ConnectionStrings__DefaultConnection`
- `Cors__AllowedOrigins`
- `Database__ApplyMigrationsOnStartup`
- `Swagger__Enabled`
- `ASPNETCORE_FORWARDEDHEADERS_ENABLED`

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

Swagger is controlled through configuration:

- Development defaults to enabled
- other environments stay disabled unless `Swagger__Enabled=true`

That behavior is configured in `src/ObraFlow.Api/Program.cs`.

## Health Endpoint

The API exposes `GET /health` and returns `200 OK` when the application and database are reachable.

## Notes About The Current Runtime

At this stage:

- the application boots as an ASP.NET Core API
- EF Core is configured against PostgreSQL
- `Workers`, `DailyReports`, and `Incidents` are exposed through controllers
- `Materials` is mapped in persistence but not exposed yet as an HTTP module
- HTTPS redirection is enabled in the pipeline
