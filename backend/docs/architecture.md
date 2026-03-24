# Architecture

## Overview

ObraFlow backend follows a four-layer architecture so business concepts, application contracts, persistence, and HTTP concerns stay separated.

## Layers

### Domain

Contains:

- entities
- enums
- domain-focused types

It should not contain EF Core configuration or HTTP concerns.

### Application

Contains:

- DTOs
- service contracts
- use-case level contracts between the API and the implementation

It should not contain controllers or persistence implementation details.

### Infrastructure

Contains:

- `AppDbContext`
- EF Core configurations
- migrations
- seed data
- service implementations

It is the persistence and runtime implementation layer for the application contracts.

### Api

Contains:

- controllers
- dependency injection bootstrap
- middleware and HTTP pipeline configuration
- Swagger and runtime configuration

It should stay thin and delegate behavior to application-facing services.

## Request Flow

The typical request path is:

1. an HTTP request reaches an API controller
2. the controller calls an application service contract
3. the infrastructure implementation executes persistence logic through EF Core
4. DTOs are returned to the API and serialized back to the client

Dependency direction stays aligned with that flow:

- `Api` depends on `Application` and `Infrastructure`
- `Infrastructure` depends on `Application` and `Domain`
- `Application` depends on `Domain`
- `Domain` depends on no internal project

## Current Scope

Implemented end-to-end today:

- workers
- daily reports
- incidents
- dashboard summary

Implemented at persistence level only:

- materials
