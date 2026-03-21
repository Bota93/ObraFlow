# ObraFlow

ObraFlow is a portfolio-ready construction site management backend built with ASP.NET Core, Entity Framework Core, PostgreSQL, Docker Compose, and a strict layered architecture.

The repository already includes working MVP slices for `Workers`, `DailyReports`, and `Incidents`, plus persistence support for `Materials`. The project is structured to stay buildable, easy to extend, and aligned with the architectural rules in `AGENTS.md`.

## Features

- Workers management (CRUD)
- Daily reports tracking
- Incident management
- Dashboard summary with aggregated metrics
- Integration tests with isolated test database
- Dockerized environment with PostgreSQL

## Current Stack

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL 16
- Docker Compose
- Swagger / OpenAPI
- xUnit + FluentAssertions integration tests

## Solution Structure

```text
src/
├── ObraFlow.Api
├── ObraFlow.Application
├── ObraFlow.Domain
└── ObraFlow.Infrastructure

tests/
└── ObraFlow.Api.IntegrationTests
```

## Layer Responsibilities

- `ObraFlow.Domain`: entities and enums only
- `ObraFlow.Application`: DTOs and service contracts
- `ObraFlow.Infrastructure`: EF Core, PostgreSQL, entity configurations, migrations, service implementations, seed data
- `ObraFlow.Api`: controllers, HTTP pipeline, Swagger, DI, runtime configuration

## MVP Coverage

Implemented vertical slices:

- Workers
- DailyReports
- Incidents

Implemented at persistence level only:

- Materials

## Current Status

Already implemented:

- layered solution and project references
- PostgreSQL `AppDbContext`
- entity configurations for all MVP entities
- migrations under `Infrastructure/Persistence/Migrations`
- coherent seed data for workers, daily reports, and incidents
- closed MVP modules for `Workers`, `DailyReports`, and `Incidents`
- CRUD endpoints for `Workers`, `DailyReports`, and `Incidents`
- `GET /dashboard/summary`
- application DTOs and service contracts for `Workers`, `DailyReports`, `Incidents`, and `Dashboard`
- infrastructure service implementations for `Workers`, `DailyReports`, `Incidents`, and `Dashboard`
- Swagger/OpenAPI
- Docker Compose for API + PostgreSQL
- API integration tests for `Workers`, `DailyReports`, `Incidents`, and `Dashboard`

Still pending:

- Materials application module and HTTP endpoints
- centralized exception middleware
- a shared API error contract beyond default validation responses
- broader test coverage at infrastructure and application levels
- authentication and authorization

## Quick Start

### Run With Docker Compose

```bash
docker compose up --build
```

Services:

- API: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`
- PostgreSQL: `localhost:5432`

Default Docker database credentials:

- Database: `obraflowdb`
- Username: `postgres`
- Password: `postgres`

### Run Locally

1. Start PostgreSQL.
2. Check the connection string in `src/ObraFlow.Api/appsettings.json`.
3. Apply migrations if needed.
4. Run the API:

```bash
dotnet run --project src/ObraFlow.Api/ObraFlow.Api.csproj
```

## Common Commands

Build the solution:

```bash
dotnet build ObraFlow.slnx
```

Run API integration tests:

```bash
dotnet test tests/ObraFlow.Api.IntegrationTests/ObraFlow.Api.IntegrationTests.csproj
```

Apply migrations:

```bash
dotnet ef database update --project src/ObraFlow.Infrastructure --startup-project src/ObraFlow.Api
```

Add a migration:

```bash
dotnet ef migrations add <MigrationName> --project src/ObraFlow.Infrastructure --startup-project src/ObraFlow.Api
```

## API Surface

Current routes:

- `/dashboard/summary`
- `/workers`
- `/daily-reports`
- `/incidents`

`/dashboard/summary` exposes `GET`.

`/workers`, `/daily-reports`, and `/incidents` expose `GET`, `GET by id`, `POST`, `PUT`, and `DELETE`.

For concrete examples, see `docs/api-reference.md`.

## Documentation

Start here:

- `docs/index.md`
- `docs/architecture.md`
- `docs/project-status.md`
- `docs/setup-and-operations.md`
- `docs/api-reference.md`

## Portfolio Notes

ObraFlow is already more than a scaffold: it demonstrates clean boundaries, EF Core persistence, containerized runtime, tested HTTP endpoints, and a controlled path for adding the remaining module work without collapsing the architecture.
