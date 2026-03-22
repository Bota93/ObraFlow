# Database

All commands below assume the current directory is `backend/`. If you are at the repository root, run `cd backend` first or prefix the paths with `backend/`.

## Provider

ObraFlow uses PostgreSQL through the `Npgsql.EntityFrameworkCore.PostgreSQL` provider.

## DbContext

The main EF Core context is `AppDbContext` in `src/ObraFlow.Infrastructure/Persistence/AppDbContext.cs`.

Registered sets:

- `Workers`
- `DailyReports`
- `Incidents`
- `Materials`

The context applies entity configurations automatically from the Infrastructure assembly.

## Entity Mapping Summary

### Worker

Table: `workers`

Columns:

- `Id` as primary key
- `Name` required, max length 140
- `Role` required, max length 80
- `PhoneNumber` required, max length 30
- `HourlyRate` required, precision `(10,2)`
- `CreatedAtUtc` required
- `IsActive` required

Relationship:

- one worker has many daily reports

Seed data:

- three workers are inserted through `WorkerSeed`

### DailyReport

Table: `dailyReports`

Columns:

- `Id` as primary key
- `Date` required
- `WorkerId` required foreign key
- `HoursWorked` required, precision `(5,2)`
- `Description` required, max length 500

Delete behavior:

- cascade delete from `workers`

### Incident

Table: `incidents`

Columns:

- `Id` as primary key
- `Title` required, max length 150
- `Description` required, max length 1000
- `Status` required
- `ReportedAtUtc` required

### Material

Table: `materials`

Columns:

- `Id` as primary key
- `Name` required, max length 120
- `Quantity` required, precision `(10,2)`
- `Unit` required, max length 30

## Migrations

The initial migration is already present:

- `20260319093208_InitialCreate`
- `20260319120000_WorkersModule`

The current migration set creates the MVP tables, the `dailyReports` to `workers` foreign key, and the worker seed entries reflected in the model snapshot.

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

## Implementation Guidance

When the project grows:

- keep all EF Core configurations under `Infrastructure/Persistence/Configurations`
- keep migrations under `Infrastructure/Persistence/Migrations`
- avoid placing persistence annotations or EF Core setup inside Domain
