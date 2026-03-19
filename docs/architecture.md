# Architecture

## Overview

ObraFlow follows a four-layer architecture designed to keep business concepts, application flow, infrastructure, and HTTP concerns separated.

```text
Clients
  -> ObraFlow.Api
  -> ObraFlow.Application
  -> ObraFlow.Domain
  <- ObraFlow.Infrastructure
```

The API composes the application and infrastructure pieces. Domain stays at the center and does not depend on EF Core or HTTP-specific code.

## Projects

### ObraFlow.Domain

Purpose:

- store business entities
- store enums
- remain independent from persistence and transport concerns

Current contents:

- `Worker`
- `DailyReport`
- `Incident`
- `Material`
- `IncidentStatus`

### ObraFlow.Application

Purpose:

- host DTOs
- define service contracts
- orchestrate use cases
- stay independent from ASP.NET Core and EF Core implementation details

Current status:

- the project exists and is referenced correctly
- folders for abstractions and DTOs are prepared
- concrete use-case implementations are still pending

### ObraFlow.Infrastructure

Purpose:

- implement persistence with EF Core
- define `AppDbContext`
- configure entity mappings
- store migrations

Current contents:

- `AppDbContext`
- entity configurations for all MVP entities
- initial migration targeting PostgreSQL

### ObraFlow.Api

Purpose:

- configure services and middleware
- expose controllers and HTTP endpoints
- host Swagger/OpenAPI
- load runtime configuration

Current contents:

- `Program.cs` with dependency injection and EF Core registration
- Swagger setup
- runtime configuration files
- Dockerfile

Current limitation:

- there are no feature controllers yet, so Swagger starts but does not expose the MVP resources yet

## Dependency Direction

The intended dependency flow is:

- `Api` depends on `Application` and `Infrastructure`
- `Infrastructure` depends on `Application` and `Domain`
- `Application` depends on `Domain`
- `Domain` depends on no internal project

This keeps the business model reusable and avoids pushing persistence or HTTP logic into the wrong layer.

## Why This Matters

This structure supports the project goals from `AGENTS.md`:

- thin controllers
- persistence isolated in Infrastructure
- no HTTP concerns in Application
- no EF Core configuration in Domain
- minimal safe growth as the MVP modules are implemented
