# ObraFlow

ObraFlow is a portfolio-ready backend project for construction site management. The solution is built with ASP.NET Core, Entity Framework Core, PostgreSQL, and Docker Compose, following a clean layered architecture.

At the current stage, the repository provides the foundational backend structure, the database model, EF Core configuration, an initial migration, and containerized infrastructure for running the API and PostgreSQL together.

## Goals

- Keep the solution compile-ready and architecture-driven
- Separate domain, application, infrastructure, and HTTP concerns
- Model the MVP entities for construction site operations
- Prepare the codebase for feature growth without overengineering

## Current Stack

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL 16
- Docker Compose
- Swagger / OpenAPI

## Solution Structure

```text
src/
├── ObraFlow.Api
├── ObraFlow.Application
├── ObraFlow.Domain
└── ObraFlow.Infrastructure
```

### Layer Responsibilities

- `ObraFlow.Domain`: business entities and enums
- `ObraFlow.Application`: application contracts and DTO space for use cases
- `ObraFlow.Infrastructure`: EF Core, PostgreSQL integration, entity configurations, migrations
- `ObraFlow.Api`: composition root, HTTP pipeline, Swagger, runtime configuration

## MVP Scope

The MVP domain currently models:

- Workers
- Daily reports
- Incidents
- Materials

The persistence layer already contains mappings and an initial migration for these modules.

## Project Status

What is already implemented:

- Layered solution and project references
- EF Core `AppDbContext`
- PostgreSQL provider configuration
- Entity mappings for all MVP entities
- Initial EF Core migration
- Dockerfile for the API
- `docker-compose.yml` with API and PostgreSQL services
- Swagger setup

What is intentionally still pending:

- Controllers and HTTP endpoints for the MVP modules
- Application DTOs, services, and use-case orchestration
- Validation and exception middleware
- Seed data
- Automated tests
- Authentication and authorization

## Quick Start

### Run with Docker Compose

```bash
docker compose up --build
```

Services:

- API: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`
- PostgreSQL: `localhost:5432`

Default database credentials used by Docker Compose:

- Database: `obraflowdb`
- Username: `postgres`
- Password: `postgres`

### Run Locally

1. Start a PostgreSQL instance.
2. Update the connection string in `src/ObraFlow.Api/appsettings.json` if needed.
3. Run the API project:

```bash
dotnet run --project src/ObraFlow.Api/ObraFlow.Api.csproj
```

## Database

The API reads the `DefaultConnection` connection string from configuration. The default local value is:

```text
Host=localhost;Port=5432;Database=obraflowdb;Username=postgres;Password=postgres
```

In Docker Compose, the API overrides this value and connects to the `db` service using the internal hostname `db`.

## Migrations

The repository already includes an initial migration in `ObraFlow.Infrastructure`.

Typical EF Core commands:

```bash
dotnet ef database update --project src/ObraFlow.Infrastructure --startup-project src/ObraFlow.Api
dotnet ef migrations add <MigrationName> --project src/ObraFlow.Infrastructure --startup-project src/ObraFlow.Api
```

## Documentation

Additional project documentation is available in `docs/`:

- `docs/index.md`
- `docs/architecture.md`
- `docs/module-delivery-guide.md`
- `docs/setup-and-operations.md`
- `docs/database.md`
- `docs/api-guidelines.md`
- `docs/testing-and-quality.md`
- `docs/project-status.md`
- `docs/roadmap.md`
- `docs/contributing.md`
- `docs/decisions/0001-layered-architecture.md`
- `docs/decisions/README.md`
- `.github/PULL_REQUEST_TEMPLATE.md`

## Portfolio Notes

This repository is intentionally focused on backend fundamentals:

- clean project boundaries
- persistence modeled with EF Core
- containerized local environment
- space prepared for use cases and HTTP endpoints

It is a strong foundation project that can grow into a full backend API without requiring a structural rewrite.

For team growth, the repository now includes contributor-facing documentation covering onboarding, delivery rules, architectural decisions, and the expected path for implementing new modules.
