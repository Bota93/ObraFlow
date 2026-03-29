# Testing And Quality

## Current Testing Approach

ObraFlow currently uses integration tests for backend verification.

The suite is built with:

- xUnit
- FluentAssertions
- `WebApplicationFactory`
- in-memory SQLite for isolated test runs

Tests are written as full integration tests against the real HTTP pipeline using `WebApplicationFactory`, without mocking the transport layer.

## What The Integration Suite Covers

Current coverage includes:

- workers endpoints
- daily reports endpoints
- incidents endpoints
- dashboard summary endpoint
- demo write rate limiting
- demo database reset behavior

The tests exercise the real API surface. They do not mock the HTTP layer.

Operational endpoints such as `/health` and `/swagger` are currently verified manually rather than through the integration suite.

## Verification Priorities

Before merging backend changes, the most important checks are:

1. the solution builds
2. integration tests pass
3. Docker Compose still works when runtime or persistence changes
4. migrations remain consistent when the schema changes
5. documentation matches the implemented behavior

## Near-Term Quality Gaps

Still missing:

- application-layer tests
- infrastructure-focused persistence tests against PostgreSQL behavior
- automated coverage for operational endpoints and broader error-path verification
- broader CI quality gates
