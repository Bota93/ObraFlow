# ObraFlow

ObraFlow is a construction site management system designed to replace manual workflows such as paper, spreadsheets, and WhatsApp with a structured, scalable digital platform.

This project is built as a monorepo with a production-style backend, real persistence, integration tests, Docker support, and a frontend portfolio application that consumes the API.

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

ObraFlow provides a backend and frontend platform to manage:

- Workers
- Daily work reports
- Incidents tracking
- Operational metrics through a dashboard
- a portfolio-ready frontend consuming live API data

Everything is exposed through a clean REST API and consumed by a React frontend organized around the main product workflows.

---

## Architecture

The backend follows a strict layered architecture:

- Domain: entities and business rules
- Application: use cases and DTOs
- Infrastructure: EF Core, PostgreSQL, persistence
- API: controllers, HTTP layer, Swagger

No shortcuts. No mixed responsibilities.

The frontend stays isolated in its own workspace and consumes the backend API without duplicating backend business logic.

---

## Validation Status

The project has been validated with:

- `dotnet build`
- `dotnet test`
- `dotnet run --project backend/src/ObraFlow.Api`
- `docker compose -f backend/docker-compose.yml up --build`
- successful frontend-to-backend communication

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
└── React application consuming the backend API
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

### Frontend

- React
- Vite
- TailwindCSS
- TanStack Query
- Axios

---

## Features

### Frontend Portfolio App

- Dashboard overview connected to the backend summary endpoint
- Workers, daily reports, and incidents views connected to API data
- Feature-oriented React structure with shared HTTP client and routing

### Backend Modules

- Workers
- DailyReports
- Incidents
- Dashboard

### Persistence Present But Not Yet Exposed End-To-End

- Materials

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

Backend URLs with Docker:

- API: `http://localhost:5000`
- Swagger: `http://localhost:5000/swagger`

### Backend Without Docker

```bash
dotnet run --project backend/src/ObraFlow.Api
```

Backend URLs in local development:

- API: `http://localhost:5250`
- Swagger: `http://localhost:5250/swagger`

### Frontend

```bash
cd frontend
pnpm install
pnpm dev
```

Default frontend dev URL:

- App: `http://localhost:5173`

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
- Ability to scale into a full product with a SaaS-ready architecture

---

## Status

Backend MVP complete:

- Workers: complete
- DailyReports: complete
- Incidents: complete
- Dashboard: complete
- validated with `dotnet build`, `dotnet test`, local runtime, and Docker Compose

Frontend MVP available:

- Dashboard connected to API
- Workers connected to API
- DailyReports connected to API
- Incidents connected to API
- validated with successful frontend/backend communication against the API

Still pending:

- Materials end-to-end module through Application and API
- richer workflows beyond read-focused portfolio views
- create and update forms
- additional frontend polish for product workflows

---

## Author

Adrián Alcaraz  
Junior Software Developer focused on building real-world systems with solid architecture and scalability in mind.
