# AGENTS.md

## Mission
Build ObraFlow as a serious portfolio project and product-ready system for construction site management.

The repository must support:
- a production-style backend
- a future frontend application
- clean evolution without mixing responsibilities across workspaces

## Product context
ObraFlow is a construction site management system.

It replaces fragmented workflows such as:
- paper-based daily reports
- incident tracking through informal communication
- unstructured worker management

The current backend MVP includes:
- Workers
- DailyReports
- Incidents
- Dashboard

Backend persistence already includes:
- Materials

Materials is not yet implemented end-to-end through the Application and API layers.

## Repository layout
This repository is organized as a monorepo:

- `backend/`
  - .NET solution
  - Docker Compose
  - backend-specific skills in `backend/.skills/`
  - backend documentation
  - source code
  - tests

- `frontend/`
  - frontend application (React planned)
  - UI layer consuming backend API

## Monorepo rules
- Keep backend and frontend isolated
- Do not mix backend code into `frontend/`
- Do not mix frontend code into `backend/`
- Do not place application logic at repository root
- Root should contain only coordination files (README, AGENTS, etc.)
- Always respect workspace boundaries when modifying code

## Backend architecture rules
The backend is split into four projects:

- ObraFlow.Domain
  - Business entities
  - Enums
  - Domain exceptions
  - No framework dependencies unless strictly necessary

- ObraFlow.Application
  - DTOs
  - Service contracts
  - Use-case orchestration
  - No HTTP concerns
  - No EF Core persistence logic

- ObraFlow.Infrastructure
  - EF Core
  - PostgreSQL
  - DbContext
  - Configurations
  - Migrations
  - Seed data

- ObraFlow.Api
  - Controllers
  - Middleware
  - DI configuration
  - Swagger/OpenAPI
  - HTTP concerns only

## Current implementation status
### Backend
- `Workers`, `DailyReports`, and `Incidents` are implemented end-to-end
- `Dashboard` is implemented as an API endpoint with aggregated metrics
- `Materials` exists in Domain and Infrastructure persistence only
- PostgreSQL persistence is configured through EF Core
- Docker Compose runs the API with PostgreSQL
- Integration tests cover the implemented API modules

### Frontend
- planned
- not yet implemented

## Frontend direction
Frontend must:
- consume backend API only
- avoid business logic duplication
- keep structure simple and maintainable
- avoid unnecessary complexity (state libraries, patterns) in MVP phase
- prioritize clear product workflows and usability over visual complexity in the MVP phase

Frontend is still provisional. Do not let frontend assumptions drive backend architecture changes.

## Non-negotiable rules
- Do not collapse layers into Api
- Do not move persistence logic into controllers
- Do not put EF Core configuration into Domain
- Do not introduce unnecessary patterns
- Do not add non-compilable code
- Do not leave pseudo-code
- Do not invent unused files
- Do not refactor unrelated code during feature work

## Coding conventions
- Use English for all code
- Use clear and explicit naming
- Prefer small, single-responsibility files
- Use async for I/O operations
- Return proper HTTP status codes
- Keep controllers thin

Backend paths must respect monorepo structure:
- Solution → `backend/ObraFlow.slnx`
- API project → `backend/src/ObraFlow.Api`
- Tests → `backend/tests/ObraFlow.Api.IntegrationTests`
- Configurations → `backend/src/ObraFlow.Infrastructure/Persistence/Configurations`
- Migrations → `backend/src/ObraFlow.Infrastructure/Persistence/Migrations`

## Working rules for AI assistants
When proposing changes:

1. Identify the target workspace (`backend/` or `frontend/`)
2. Read the current structure before modifying anything
3. Respect namespaces and existing patterns
4. Generate complete files when possible
5. Prefer minimal safe changes
6. Justify architectural changes
7. Classify issues before fixing:
   - build error
   - dependency error
   - namespace/reference error
   - Docker error
   - EF Core migration error
   - runtime configuration error
   - frontend build/tooling error
   - API integration issue

## Delivery order
1. Keep the backend buildable
2. Keep backend tests passing
3. Keep Docker working
4. Keep migrations consistent
5. Implement product features in the correct workspace
6. Improve backend validation and error handling
7. Implement frontend features without duplicating backend logic
8. Improve documentation
9. Expand product capabilities without breaking workspace boundaries

## Git workflow
- main = stable
- develop = integration
- feature/* = isolated work

Rules:
- atomic commits
- no unrelated refactors
- keep history clean

## Current stack

### Backend
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL 16
- Docker Compose
- Swagger/OpenAPI
- xUnit
- FluentAssertions

### Frontend (planned)
- React
- Vite
- TailwindCSS

## Definition of useful help
Useful help must:
- solve the real problem
- preserve architecture
- keep the project buildable
- avoid overengineering
