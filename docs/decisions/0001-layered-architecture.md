# ADR 0001: Use A Four-Layer Solution Structure

## Status

Accepted

## Context

ObraFlow is intended to be a portfolio-ready backend project that demonstrates clean structure, correct separation of concerns, and readiness for future feature growth.

The MVP covers construction site management concepts such as workers, daily reports, incidents, and materials. Even at an early stage, the codebase needs clear boundaries so feature work does not collapse into controllers or persistence classes.

## Decision

The solution is split into four projects:

- `ObraFlow.Domain`
- `ObraFlow.Application`
- `ObraFlow.Infrastructure`
- `ObraFlow.Api`

Responsibilities are assigned as follows:

- Domain holds entities and enums
- Application holds DTOs, contracts, and use-case orchestration
- Infrastructure holds EF Core and PostgreSQL persistence
- Api holds HTTP concerns, service registration, and runtime configuration

## Consequences

Positive consequences:

- business entities remain isolated from EF Core configuration
- controllers can stay thin
- persistence logic has a clear home
- the codebase can grow without a structural rewrite

Tradeoffs:

- more projects and references to maintain
- some folders may remain intentionally empty early in the project
- additional discipline is required when implementing new features

## Rules Reinforced By This Decision

- do not move persistence logic into controllers
- do not put EF Core configuration into Domain
- do not place HTTP concerns in Application
- do not collapse all layers into Api
