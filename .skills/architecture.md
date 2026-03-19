# Architecture Skill

## Goal
Preserve a layered backend architecture that is easy to explain in interviews and easy to maintain during development.

## Layer responsibilities

### Domain
Contains:
- entities
- enums
- domain exceptions

Must not contain:
- controllers
- EF Core configuration
- HTTP logic
- Docker concerns

### Application
Contains:
- DTOs
- service interfaces
- use-case orchestration

Must not contain:
- controllers
- DbContext
- direct HTTP concerns

### Infrastructure
Contains:
- AppDbContext
- EF Core configurations
- migrations
- seed logic
- repository or persistence service implementations

Must not contain:
- controller logic
- UI/API contracts unless strictly required by infrastructure adapters

### Api
Contains:
- controllers
- middleware
- dependency injection setup
- app configuration
- swagger

Must not contain:
- raw persistence logic inside controllers

## Decision criteria
When in doubt:
- ask which layer owns the concern
- keep business model away from HTTP and persistence details
- prefer simple layering over pattern inflation

## Anti-patterns to avoid
- fat controllers
- anemic folder chaos
- putting everything in Api
- repository pattern without real value
- fake clean architecture with empty abstractions