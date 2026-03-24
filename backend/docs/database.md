# Database

All commands below assume the current directory is `backend/`.

## Provider

ObraFlow uses PostgreSQL through `Npgsql.EntityFrameworkCore.PostgreSQL`.

## DbContext

The main EF Core context is `src/ObraFlow.Infrastructure/Persistence/AppDbContext.cs`.

Registered sets:

- `Workers`
- `DailyReports`
- `Incidents`
- `Materials`

Entity configurations are applied automatically from the Infrastructure assembly.

## Seeded Demo Data

The backend includes deterministic seed data for:

- workers
- daily reports
- incidents

That seeded state is used by the dashboard, integration tests, and demo reset flow.

## Migrations

Current migration set:

- `20260319093208_InitialCreate`
- `20260319120000_WorkersModule`
- `20260321170000_DashboardSeedData`

The current model snapshot reflects the existing MVP tables and seeded demo data used by the backend.

## Common EF Core Commands

Apply migrations:

```bash
dotnet ef database update --project src/ObraFlow.Infrastructure --startup-project src/ObraFlow.Api
```

Add a new migration:

```bash
dotnet ef migrations add <MigrationName> --project src/ObraFlow.Infrastructure --startup-project src/ObraFlow.Api
```

Remove the last migration:

```bash
dotnet ef migrations remove --project src/ObraFlow.Infrastructure --startup-project src/ObraFlow.Api
```

## Demo Reset

For demo environments, the backend can recreate the seeded state through:

```bash
dotnet run --project src/ObraFlow.Api/ObraFlow.Api.csproj -- reset-demo
```

This resets the database and restores the deterministic workers, daily reports, and incidents records.

## Guidance

- keep EF Core configurations under `Infrastructure/Persistence/Configurations`
- keep migrations under `Infrastructure/Persistence/Migrations`
- do not move persistence concerns into `Domain`
