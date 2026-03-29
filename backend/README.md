# ObraFlow Backend

Backend workspace for ObraFlow, a construction operations MVP built with ASP.NET Core, EF Core, PostgreSQL, and a layered architecture.

## Current Scope

Implemented end-to-end:

- workers CRUD
- daily reports CRUD
- incidents CRUD
- dashboard summary endpoint

Also implemented:

- PostgreSQL persistence with EF Core migrations
- seeded demo data for workers, daily reports, and incidents
- integration tests with isolated HTTP-level verification
- global exception middleware for unhandled failures
- `GET /health` with database connectivity check
- configurable Swagger availability through `Swagger:Enabled`
- demo write protection and demo reset support

Planned next steps:

- materials through Application and API
- broader test layers beyond the current integration suite
- richer domain and application error mapping
- authentication and authorization after the MVP stabilizes

## Architecture

The backend is split into four projects:

- `ObraFlow.Domain`
- `ObraFlow.Application`
- `ObraFlow.Infrastructure`
- `ObraFlow.Api`

Responsibilities stay separated:

- `Domain` contains entities and enums
- `Application` contains DTOs and service contracts
- `Infrastructure` contains EF Core, migrations, seed data, and service implementations
- `Api` contains controllers, runtime configuration, Swagger, and HTTP concerns

## Runtime

Public demo URLs:

- App: `https://obraflow.adrianalcaraz.es`
- API: `https://api-obraflow.adrianalcaraz.es`
- Health: `https://api-obraflow.adrianalcaraz.es/health`
- Swagger: `https://api-obraflow.adrianalcaraz.es/swagger`

From `backend/`, run with Docker:

```bash
docker compose up --build
```

URLs:

- API: `http://localhost:5000`
- Health: `http://localhost:5000/health`
- Swagger: `http://localhost:5000/swagger`

Run locally against PostgreSQL:

```bash
dotnet run --project src/ObraFlow.Api/ObraFlow.Api.csproj
```

URLs:

- API: `http://localhost:5250`
- HTTPS API: `https://localhost:7129`
- Health: `http://localhost:5250/health`
- Swagger: `http://localhost:5250/swagger`

For environment-based local configuration, start from `backend/.env.example`.

For Supabase + Render deployment guidance, see `../docs/deployment.md`.

For the production-like Render target used by this repository:

- Root Directory: `backend`
- Dockerfile path: `Dockerfile`
- health check: `/health`

## Testing

The backend uses xUnit integration tests with `WebApplicationFactory`.

Current coverage includes:

- workers endpoints
- daily reports endpoints
- incidents endpoints
- dashboard summary endpoint
- demo write rate limiting
- demo reset behavior

These tests exercise the real API pipeline without mocking the HTTP layer.

## Demo Protection

For shared demo environments, the backend supports:

- optional write rate limiting on protected `POST` endpoints
- deterministic demo database reset through `reset-demo`

See:

- `docs/demo-protection.md`
- `docs/testing-and-quality.md`
- `docs/api-reference.md`

## Documentation

Start here:

- `docs/index.md`
- `docs/architecture.md`
- `docs/setup-and-operations.md`
- `docs/api-reference.md`
