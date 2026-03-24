# Demo Protection

ObraFlow includes lightweight demo protection so shared environments remain stable and presentable.

## Purpose

The goal is to prevent public demo abuse without introducing unnecessary product complexity into the MVP.

## Write Rate Limiting

When `DemoMode:EnableWriteRateLimiting` is enabled:

- rate limiting applies per client IP
- only protected `POST` endpoints are limited
- the current limit is `5` create requests per `10` minutes

Protected endpoints:

- `POST /workers`
- `POST /daily-reports`
- `POST /incidents`

When the limit is exceeded, the API returns:

- `429 Too Many Requests`

## Demo Reset

The API also supports resetting the demo database to the deterministic seeded state used by the project.

Run:

```bash
dotnet run --project src/ObraFlow.Api/ObraFlow.Api.csproj -- reset-demo
```

This recreates the database state for:

- workers
- daily reports
- incidents

## Notes

- rate limiting is configuration-driven
- reset is intended for controlled demo environments and operations use
- this protection is focused on demo stability, not full production security
