# Module Delivery Guide

## Purpose

This guide explains how to add or complete an ObraFlow module in a way that remains consistent with the repository architecture.

The current MVP modules are:

- Workers
- DailyReports
- Incidents
- Materials

Current implementation note:

- `Workers`, `DailyReports`, and `Incidents` already exist as full vertical slices
- `Materials` currently exists only in Domain and Infrastructure persistence

## Delivery Principle

A module should be implemented as a vertical slice across the correct layers, not by pushing all logic into the API.

## Recommended Order

### 1. Domain

Work in Domain only if business structure changes.

Examples:

- add a new entity
- add a new enum
- add a domain exception when there is actual business behavior to protect

Do not add EF Core configuration here.

### 2. Application

Define:

- request DTOs
- response DTOs
- service contracts
- use-case orchestration interfaces or services

Do not add HTTP-specific types here.

### 3. Infrastructure

Implement:

- persistence details
- EF Core queries
- repository or service implementations only when needed
- entity configurations
- migrations

### 4. API

Add:

- controller endpoints
- request binding
- response mapping
- status code handling

Controllers should remain thin and delegate behavior instead of owning persistence logic.

## Suggested Folder Shape As The Project Grows

An example future organization could look like this:

```text
src/
├── ObraFlow.Application
│   ├── Abstractions/
│   ├── DTOs/
│   └── Services/
├── ObraFlow.Api
│   ├── Controllers/
│   └── Middlewares/
└── ObraFlow.Infrastructure
    ├── Persistence/
    │   ├── Configurations/
    │   └── Migrations/
    └── Services/
```

This is a direction guide, not a reason to create folders before they are needed.

## Done Criteria For A Module

A module is in good shape when:

- the domain model is clear
- application contracts exist
- persistence is isolated in Infrastructure
- API endpoints are thin and documented
- integration tests cover the main endpoint flow
- migrations are added if schema changed
- project documentation reflects the new behavior

## Anti-Patterns To Avoid

- controller methods directly using raw persistence logic
- duplicate DTOs with unclear purpose
- feature code added only in the API layer
- database changes without migrations
- documentation that claims endpoints exist before they are implemented
