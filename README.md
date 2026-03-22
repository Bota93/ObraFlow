# ObraFlow

ObraFlow is a construction site management backend designed to replace manual workflows such as paper, spreadsheets, and WhatsApp with a structured, scalable digital system.

This project is built as a production-style backend with clean architecture, real persistence, integration tests, and a monorepo structure ready for a frontend application.

---

## Problem

Construction teams still operate with fragmented tools:

- Work reports on paper
- Incidents tracked informally
- Workers managed without centralized systems

This leads to:

- Loss of information
- Lack of traceability
- Inefficient coordination

---

## Solution

ObraFlow provides a backend system to manage:

- Workers
- Daily work reports
- Incidents tracking
- Operational metrics through a dashboard

Everything is exposed through a clean REST API ready to be consumed by a frontend, with React planned for the next stage.

---

## Architecture

The backend follows a strict layered architecture:

- Domain: entities and business rules
- Application: use cases and DTOs
- Infrastructure: EF Core, PostgreSQL, persistence
- API: controllers, HTTP layer, Swagger

No shortcuts. No mixed responsibilities.

---

## Repository Structure

```text
backend/
├── src/        # .NET solution (Domain, Application, Infrastructure, API)
├── tests/      # Integration tests
├── docs/       # Technical documentation
├── docker-compose.yml
└── ObraFlow.slnx

frontend/
└── (planned React app)
```

---

## Tech Stack

### Backend

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose
- xUnit + FluentAssertions

### Frontend (planned)

- React
- Vite
- TailwindCSS

---

## Features

### Workers

- CRUD operations
- Validation and persistence
- Integration tested

### Daily Reports

- Track work per worker and day
- Business validation for hours and descriptions
- Linked to workers

### Incidents

- Lifecycle: Open -> In Progress -> Resolved
- Structured tracking instead of informal communication

### Dashboard

- Aggregated operational data
- Worker stats
- Incident distribution
- Activity metrics

---

## Testing

- Integration tests using `WebApplicationFactory`
- Isolated database per test run
- Real HTTP-level validation, not just unit tests

Run tests:

```bash
dotnet test backend/tests/ObraFlow.Api.IntegrationTests
```

---

## Running The Project

### With Docker

```bash
docker compose -f backend/docker-compose.yml up --build
```

### Without Docker

```bash
dotnet run --project backend/src/ObraFlow.Api
```

---

## Documentation

- Backend overview: `backend/README.md`
- Docs index: `backend/docs/index.md`
- Contributing: `backend/docs/contributing.md`

---

## Why This Project Matters

This is not a tutorial project.

It is designed to demonstrate:

- Real backend architecture decisions
- Clean separation of concerns
- Production-like setup with database, Docker, and tests
- Ability to scale into a full product with a SaaS-ready base

---

## Status

Backend MVP complete:

- Workers: complete
- DailyReports: complete
- Incidents: complete
- Dashboard: complete

Frontend: planned with React.

---

## Author

Adrián Alcaraz  
Junior Software Developer focused on building real-world systems with solid architecture and scalability in mind.
