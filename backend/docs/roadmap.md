# Roadmap

## Goal

Grow ObraFlow from a persistence-ready backend foundation into a production-style API without losing the current architectural discipline.

## Phase 1: Foundation Stabilization

Current focus:

- keep the solution compiling cleanly
- keep PostgreSQL connectivity stable
- keep Docker Compose working
- keep migrations consistent

Already mostly in place:

- layered solution
- PostgreSQL provider
- initial migration
- Docker setup

## Phase 2: MVP Modules

Current state:

- `Workers` implemented
- `DailyReports` implemented
- `Incidents` implemented
- `Materials` pending outside persistence

Finish the remaining MVP module work for:

- Materials

For the remaining module and any expansions, the target is:

- Application DTOs
- service contracts
- persistence operations
- thin controllers
- proper HTTP status codes

## Phase 3: Validation And Error Handling

Add:

- request validation
- domain or application-level error mapping
- middleware-based exception handling
- predictable API error responses

## Phase 4: Operational Quality

Add:

- broader seed data strategy if more modules need it
- logging
- health checks
- broader automated coverage across layers
- API examples in documentation

## Phase 5: Security And Growth

Add:

- authentication
- role-based authorization
- audit-friendly behaviors
- CI/CD support

## Team Scaling Notes

To support multiple contributors safely:

- implement one module at a time
- keep ADRs for important structural decisions
- document new conventions as they appear
- avoid broad rewrites while MVP slices are still landing
