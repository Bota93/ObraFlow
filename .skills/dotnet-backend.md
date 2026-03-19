# .NET Backend Skill

## Goal
Develop a robust ASP.NET Core Web API with PostgreSQL and EF Core using practical, portfolio-quality conventions.

## Standards
- Use controllers, not minimal APIs, for this project
- Use dependency injection through Program.cs
- Use Swagger/OpenAPI
- Use async database calls
- Use ActionResult<T> in controllers
- Keep DTOs separate from entities
- Use EF Core migrations instead of EnsureCreated for normal workflow

## Project-specific conventions
- Primary namespace prefix: ObraFlow
- Migrations assembly: ObraFlow.Infrastructure
- DbContext location: ObraFlow.Infrastructure/Persistence
- EF Core entity configurations: ObraFlow.Infrastructure/Persistence/Configurations
- Startup project: ObraFlow.Api

## Database conventions
- Use PostgreSQL naming consciously
- Prefer explicit table names in configurations
- Define max lengths and precision
- Define relationships explicitly
- Keep seed data minimal and realistic

## Before generating code
Check:
1. current namespace
2. target project
3. project references
4. package references
5. framework version
6. whether the code belongs in Domain, Application, Infrastructure or Api

## Common tasks
- create entity
- create DTO
- create EF configuration
- register DbContext
- create migration
- create controller
- wire service into DI

## Common mistakes
- adding package to wrong project
- wrong namespace after moving files
- migration command using wrong startup project
- controller referencing Infrastructure types directly when a contract should exist
- mixing domain entity and API request model