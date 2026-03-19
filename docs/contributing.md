# Contributing

## Purpose

This project is structured so more contributors can join over time without having to reverse-engineer decisions from the codebase.

Contributions should preserve:

- clean layer boundaries
- small, reviewable changes
- compile-ready code
- truthful documentation

## Branch Strategy

The repository follows the workflow defined in `AGENTS.md`:

- `main`: stable branch
- `develop`: integration branch
- `feature/*`: feature work

Examples:

- `feature/workers-crud`
- `feature/incidents-validation`
- `feature/materials-endpoints`

## Change Scope

Prefer atomic changes. A single pull request should usually focus on one of these:

- one feature module
- one infrastructure change
- one migration
- one documentation improvement
- one validation or error-handling slice

Avoid mixing unrelated refactors into feature work.

## Architectural Guardrails

Before opening a pull request, check these rules:

- do not move persistence logic into controllers
- do not put EF Core configuration into Domain
- do not add HTTP concerns to Application
- do not collapse all behavior into the API project
- do not invent abstractions with no concrete need

## Expected Delivery Pattern For Features

When implementing a new module or expanding an existing one, the preferred sequence is:

1. Update or add Domain entities only if business structure changes.
2. Add Application DTOs and contracts.
3. Add Infrastructure persistence implementation and EF Core mappings if needed.
4. Add API controllers and request/response wiring.
5. Add migration if the database model changes.
6. Update documentation.

## Pull Request Expectations

A good pull request should include:

- a short explanation of the problem being solved
- the architectural area affected
- any migration or configuration impact
- what was verified locally
- documentation updates when behavior changed

## Review Checklist

Reviewers should verify:

- the change matches the layer responsibilities
- the API stays thin
- the naming is consistent and in English
- migrations live only in Infrastructure
- no planned work is documented as if it already existed

## Local Onboarding Checklist

For a new contributor:

1. Read `README.md`.
2. Read `docs/index.md`.
3. Read `docs/architecture.md`.
4. Start the stack with Docker Compose or local PostgreSQL.
5. Review the current migration and entity configurations.
6. Pick one isolated module slice before touching multiple layers.

## Documentation Rule

If you change architecture, runtime behavior, or contributor workflow, update the relevant document in `docs/` in the same branch.
