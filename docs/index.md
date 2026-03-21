# Documentation Index

This folder contains the operational and architectural documentation for ObraFlow.

## Start Here

If you are new to the project, read in this order:

1. `README.md`
2. `docs/architecture.md`
3. `docs/project-status.md`
4. `docs/setup-and-operations.md`
5. `docs/api-reference.md`
6. `docs/contributing.md`

## Documents

### Product And Planning

- `docs/project-status.md`: current implementation status and missing pieces
- `docs/roadmap.md`: suggested staged evolution of the backend

### Architecture And Delivery

- `docs/architecture.md`: layer responsibilities and dependency direction
- `docs/module-delivery-guide.md`: how to implement a new module without breaking architecture
- `docs/api-guidelines.md`: API design and controller conventions
- `docs/api-reference.md`: current implemented endpoints and sample payloads

### Runtime And Persistence

- `docs/setup-and-operations.md`: local setup, Docker, and runtime notes
- `docs/database.md`: PostgreSQL, EF Core model, and migration workflow
- `docs/testing-and-quality.md`: verification expectations, testing direction, and quality rules

### Team Collaboration

- `docs/contributing.md`: branch strategy, change scope, review expectations, and onboarding
- `.github/PULL_REQUEST_TEMPLATE.md`: pull request structure for contributors

### Architecture Decision Records

- `docs/decisions/README.md`: how ADRs are used in this repository
- `docs/decisions/0001-layered-architecture.md`: current accepted architectural decision

## Writing Principle

Documentation in this project should:

- describe the current state truthfully
- distinguish clearly between implemented and planned work
- preserve the layered design from `AGENTS.md`
- make onboarding easier for the next contributor
