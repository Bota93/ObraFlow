# AGENTS.md

## Mission
Build ObraFlow as a portfolio-ready backend project using .NET, PostgreSQL, Docker and clean layering.

## Product context
ObraFlow is a construction site management backend.
The MVP includes:
- Workers
- DailyReports
- Incidents
- Materials

## Architecture rules
The solution is split into four projects:

- ObraFlow.Domain
  - Business entities
  - Enums
  - Domain exceptions
  - No framework-specific dependencies unless strictly necessary

- ObraFlow.Application
  - DTOs
  - Service contracts
  - Use-case orchestration
  - No HTTP concerns
  - No EF Core persistence details

- ObraFlow.Infrastructure
  - EF Core
  - PostgreSQL persistence
  - DbContext
  - Entity configurations
  - Migrations
  - Seed data
  - External service implementations

- ObraFlow.Api
  - Controllers
  - Middleware
  - Dependency injection
  - Configuration
  - Swagger/OpenAPI
  - HTTP concerns only

## Non-negotiable rules
- Do not collapse all layers into Api
- Do not move persistence logic into controllers
- Do not put EF Core configuration into Domain
- Do not introduce unnecessary patterns just for aesthetics
- Do not add code that is not compile-ready
- Do not leave pseudo-code
- Do not create duplicate DTOs without need
- Do not invent files that are not referenced by the solution

## Coding conventions
- Use English for code, classes, methods and properties
- Use clear names
- Prefer small files with one responsibility
- Prefer async methods for I/O work
- Return proper HTTP status codes
- Keep controllers thin
- Put persistence configuration in Infrastructure/Persistence/Configurations
- Keep migrations in Infrastructure/Persistence/Migrations

## Working rules for AI assistants
When proposing changes:
1. Read the current project structure first
2. Respect existing namespaces
3. Generate complete files, not fragments, when possible
4. Prefer minimal safe changes over broad rewrites
5. Explain why a change is needed if it affects architecture
6. Before suggesting a fix, identify whether the issue is:
   - build error
   - dependency error
   - namespace/reference error
   - Docker error
   - EF Core migration error
   - runtime configuration error

## Delivery order
1. Make the solution compile
2. Make PostgreSQL connection work
3. Make Docker Compose work
4. Make migrations work
5. Implement feature modules
6. Improve validation and error handling
7. Add documentation and polish

## Git workflow
- main = stable
- develop = integration
- feature/* = work branches

Do not suggest unrelated refactors in the middle of a feature fix.
Always prefer atomic commits.

## Current stack
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose
- Swagger/OpenAPI

## Definition of useful help
Useful help must:
- solve the concrete problem
- preserve architecture
- keep the project buildable
- avoid overengineering