# Database

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
- `FullName` required, max length 120
- `Role` required, max length 80
- `Phone` required, max length 30

Relationship:

- one worker has many daily reports

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

It creates the four MVP tables and the `dailyReports` to `workers` foreign key.

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
