# Documentation Index

This folder contains the operational and architectural documentation for ObraFlow.

## Start Here

If you are new to the backend, read in this order:

1. `../README.md`
2. `architecture.md`
3. `project-status.md`
4. `setup-and-operations.md`
5. `api-reference.md`
6. `contributing.md`

## Documents

### Product And Planning

- `project-status.md`: current implementation status and missing pieces
- `roadmap.md`: suggested staged evolution of the backend

### Architecture And Delivery

- `architecture.md`: layer responsibilities and dependency direction
- `module-delivery-guide.md`: how to implement a new module without breaking architecture
- `api-guidelines.md`: API design and controller conventions
- `api-reference.md`: current implemented endpoints and sample payloads

### Runtime And Persistence

- `setup-and-operations.md`: local setup, Docker, and runtime notes
- `database.md`: PostgreSQL, EF Core model, and migration workflow
- `demo-protection.md`: demo write protection and reset behavior
- `testing-and-quality.md`: verification expectations, testing direction, and quality rules

### Team Collaboration

- `contributing.md`: branch strategy, change scope, review expectations, and onboarding
- `../../.github/PULL_REQUEST_TEMPLATE.md`: pull request structure for contributors

### Architecture Decision Records

- `decisions/README.md`: how ADRs are used in this repository
- `decisions/0001-layered-architecture.md`: current accepted architectural decision

## Writing Principle

Documentation in this project should:

- describe the current state truthfully
- distinguish clearly between implemented and planned work
- preserve the layered design from `AGENTS.md`
- make onboarding easier for the next contributor
